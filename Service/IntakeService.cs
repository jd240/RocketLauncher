using DataTransferObject;
using DataTransferObject.DTO;
using Entities;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;
using ViewModels;

namespace Services
{
    public class IntakeService : IntakeIService
    {
        private readonly UserDbcontext _db;

        public IntakeService(UserDbcontext db)
        {
            _db = db;
        }

        /// <summary>
        /// Loads the Intake module + its questions/options, and (if present) the user's existing answers.
        /// </summary>
        public async Task<IntakeFormVM> GetIntakeFormAsync(Guid userId)
        {
            var intakeModule = await _db.Modules
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Key == "intake" && m.IsActive);

            if (intakeModule == null)
                throw new InvalidOperationException("Intake module not found. Ensure it is seeded (Module.Key = 'intake').");

            var questions = await _db.Questions
                .AsNoTracking()
                .Where(q => q.ModuleId == intakeModule.ModuleId && q.IsActive)
                .OrderBy(q => q.SortOrder)
                .ToListAsync();

            var questionIds = questions.Select(q => q.QuestionId).ToList();

            var options = await _db.QuestionOptions
                .AsNoTracking()
                .Where(o => questionIds.Contains(o.QuestionId) && o.IsActive)
                .OrderBy(o => o.SortOrder)
                .ToListAsync();

            var answers = await _db.UserAnswers
                .AsNoTracking()
                .Where(a => a.UserId == userId && questionIds.Contains(a.QuestionId))
                .ToDictionaryAsync(a => a.QuestionId, a => a.Value);

            // Build VM
            var form = new IntakeFormVM
            {
                ModuleId = intakeModule.ModuleId,
                Questions = questions.Select(q =>
                {
                    var qvm = new IntakeQuestionVM
                    {
                        QuestionId = q.QuestionId,
                        Key = q.Key,
                        Prompt = q.Prompt,
                        QuestionType = q.QuestionType,
                        IsRequired = q.IsRequired,
                        Options = options
                            .Where(o => o.QuestionId == q.QuestionId)
                            .Select(o => new IntakeOptionVM { Value = o.Value, Label = o.Label })
                            .ToList()
                    };

                    if (answers.TryGetValue(q.QuestionId, out var val) && !string.IsNullOrWhiteSpace(val))
                    {
                        if (IsMultiSelect(q.QuestionType))
                        {
                            // Stored as JSON array: ["a","b"]
                            qvm.Answers = TryParseJsonStringArray(val);
                        }
                        else
                        {
                            qvm.Answer = val;
                        }
                    }

                    return qvm;
                }).ToList()
            };

            return form;
        }

