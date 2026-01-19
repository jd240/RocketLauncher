using System;
using System.ComponentModel.DataAnnotations;

namespace Entities
{
    public class UserModule
    {
        [Key]
        public Guid UserModuleId { get; set; }

        [Required]
        public Guid UserId { get; set; }
        public User User { get; set; } = null!;

        [Required]
        public Guid ModuleId { get; set; }
        public Module Module { get; set; } = null!;

        // "Locked", "Active", "Completed", "Skipped"
        [Required]
        [StringLength(20)]
        public string Status { get; set; } = "Locked";
        public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
        public DateTime? ActivatedAtUtc { get; set; }
        public DateTime? CompletedAtUtc { get; set; }
    }
}
