using System;
using System.ComponentModel.DataAnnotations;

namespace Entities
{
    /// <summary>
    /// Domain Model for Country
    /// </summary>
    public class WhiteLabelTenant: User   
    {
        [Key]
        public Guid TenantID { get; set; }
        [StringLength(100)]
        public string? TenantName { get; set; }
    }
}
