using Entities;
using System;
using System.Diagnostics.Metrics;


namespace DataTransferObject.DTO
{
    public class TenantAddRequest
    {
        public string? TenantName { get; set; }
        public WhiteLabelTenant ToTenant()
        {
            return new WhiteLabelTenant() { TenantName = TenantName};
        }
    }
}
