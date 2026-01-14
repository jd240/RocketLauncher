using Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace DataTransferObject.DTO
{
    [RequireAtLeastOneAdditionalField]
    public class TenantUpdateRequest
    {
        [Required(ErrorMessage = "Please supply the Tenant ID you wish to update")]
        public Guid TenantID { get; set; }

        // Business / organisation identity
        [StringLength(100)]
        public string? TenantName { get; set; }

        [StringLength(50)]
        public string? Slug { get; set; }

        // Business contact
        [EmailAddress(ErrorMessage = "The provided contact email address was not valid!")]
        public string? ContactEmail { get; set; }

        // Business address
        [StringLength(200)]
        public string? AddressLine1 { get; set; }

        // Tenant lifecycle
        public bool? IsActive { get; set; }

        public WhiteLabelTenant ToTenant()
        {
            return new WhiteLabelTenant
            {
                TenantID = TenantID,
                TenantName = TenantName,
                Slug = Slug,
                ContactEmail = ContactEmail,
                AddressLine1 = AddressLine1,
                IsActive = IsActive ?? true
            };
        }
    }
}

