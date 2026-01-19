using System;
using System.ComponentModel.DataAnnotations;

namespace Entities
{
    public class UserTask
    {
        [Key]
        public Guid UserTaskId { get; set; }

        [Required]
        public Guid UserId { get; set; }
        public User User { get; set; } = null!;

        [Required]
        public Guid TaskItemId { get; set; }
        public TaskItem TaskItem { get; set; } = null!;

        public bool IsCompleted { get; set; } = false;

        public DateTime? CompletedAtUtc { get; set; }
    }
}
