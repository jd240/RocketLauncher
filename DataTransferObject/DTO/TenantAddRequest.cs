using System;
using System.ComponentModel.DataAnnotations;
using Entities;

namespace DataTransferObject.DTO
{
    public class TenantAddRequest
    {
        [Required]
        [StringLength(100)]
        public string TenantName { get; set; } = null!;

        [Required]
        [StringLength(50)]
        public string Slug { get; set; } = null!;

        [EmailAddress]
        [StringLength(100)]
        public string? ContactEmail { get; set; }

        [StringLength(200)]
        public string? AddressLine1 { get; set; }

        public WhiteLabelTenant ToTenant()
        {
            return new WhiteLabelTenant
            {
                TenantName = TenantName,
                Slug = Slug,
                ContactEmail = ContactEmail,
                AddressLine1 = AddressLine1,
                IsActive = true,
                CreatedAtUtc = DateTime.UtcNow
            };
        }
    }
}
