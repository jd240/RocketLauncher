using DataTransferObject.Enum;
using Entities;
using System;
using System.Collections.Generic;
using System.Reflection;
namespace DataTransferObject.DTO
{
    public class UserAddRequest
    {
        public string? UserName { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public bool emailVerified { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public Role? Role { get; set; }
        public string? Address { get; set; }
        public User toUser()
        {
            return new User() { UserName = UserName, FirstName = FirstName, LastName = LastName, Email = Email, emailVerified = emailVerified, 
                DateOfBirth = DateOfBirth, Role = Role.ToString(), Address = Address};
        }
    }
}