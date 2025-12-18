using DataTransferObject;
using DataTransferObject.DTO;
using DataTransferObject.Enums;
using Entities;
using Services;
using Services.Helpers;
using System;
using System.Linq;
using System.Xml.Schema;

namespace Service
{
    public class UserService : UserIService
    {
        private readonly List<User> _users;
        private readonly TenantIService _tenantService;
        public UserService(bool Init = true) 
        {
            _users = new List<User>();
            _tenantService = new TenantService(true);
            if (Init)
            {
                _users.AddRange(new List<User>()
                {
                new User()
                {
                    UserID =  Guid.Parse("478C2DD3-008D-4060-8E3C-89AF89C453FC"),
                    UserName = "JohnW",
                    FirstName = "John",
                    LastName = "Wick",
                    TenantID = Guid.Parse("935AC7B5-7B7B-4367-9193-D298467994FD"),
                    Address = "Abc road",
                    DateOfBirth = DateTime.Parse("2000-01-01"),
                    Email = "abc@example.com",
                    UserRole = "User",
                    emailVerified = true
                }, new User()
                {
                    UserID = Guid.Parse("3e4d2d4e-0b6e-4e2c-8c7c-8b9c8f1a12a1"),
                    UserName = "SarahK",
                    FirstName = "Sarah",
                    LastName = "Klein",
                    TenantID = Guid.Parse("935AC7B5-7B7B-4367-9193-D298467994FD"),
                    Address = "12 Maple Street",
                    DateOfBirth = DateTime.Parse("1995-06-14"),
                    Email = "sarah.klein@example.com",
                    UserRole = "User",
                    emailVerified = true
                },
                new User()
                {
                    UserID = Guid.Parse("a91b0c3d-8d5f-4c42-9b73-14b8fa4d51e2"),
                    UserName = "MarkT",
                    FirstName = "Mark",
                    LastName = "Thompson",
                    TenantID = Guid.Parse("935AC7B5-7B7B-4367-9193-D298467994FD"),
                    Address = "88 King Road",
                    DateOfBirth = DateTime.Parse("1988-11-02"),
                    Email = "mark.thompson@example.com",
                    UserRole = "Admin",
                    emailVerified = true
                },
                new User()
                {
                    UserID = Guid.Parse("c2d7f4a9-6b12-4b6d-9c6a-2a91f0e9c3b4"),
                    UserName = "EmilyR",
                    FirstName = "Emily",
                    LastName = "Reed",
                    TenantID = Guid.Parse("1E83383A-8816-4DF8-A6D3-6C336CEF4CC4"),
                    Address = "5 Oak Avenue",
                    DateOfBirth = DateTime.Parse("1999-03-27"),
                    Email = "emily.reed@example.com",
                    UserRole = "User",
                    emailVerified = false
                },
                new User()
                {
                    UserID = Guid.Parse("6bfe8a10-4c9a-4a73-9c84-3b41d7f91c2d"),
                    UserName = "DanielH",
                    FirstName = "Daniel",
                    LastName = "Harris",
                    TenantID = Guid.Parse("1E83383A-8816-4DF8-A6D3-6C336CEF4CC4"),
                    Address = "44 Sunset Blvd",
                    DateOfBirth = DateTime.Parse("1992-09-18"),
                    Email = "daniel.harris@example.com",
                    UserRole = "User",
                    emailVerified = true
                },
                new User()
                {
                    UserID = Guid.Parse("d71e4b22-9f73-4bcb-a7b6-5d13a1f2c4e9"),
                    UserName = "OliviaM",
                    FirstName = "Olivia",
                    LastName = "Moore",
                    TenantID = Guid.Parse("CBB21EA5-9C63-4E6E-9F02-37918E3BDBF2"),
                    Address = "77 River Lane",
                    DateOfBirth = DateTime.Parse("2001-12-05"),
                    Email = "olivia.moore@example.com",
                    UserRole = "User",
                    emailVerified = false
                },
                new User()
                {
                    UserID = Guid.Parse("4e3a8b1c-9f65-42f0-b9a3-1e9a72c9a0d8"),
                    UserName = "JamesL",
                    FirstName = "James",
                    LastName = "Lawson",
                    TenantID = Guid.Parse("CBB21EA5-9C63-4E6E-9F02-37918E3BDBF2"),
                    Address = "19 Hillcrest Drive",
                    DateOfBirth = DateTime.Parse("1985-01-21"),
                    Email = "james.lawson@example.com",
                    UserRole = "Admin",
                    emailVerified = true
                },
                new User()
                {
                    UserID = Guid.Parse("b83f91a6-0d0f-4f4e-9a7b-92a3d3c8f1e5"),
                    UserName = "ChloeB",
                    FirstName = "Chloe",
                    LastName = "Brown",
                    TenantID = Guid.Parse("2431659E-879F-4EEB-B172-81C6776B686B"),
                    Address = "102 Garden Way",
                    DateOfBirth = DateTime.Parse("1997-07-09"),
                    Email = "chloe.brown@example.com",
                    UserRole = "User",
                    emailVerified = true
                },
                new User()
                {
                    UserID = Guid.Parse("1f9c3a62-8e9b-4d1c-9f8c-7b5e3a2c91d4"),
                    UserName = "KevinP",
                    FirstName = "Kevin",
                    LastName = "Parker",
                    TenantID = Guid.Parse("2431659E-879F-4EEB-B172-81C6776B686B"),
                    Address = "60 Lake View Rd",
                    DateOfBirth = DateTime.Parse("1990-04-30"),
                    Email = "kevin.parker@example.com",
                    UserRole = "User",
                    emailVerified = true
                },
                new User()
                {
                    UserID = Guid.Parse("92a1f1c7-7c1e-4b91-8f5d-0d8bfa6e3a21"),
                    UserName = "LauraS",
                    FirstName = "Laura",
                    LastName = "Stevens",
                    TenantID = Guid.Parse("427E3E9C-1254-48D0-B0C1-50FC4BB936C8"),
                    Address = "8 Cedar Court",
                    DateOfBirth = DateTime.Parse("1993-10-11"),
                    Email = "laura.stevens@example.com",
                    UserRole = "User",
                    emailVerified = false
                } });
            }
        }
        private UserResponse convertUserIntoUserResponse(User user)
        {
            UserResponse userResponse = user.ToPersonResponse();
            userResponse.TenantName = _tenantService.GetTenantByID(user.TenantID)?.TenantName;
            return userResponse;
        }
        public UserResponse AddUser(UserAddRequest? request)
        {
            if(request == null)
            {
               throw new ArgumentNullException(nameof(request), "UserAddRequest cannot be null.");
            }
            ValidationHelper.ModelValidation(request);
            User user = request.toUser();
            user.UserID = Guid.NewGuid();
            _users.Add(user);
            return convertUserIntoUserResponse(user);

        }
        public List<UserResponse> ListAllUser()
        {
            return _users.Select(temp => convertUserIntoUserResponse(temp)).ToList();
        }

