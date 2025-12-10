using DataTransferObject.Enums;
using Entities;
using System.ComponentModel.DataAnnotations;


namespace DataTransferObject.DTO
{
    [RequireAtLeastOneAdditionalField]
    public class UserUpdateRequest
    {
        [Required (ErrorMessage = "Please Supply the User ID you wish to update")]
        public Guid UserId { get; set; }
        public string? UserName { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        [EmailAddress(ErrorMessage = "The Provided Email Address was not valid!")]
        public string? Email { get; set; }
        public bool? emailVerified { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public Role? UserRole { get; set; }
        public Guid? TenantID { get; set; }
        public string? Address { get; set; }
        public User toUser()
        {
            return new User()
            {
                UserID = UserId,
                UserName = UserName,
                FirstName = FirstName,
                LastName = LastName,
                Email = Email,
                emailVerified = emailVerified,
                DateOfBirth = DateOfBirth,
                UserRole = UserRole.ToString(),
                Address = Address,
                TenantID = TenantID
            };
        }
    }
}
