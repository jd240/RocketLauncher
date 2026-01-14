using Entities;
using System;
namespace DataTransferObject.DTO
{
    /// <summary>
    /// DTO returned by TenantService methods.
    /// Represents a white-label tenant (organisation),
    /// </summary>
    public class TenantResponse
    {
        public Guid TenantID { get; set; }

        // Organisation identity
        public string TenantName { get; set; } = null!;
        public string Slug { get; set; } = null!;

        // Business contact (not login)
        public string? ContactEmail { get; set; }

        // Business address
        public string? AddressLine1 { get; set; }

        // Tenant lifecycle
        public bool IsActive { get; set; }

        public DateTime CreatedAtUtc { get; set; }

        // Equality: tenants are uniquely identified by TenantID
        public override bool Equals(object? obj)
        {
            if (obj is not TenantResponse other)
                return false;

            return TenantID == other.TenantID;
        }

        public override int GetHashCode()
        {
            return TenantID.GetHashCode();
        }
    }
}
namespace DataTransferObject.DTO
{
    public static class TenantExtensions
    {
        public static TenantResponse ToTenantResponse(this WhiteLabelTenant tenant)
        {
            return new TenantResponse
            {
                TenantID = tenant.TenantID,
                TenantName = tenant.TenantName,
                Slug = tenant.Slug,
                ContactEmail = tenant.ContactEmail,
                AddressLine1 = tenant.AddressLine1,
                IsActive = tenant.IsActive,
                CreatedAtUtc = tenant.CreatedAtUtc
            };
        }
    }
}
