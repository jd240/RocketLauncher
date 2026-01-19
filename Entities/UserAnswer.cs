using System;
using System.ComponentModel.DataAnnotations;

namespace Entities
{
    public class UserAnswer
    {
        [Key]
        public Guid UserAnswerId { get; set; }

        [Required]
        public Guid UserId { get; set; }
        public User User { get; set; } = null!;

        [Required]
        public Guid QuestionId { get; set; }
        public Question Question { get; set; } = null!;

        // Store as string; for MultiSelect store JSON array (e.g. ["a","b"])
        [Required]
        public string Value { get; set; } = null!;

        public DateTime AnsweredAtUtc { get; set; } = DateTime.UtcNow;

        public int QuestionVersion { get; set; } = 1;
    }
}
