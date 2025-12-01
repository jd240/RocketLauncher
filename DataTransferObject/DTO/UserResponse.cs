using Entities;
using System;
using System.Reflection;


namespace DataTransferObject.DTO
{
    public class UserResponse
    {
        public Guid UserID { get; set; }
        public string? UserName { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public bool emailVerified { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Role { get; set; }
        public string? Address { get; set; }
        public override bool Equals(object? obj)
        {
            if (obj == null) return false;

            if (obj.GetType() != typeof(UserResponse)) return false;

            UserResponse person = (UserResponse)obj;
            return UserID == person.UserID && UserName == person.UserName && FirstName == person.FirstName && LastName == person.LastName 
                && Email == person.Email && emailVerified == person.emailVerified && DateOfBirth == person.DateOfBirth && Role == person.Role 
                && Address == person.Address;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
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
                Role = person.Role,
                Address = person.Address
            };
        }
    }
}
