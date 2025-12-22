using System;

namespace Entities
{
    /// <summary>
    /// Domain Model for Country
    /// </summary>
    public class WhiteLabelTenant: User   
    {
        public Guid TenantID { get; set; }
        public string? TenantName { get; set; }
    }
}
