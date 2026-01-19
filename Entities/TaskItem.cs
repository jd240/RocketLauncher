using System;
using System.ComponentModel.DataAnnotations;

namespace Entities
{
    public class TaskItem
    {
        [Key]
        public Guid TaskItemId { get; set; }

        [Required]
        public Guid ModuleId { get; set; }

        public Module Module { get; set; } = null!;

        [Required]
        [StringLength(200)]
        public string Title { get; set; } = null!;

        [StringLength(2000)]
        public string? Description { get; set; }

        [StringLength(1000)]
        public string? LinkUrl { get; set; }

        public int SortOrder { get; set; }

        public bool IsActive { get; set; } = true;
    }
}

