using System;
using System.ComponentModel.DataAnnotations;

namespace Entities
{
    public class QuestionOption
    {
        [Key]
        public Guid QuestionOptionId { get; set; }

        [Required]
        public Guid QuestionId { get; set; }

        public Question Question { get; set; } = null!;

        // Stable option value stored in answers (e.g. "yes", "no", "sole_trader")
        [Required]
        [StringLength(100)]
        public string Value { get; set; } = null!;

        // UI label shown to users (e.g. "Yes", "No", "Sole trader")
        [Required]
        [StringLength(200)]
        public string Label { get; set; } = null!;

        public int SortOrder { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
