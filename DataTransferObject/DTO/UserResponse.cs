using DataTransferObject.Enums;
using Entities;
using System;
using System.Reflection;
using System.Xml.Linq;


namespace DataTransferObject.DTO
{
    public class UserResponse
    {
        public Guid UserID { get; set; }
        public Guid? TenantID { get; set; }
        public string? UserName { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? TenantName { get; set; }
        public string? Email { get; set; }
        public bool? emailVerified { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? UserRole { get; set; }
        public Guid? AssociatedTenantID { get; set; }
        public string? Address { get; set; }
        public string? AssociatedTenantName { get; set; }
        public override bool Equals(object? obj)
        {
            if (obj == null) return false;

            if (obj.GetType() != typeof(UserResponse)) return false;

            UserResponse person = (UserResponse)obj;
            return UserID == person.UserID && UserName == person.UserName && FirstName == person.FirstName && LastName == person.LastName 
                && Email == person.Email && emailVerified == person.emailVerified && DateOfBirth == person.DateOfBirth && UserRole == person.UserRole 
                && Address == person.Address && AssociatedTenantID == person.AssociatedTenantID;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public UserUpdateRequest toUserUpdateRequest()
        {
            return new UserUpdateRequest()
            {
                UserId = UserID,
                TenantID = TenantID,
                UserName = UserName,
                FirstName = FirstName,
                LastName = LastName,
                TenantName = TenantName,
                Email = Email,
                emailVerified = emailVerified,
                DateOfBirth = DateOfBirth,
                UserRole = Enum.Parse<Role>(UserRole, true),
                Address = Address,
                AssociatedTenantID = AssociatedTenantID
            };
        }
    }

    public static class PersonExtensions
    {
        /// <summary>
        /// An extension method to convert an object of User class into UserResponse class
        /// </summary>
        /// <param name="person">The User object to convert</param>
        /// /// <returns>Returns the converted UserResponse object</returns>
        public static UserResponse ToPersonResponse(this User person)
        {
            //person => convert => PersonResponse
            return new UserResponse()
            {
                UserID = person.UserID,
                UserName = person.UserName,
                FirstName = person.FirstName,
                LastName = person.LastName,
                Email = person.Email,
                emailVerified = person.emailVerified,
                DateOfBirth = person.DateOfBirth,
                UserRole = person.UserRole,
                Address = person.Address,
                AssociatedTenantID = person.AssociatedTenantID
            };
        }
    }
}
