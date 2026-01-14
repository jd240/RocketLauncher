using System.ComponentModel.DataAnnotations;

namespace Entities
{
    public class User
    {
        [Key]
        public Guid UserID { get; set; }
        [StringLength(50)]
        public string? UserName { get; set; }
        [StringLength(50)]
        public string? FirstName { get; set; }
        [StringLength(50)]
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public bool? emailVerified { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? UserRole { get; set; }
        [StringLength(200)]
        public string? Address { get; set; }
        public Guid? AssociatedTenantID { get; set; }
        public WhiteLabelTenant? AssociatedTenant { get; set; }

    }
}
