using DataTransferObject.Enums;
using Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
namespace DataTransferObject.DTO
{
    public class UserAddRequest
    {
        [Required (ErrorMessage = "UserName Must Not Be Blanked!")]
        public string? UserName { get; set; }
        public string? FirstName { get; set; }
        public string? TenantName { get; set; }
        public string? LastName { get; set; }
        [Required(ErrorMessage = "Email Must Not Be Blanked!")]
        [EmailAddress(ErrorMessage = "The Provided Email Address was not valid!")]
        public string? Email { get; set; }
        public bool emailVerified { get; set; }
        public DateTime? DateOfBirth { get; set; }
        [Required(ErrorMessage = "Please Pick Your Role!")]
        public Role? UserRole { get; set; }
        public Guid? AssociatedTenantID { get; set; }
        public string? Address { get; set; }
        public User toUser()
        {
            return new User() { UserName = UserName, FirstName = FirstName, LastName = LastName, Email = Email, emailVerified = emailVerified, 
                DateOfBirth = DateOfBirth, UserRole = UserRole.ToString(), Address = Address, TenantID = AssociatedTenantID};
        }
    }
}