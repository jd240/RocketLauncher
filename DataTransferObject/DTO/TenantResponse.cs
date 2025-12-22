using Entities;
using System;

namespace DataTransferObject.DTO
{
    /// <summary>
    /// DTO class that is used as return type for most of TenantService methods
    /// </summary
    public class TenantResponse
    {
        public Guid TenantID { get; set; }
        public string? TenantName { get; set; }
        public override bool Equals(object? obj)
        {
            if (obj == null)
            {
                return false;
            }

            if (obj.GetType() != typeof(TenantResponse))
            {
                return false;
            }
            TenantResponse tenant_to_compare = (TenantResponse)obj;

            return TenantID == tenant_to_compare.TenantID && TenantName == tenant_to_compare.TenantName;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
    public static class TenantExtensions
    {
        public static TenantResponse ToTenantResponse2(this WhiteLabelTenant Tenant)
        {
            return new TenantResponse() { TenantID = Tenant.TenantID, TenantName = Tenant.TenantName };
        }
    }
}