        public UserResponse? GetUserByID(Guid? UserID)
        {

            if (UserID == null)
                return null;

            User? user_response_from_list = _users.FirstOrDefault(temp => temp.UserID == UserID);

            if (user_response_from_list == null)
                return null;

            return user_response_from_list.ToPersonResponse();
        }

        public List<UserResponse> SearchUserBy(string searchBy, string? SearchString)
        {
            List<UserResponse> result = ListAllUser();
            List<UserResponse> MatchedResult = result;
            if (string.IsNullOrEmpty(searchBy) || string.IsNullOrEmpty(SearchString))
                return MatchedResult;
            switch (searchBy)
            {
                case nameof(UserResponse.UserName):
                    MatchedResult = result.Where(temp =>
                    (!string.IsNullOrEmpty(temp.UserName) ?
                    temp.UserName.Contains(SearchString, StringComparison.OrdinalIgnoreCase) :
                    true)).ToList();
                    break;
                case nameof(UserResponse.FirstName):
                    MatchedResult = result.Where(temp =>
                    (!string.IsNullOrEmpty(temp.FirstName) ?
                    temp.FirstName.Contains(SearchString, StringComparison.OrdinalIgnoreCase) :
                    true)).ToList();
                    break;
                case nameof(UserResponse.LastName):
                    MatchedResult = result.Where(temp =>
                    (!string.IsNullOrEmpty(temp.LastName) ?
                    temp.LastName.Contains(SearchString, StringComparison.OrdinalIgnoreCase) :
                    true)).ToList();
                    break;
                case nameof(UserResponse.Email):
                    MatchedResult = result.Where(temp =>
                    (!string.IsNullOrEmpty(temp.Email) ?
                    temp.Email.Contains(SearchString, StringComparison.OrdinalIgnoreCase) :
                    true)).ToList();
                    break;
                case nameof(UserResponse.UserRole):
                    MatchedResult = result.Where(temp =>
                    (!string.IsNullOrEmpty(temp.UserRole) ?
                    temp.UserRole.Contains(SearchString, StringComparison.OrdinalIgnoreCase) :
                    true)).ToList();
                    break;
                case nameof(UserResponse.TenantName):
                    MatchedResult = result.Where(temp =>
                    (!string.IsNullOrEmpty(temp.TenantName) ?
                    temp.TenantName.Contains(SearchString, StringComparison.OrdinalIgnoreCase) :
                    true)).ToList();
                    break;
                case nameof(UserResponse.Address):
                    MatchedResult = result.Where(temp =>
                    (!string.IsNullOrEmpty(temp.Address) ?
                    temp.Address.Contains(SearchString, StringComparison.OrdinalIgnoreCase) :
                    true)).ToList();
                    break;
                case nameof(UserResponse.DateOfBirth):
                    MatchedResult = result.Where(temp => 
                    (temp.DateOfBirth != null) ? 
                    temp.DateOfBirth.Value.ToString("dd MMMM yyyy").Contains(SearchString, StringComparison.OrdinalIgnoreCase) : true).ToList();
                    break;

                default:
                    MatchedResult = result;
                    break;
            }
            return MatchedResult;
        }