        /// <summary>
        /// Validates and saves Intake answers (PATCH/upsert).
        /// Also ensures a UserModule row exists for intake and marks it completed.
        /// </summary>
        public async Task SubmitIntakeAsync(Guid userId, IntakeFormVM form)
        {
            if (form == null)
                throw new ArgumentNullException(nameof(form));

            // Ensure intake module exists
            var intakeModule = await _db.Modules
                .FirstOrDefaultAsync(m => m.ModuleId == form.ModuleId && m.Key == "intake" && m.IsActive);

            if (intakeModule == null)
                throw new InvalidOperationException("Invalid intake module. Ensure the intake module is seeded and active.");

            // Load questions from DB to validate required + types (do not trust client)
            var dbQuestions = await _db.Questions
                .AsNoTracking()
                .Where(q => q.ModuleId == intakeModule.ModuleId && q.IsActive)
                .Select(q => new { q.QuestionId, q.QuestionType, q.IsRequired, q.Version })
                .ToListAsync();

            var dbQuestionIds = dbQuestions.Select(q => q.QuestionId).ToHashSet();

            // Filter only answers for questions that belong to intake module
            var submitted = (form.Questions ?? new List<IntakeQuestionVM>())
                .Where(q => dbQuestionIds.Contains(q.QuestionId))
                .ToList();

            // Validate required
            foreach (var q in dbQuestions)
            {
                if (!q.IsRequired) continue;

                var submittedQ = submitted.FirstOrDefault(x => x.QuestionId == q.QuestionId);
                if (submittedQ == null)
                    throw new ArgumentException("Missing required intake answer.");

                if (IsMultiSelect(q.QuestionType))
                {
                    if (submittedQ.Answers == null || submittedQ.Answers.Count == 0)
                        throw new ArgumentException("Please answer all required intake questions.");
                }
                else
                {
                    if (string.IsNullOrWhiteSpace(submittedQ.Answer))
                        throw new ArgumentException("Please answer all required intake questions.");
                }
            }

            // Upsert answers
            var submittedIds = submitted.Select(s => s.QuestionId).ToList();

            var existingAnswers = await _db.UserAnswers
                .Where(a => a.UserId == userId && submittedIds.Contains(a.QuestionId))
                .ToListAsync();

            var existingMap = existingAnswers.ToDictionary(a => a.QuestionId, a => a);

            foreach (var qvm in submitted)
            {
                var dbQ = dbQuestions.First(x => x.QuestionId == qvm.QuestionId);

                string valueToStore;
                if (IsMultiSelect(dbQ.QuestionType))
                {
                    valueToStore = SerializeJsonStringArray(qvm.Answers ?? new List<string>());
                }
                else
                {
                    valueToStore = (qvm.Answer ?? string.Empty).Trim();
                }

                if (existingMap.TryGetValue(qvm.QuestionId, out var existing))
                {
                    existing.Value = valueToStore;
                    existing.AnsweredAtUtc = DateTime.UtcNow;
                    existing.QuestionVersion = dbQ.Version;
                }
                else
                {
                    _db.UserAnswers.Add(new UserAnswer
                    {
                        UserAnswerId = Guid.NewGuid(),
                        UserId = userId,
                        QuestionId = qvm.QuestionId,
                        Value = valueToStore,
                        AnsweredAtUtc = DateTime.UtcNow,
                        QuestionVersion = dbQ.Version
                    });
                }
            }

            // Ensure UserModule row exists for intake and mark complete
            var userModule = await _db.UserModules
                .FirstOrDefaultAsync(um => um.UserId == userId && um.ModuleId == intakeModule.ModuleId);

            if (userModule == null)
            {
                userModule = new UserModule
                {
                    UserModuleId = Guid.NewGuid(),
                    UserId = userId,
                    ModuleId = intakeModule.ModuleId,
                    Status = "Completed",
                    CreatedAtUtc = DateTime.UtcNow,
                    ActivatedAtUtc = DateTime.UtcNow,
                    CompletedAtUtc = DateTime.UtcNow
                };
                _db.UserModules.Add(userModule);
            }
            else
            {
                // If they revisit intake, keep it completed but update timestamps sensibly
                userModule.Status = "Completed";
                userModule.ActivatedAtUtc ??= DateTime.UtcNow;
                userModule.CompletedAtUtc = DateTime.UtcNow;
            }

            await _db.SaveChangesAsync();
        }

        // -----------------------
        // Helpers
        // -----------------------

        private static bool IsMultiSelect(string? questionType)
            => string.Equals(questionType, "MultiSelect", StringComparison.OrdinalIgnoreCase);

        private static List<string> TryParseJsonStringArray(string json)
        {
            // Minimal, dependency-free parser for ["a","b"].
            // If you already use System.Text.Json, you can swap this for JsonSerializer.Deserialize<List<string>>(json).
            try
            {
                return System.Text.Json.JsonSerializer.Deserialize<List<string>>(json) ?? new List<string>();
            }
            catch
            {
                return new List<string>();
            }
        }

        private static string SerializeJsonStringArray(List<string> values)
        {
            return System.Text.Json.JsonSerializer.Serialize(values ?? new List<string>());
        }
    }
}
