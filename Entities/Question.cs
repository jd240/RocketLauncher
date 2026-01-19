
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Entities
{
    public class Question
    {
        [Key]
        public Guid QuestionId { get; set; }

        // Stable semantic identifier (used by rules, NOT editable by admins)
        [Required]
        [StringLength(100)]
        public string Key { get; set; } = null!;

        // Display text (admin-editable)
        [Required]
        [StringLength(500)]
        public string Prompt { get; set; } = null!;

        // Optional helper text shown under the question
        [StringLength(1000)]
        public string? HelpText { get; set; }

        // Question behavior / rendering type
        [Required]
        [StringLength(50)]
        public string QuestionType { get; set; } = null!;
        // e.g. SingleSelect, MultiSelect, Text, Number, Date, YesNo

        // Intake vs module-specific
        // NULL = intake question
        public Guid? ModuleId { get; set; }
        public Module? Module { get; set; }

        // Validation / UX behavior
        public bool IsRequired { get; set; } = false;

        // Ordering within intake or module
        public int SortOrder { get; set; }

        // Soft toggle for deprecating questions
        public bool IsActive { get; set; } = true;

        // Versioning support (optional but very useful)
        public int Version { get; set; } = 1;

        // Navigation: options for select-type questions
        public ICollection<QuestionOption> Options { get; set; }
            = new List<QuestionOption>();
    }
}
