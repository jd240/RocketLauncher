using System.ComponentModel.DataAnnotations;

namespace Entities
{
    public class User
    {
        public Guid UserID { get; set; }
        public string? UserName { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public bool? emailVerified { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? UserRole { get; set; }
        public string? Address { get; set; }
        public Guid? AssociatedTenantID { get; set; }

    }
}
