using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Entities
{
    public class Module
    {
        [Key]
        public Guid ModuleId { get; set; }

        // Stable code for rules/routing (e.g. "intake", "domain", "insurance")
        [Required]
        [StringLength(100)]
        public string Key { get; set; } = null!;

        [Required]
        [StringLength(150)]
        public string Title { get; set; } = null!;

        [StringLength(1000)]
        public string? Description { get; set; }

        public int SortOrder { get; set; }

        public bool IsActive { get; set; } = true;

        // Navigation
        public ICollection<Question> Questions { get; set; } = new List<Question>();
        public ICollection<TaskItem> Tasks { get; set; } = new List<TaskItem>();
    }
}
