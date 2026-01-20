using Microsoft.AspNetCore.Mvc;
using Services;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using ViewModels;

namespace RocketLauncher.Controllers
{
    [Route("Intake")]
    public class IntakeController : Controller
    {
        private readonly IntakeService _intakeService;

        public IntakeController(IntakeService intakeService)
        {
            _intakeService = intakeService;
        }

        // GET: /Intake
        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            var userId = Guid.Parse("6a44ab59-bf76-4385-8222-5584dcb5dde2"); //GetCurrentUserId();
            var vm = await _intakeService.GetIntakeFormAsync(userId);
            return View(vm); // Views/Intake/Index.cshtml
        }

        // POST: /Intake
        [HttpPost("")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(IntakeFormVM model)
        {
            var userId = Guid.Parse("6a44ab59-bf76-4385-8222-5584dcb5dde2"); //GetCurrentUserId();
            try
            {
                await _intakeService.SubmitIntakeAsync(userId, model);
                return RedirectToAction("Index", "User");
            }
            catch (ArgumentException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);

                var vm = await _intakeService.GetIntakeFormAsync(userId);
                MergePostedAnswers(vm, model);
                return View(vm);
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);

                var vm = await _intakeService.GetIntakeFormAsync(userId);
                MergePostedAnswers(vm, model);
                return View(vm);
            }
        }

        /// <summary>
        /// Resolve the current user's Guid.
        /// If you don't have auth wired yet, replace this with your dev user id logic.
        /// </summary>
        private Guid GetCurrentUserId()
        {
            var idStr = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!string.IsNullOrWhiteSpace(idStr) && Guid.TryParse(idStr, out var userId))
                return userId;

            return Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa");
        }

        /// <summary>
        /// Ensures user doesn't lose their posted answers when we re-fetch the form.
        /// </summary>
        private static void MergePostedAnswers(IntakeFormVM target, IntakeFormVM posted)
        {
            if (target?.Questions == null || posted?.Questions == null) return;

            var postedMap = posted.Questions.ToDictionary(q => q.QuestionId, q => q);

            foreach (var q in target.Questions)
            {
                if (!postedMap.TryGetValue(q.QuestionId, out var p)) continue;

                q.Answer = p.Answer;
                q.Answers = p.Answers ?? new System.Collections.Generic.List<string>();
            }
        }
    }
}
