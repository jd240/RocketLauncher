using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;

namespace Entities
{
    /// <summary>
    /// Domain Model for Country
    /// </summary>
    public class WhiteLabelTenant
    {
        [Key]
        public Guid TenantID { get; set; }

        // Business / organisation identity
        [Required]
        [StringLength(100)]
        public string TenantName { get; set; } = null!;

        // URL-safe identifier for white-labeling
        [Required]
        [StringLength(50)]
        public string Slug { get; set; } = null!;

        // Business contact (NOT login)
        [EmailAddress]
        [StringLength(100)]
        public string? ContactEmail { get; set; }

        // Business address
        [StringLength(200)]
        public string? AddressLine1 { get; set; }

        // Tenant status / lifecycle
        public bool IsActive { get; set; } = true;

        public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;

        // Navigation: one tenant -> many users
        public ICollection<User> Users { get; set; } = new List<User>();
    }
}