        public List<UserResponse> GetSortedUser(List<UserResponse> allusers, string SortBy, SortOrderOption sortorder)
        {
            if(string.IsNullOrEmpty(SortBy))
            {
                return allusers;
            }
            List<UserResponse> sortedList = (SortBy, sortorder) 
            switch
            {
                (nameof(UserResponse.UserName), SortOrderOption.ASC) => allusers.OrderBy(temp => temp.UserName, StringComparer.OrdinalIgnoreCase).ToList(),
                (nameof(UserResponse.UserName), SortOrderOption.DESC) => allusers.OrderByDescending(temp => temp.UserName, StringComparer.OrdinalIgnoreCase).ToList(),
                (nameof(UserResponse.FirstName), SortOrderOption.ASC) => allusers.OrderBy(temp => temp.FirstName, StringComparer.OrdinalIgnoreCase).ToList(),
                (nameof(UserResponse.FirstName), SortOrderOption.DESC) => allusers.OrderByDescending(temp => temp.FirstName, StringComparer.OrdinalIgnoreCase).ToList(),
                (nameof(UserResponse.LastName), SortOrderOption.ASC) => allusers.OrderBy(temp => temp.LastName, StringComparer.OrdinalIgnoreCase).ToList(),
                (nameof(UserResponse.LastName), SortOrderOption.DESC) => allusers.OrderByDescending(temp => temp.LastName, StringComparer.OrdinalIgnoreCase).ToList(),
                (nameof(UserResponse.Email), SortOrderOption.ASC) => allusers.OrderBy(temp => temp.Email, StringComparer.OrdinalIgnoreCase).ToList(),
                (nameof(UserResponse.Email), SortOrderOption.DESC) => allusers.OrderByDescending(temp => temp.Email, StringComparer.OrdinalIgnoreCase).ToList(),
                (nameof(UserResponse.DateOfBirth), SortOrderOption.ASC) => allusers.OrderBy(temp => temp.DateOfBirth).ToList(),
                (nameof(UserResponse.DateOfBirth), SortOrderOption.DESC) => allusers.OrderByDescending(temp => temp.DateOfBirth).ToList(),
                (nameof(UserResponse.Address), SortOrderOption.ASC) => allusers.OrderBy(temp => temp.Address, StringComparer.OrdinalIgnoreCase).ToList(),
                (nameof(UserResponse.Address), SortOrderOption.DESC) => allusers.OrderByDescending(temp => temp.Address, StringComparer.OrdinalIgnoreCase).ToList(),
                _ => allusers
            };
            return sortedList;
        }

        public UserResponse UpdateUser(UserUpdateRequest? request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(User));

            //validation
            ValidationHelper.ModelValidation(request);

            //get matching person object to update
            User? matchingUser = _users.FirstOrDefault(temp => temp.UserID == request.UserId);
            if (matchingUser == null)
            {
                throw new ArgumentException("Given person id doesn't exist");
            }

            //update all details
            // Only update if supplied
            if (!string.IsNullOrWhiteSpace(request.UserName))
                matchingUser.UserName = request.UserName;

            if (!string.IsNullOrWhiteSpace(request.FirstName))
                matchingUser.FirstName = request.FirstName;

            if (!string.IsNullOrWhiteSpace(request.LastName))
                matchingUser.LastName = request.LastName;

            if (!string.IsNullOrWhiteSpace(request.Email))
                matchingUser.Email = request.Email;

            if (request.DateOfBirth.HasValue)
                matchingUser.DateOfBirth = request.DateOfBirth;

            if (request.UserRole.HasValue)
                matchingUser.UserRole = request.UserRole.Value.ToString();

            if (!string.IsNullOrWhiteSpace(request.Address))
                matchingUser.Address = request.Address;
            
            if (request.emailVerified.HasValue)
                matchingUser.emailVerified = request.emailVerified.Value;
            
            if (request.TenantID.HasValue)
                matchingUser.TenantID = request.TenantID;
            else
                matchingUser.TenantID = null;


                return matchingUser.ToPersonResponse();
        }

        public bool DeleteUser(Guid? UserID)
        {
            if (UserID == null)
            {
                throw new ArgumentNullException(nameof(UserID));
            }

            User? user = _users.FirstOrDefault(temp => temp.UserID == UserID);
            if (user == null)
                return false;

            _users.RemoveAll(temp => temp.UserID == UserID);

            return true;
        }
    }
}
