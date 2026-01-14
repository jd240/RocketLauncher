using Azure.Core;
using DataTransferObject;
using DataTransferObject.DTO;
using DataTransferObject.Enums;
using Entities;
using Microsoft.EntityFrameworkCore;
using Services;
using Services.Helpers;
using System;
using System.Linq;
using System.Xml.Schema;

namespace Service
{
    public class UserService : UserIService
    {
        private readonly UserDbcontext _users;
        private readonly TenantIService _tenantService;
        public UserService(UserDbcontext userDbcontext, bool Init = true) 
        {
            _users = userDbcontext;
            _tenantService = new TenantService(false);
            //if (Init)
            //{
            //    _users.AddRange(new List<User>()
            //    {
            //    new User()
            //    {
            //        UserID =  Guid.Parse("478C2DD3-008D-4060-8E3C-89AF89C453FC"),
            //        UserName = "JohnW",
            //        FirstName = "John",
            //        LastName = "Wick",
            //        AssociatedTenantID = Guid.Parse("935AC7B5-7B7B-4367-9193-D298467994FD"),
            //        Address = "Abc road",
            //        DateOfBirth = DateTime.Parse("2000-01-01"),
            //        Email = "abc@example.com",
            //        UserRole = "User",
            //        emailVerified = true
            //    }, new User()
            //    {
            //        UserID = Guid.Parse("3e4d2d4e-0b6e-4e2c-8c7c-8b9c8f1a12a1"),
            //        UserName = "SarahK",
            //        FirstName = "Sarah",
            //        LastName = "Klein",
            //        AssociatedTenantID = Guid.Parse("935AC7B5-7B7B-4367-9193-D298467994FD"),
            //        Address = "12 Maple Street",
            //        DateOfBirth = DateTime.Parse("1995-06-14"),
            //        Email = "sarah.klein@example.com",
            //        UserRole = "User",
            //        emailVerified = true
            //    },
            //    new User()
            //    {
            //        UserID = Guid.Parse("a91b0c3d-8d5f-4c42-9b73-14b8fa4d51e2"),
            //        UserName = "MarkT",
            //        FirstName = "Mark",
            //        LastName = "Thompson",
            //        AssociatedTenantID = Guid.Parse("935AC7B5-7B7B-4367-9193-D298467994FD"),
            //        Address = "88 King Road",
            //        DateOfBirth = DateTime.Parse("1988-11-02"),
            //        Email = "mark.thompson@example.com",
            //        UserRole = "Admin",
            //        emailVerified = true
            //    },
            //    new User()
            //    {
            //        UserID = Guid.Parse("c2d7f4a9-6b12-4b6d-9c6a-2a91f0e9c3b4"),
            //        UserName = "EmilyR",
            //        FirstName = "Emily",
            //        LastName = "Reed",
            //        AssociatedTenantID = Guid.Parse("1E83383A-8816-4DF8-A6D3-6C336CEF4CC4"),
            //        Address = "5 Oak Avenue",
            //        DateOfBirth = DateTime.Parse("1999-03-27"),
            //        Email = "emily.reed@example.com",
            //        UserRole = "User",
            //        emailVerified = false
            //    },
            //    new User()
            //    {
            //        UserID = Guid.Parse("6bfe8a10-4c9a-4a73-9c84-3b41d7f91c2d"),
            //        UserName = "DanielH",
            //        FirstName = "Daniel",
            //        LastName = "Harris",
            //        AssociatedTenantID = Guid.Parse("1E83383A-8816-4DF8-A6D3-6C336CEF4CC4"),
            //        Address = "44 Sunset Blvd",
            //        DateOfBirth = DateTime.Parse("1992-09-18"),
            //        Email = "daniel.harris@example.com",
            //        UserRole = "User",
            //        emailVerified = true
            //    },
            //    new User()
            //    {
            //        UserID = Guid.Parse("d71e4b22-9f73-4bcb-a7b6-5d13a1f2c4e9"),
            //        UserName = "OliviaM",
            //        FirstName = "Olivia",
            //        LastName = "Moore",
            //        AssociatedTenantID = Guid.Parse("CBB21EA5-9C63-4E6E-9F02-37918E3BDBF2"),
            //        Address = "77 River Lane",
            //        DateOfBirth = DateTime.Parse("2001-12-05"),
            //        Email = "olivia.moore@example.com",
            //        UserRole = "User",
            //        emailVerified = false
            //    },
            //    new User()
            //    {
            //        UserID = Guid.Parse("4e3a8b1c-9f65-42f0-b9a3-1e9a72c9a0d8"),
            //        UserName = "JamesL",
            //        FirstName = "James",
            //        LastName = "Lawson",
            //        AssociatedTenantID = Guid.Parse("CBB21EA5-9C63-4E6E-9F02-37918E3BDBF2"),
            //        Address = "19 Hillcrest Drive",
            //        DateOfBirth = DateTime.Parse("1985-01-21"),
            //        Email = "james.lawson@example.com",
            //        UserRole = "Admin",
            //        emailVerified = true
            //    },
            //    new User()
            //    {
            //        UserID = Guid.Parse("b83f91a6-0d0f-4f4e-9a7b-92a3d3c8f1e5"),
            //        UserName = "ChloeB",
            //        FirstName = "Chloe",
            //        LastName = "Brown",
            //        AssociatedTenantID = Guid.Parse("2431659E-879F-4EEB-B172-81C6776B686B"),
            //        Address = "102 Garden Way",
            //        DateOfBirth = DateTime.Parse("1997-07-09"),
            //        Email = "chloe.brown@example.com",
            //        UserRole = "User",
            //        emailVerified = true
            //    },
            //    new User()
            //    {
            //        UserID = Guid.Parse("1f9c3a62-8e9b-4d1c-9f8c-7b5e3a2c91d4"),
            //        UserName = "KevinP",
            //        FirstName = "Kevin",
            //        LastName = "Parker",
            //        AssociatedTenantID = Guid.Parse("2431659E-879F-4EEB-B172-81C6776B686B"),
            //        Address = "60 Lake View Rd",
            //        DateOfBirth = DateTime.Parse("1990-04-30"),
            //        Email = "kevin.parker@example.com",
            //        UserRole = "User",
            //        emailVerified = true
            //    },
            //    new User()
            //    {
            //        UserID = Guid.Parse("92a1f1c7-7c1e-4b91-8f5d-0d8bfa6e3a21"),
            //        UserName = "LauraS",
            //        FirstName = "Laura",
            //        LastName = "Stevens",
            //        AssociatedTenantID = Guid.Parse("427E3E9C-1254-48D0-B0C1-50FC4BB936C8"),
            //        Address = "8 Cedar Court",
            //        DateOfBirth = DateTime.Parse("1993-10-11"),
            //        Email = "laura.stevens@example.com",
            //        UserRole = "User",
            //        emailVerified = false
            //    },
            //     new WhiteLabelTenant()
            //    {
            //        UserID = Guid.Parse("6E50AA82-9D11-4C6C-8E14-86AC206D2A49"),
            //        TenantID = Guid.Parse("935AC7B5-7B7B-4367-9193-D298467994FD"),
            //        UserName = "ACounsult",
            //        TenantName = "Tenant A",
            //        UserRole = "Tenant"
            //    },
            //    new WhiteLabelTenant()
            //    {
            //        UserID = Guid.Parse("D6E656D5-9F1A-4458-9F5E-0E641D0702DB"),
            //        TenantID = Guid.Parse("1E83383A-8816-4DF8-A6D3-6C336CEF4CC4"),
            //        UserName = "B law",
            //        TenantName = "Tenant B",
            //        UserRole = "Tenant"
            //    },
            //    new WhiteLabelTenant()
            //    {
            //        UserID = Guid.Parse("03146D45-5859-44FD-856F-81BF6BA46C88"),
            //        TenantID = Guid.Parse("CBB21EA5-9C63-4E6E-9F02-37918E3BDBF2"),
            //        UserName = "C wholeSale",
            //        TenantName = "Tenant C",
            //        UserRole = "Tenant"
            //    },
            //    new WhiteLabelTenant()
            //    {
            //        UserID = Guid.Parse("5D6A9517-211D-4A32-88C4-4FD2307781DF"),
            //        TenantID = Guid.Parse("2431659E-879F-4EEB-B172-81C6776B686B"),
            //        UserName = "D Web Dev",
            //        TenantName = "Tenant D",
            //        UserRole = "Tenant"
            //    },
            //    new WhiteLabelTenant()
            //    {
            //        UserID = Guid.Parse("D4B3C794-A3BA-4274-8609-11B798B15790"),
            //        TenantID = Guid.Parse("427E3E9C-1254-48D0-B0C1-50FC4BB936C8"),
            //        UserName = "ElemtalCore",
            //        TenantName = "Tenant E",
            //        UserRole = "Tenant"
            //    }
            //    });
            //}
        }
        private UserResponse convertUserIntoUserResponse(User user)
        {
            UserResponse userResponse = user.ToPersonResponse();
            userResponse.AssociatedTenantName = GetUserByTenantID(user.AssociatedTenantID)?.TenantName;
            return userResponse;
        }
        private TenantResponse convertTenantIntoUserResponse(WhiteLabelTenant user)
        {
            TenantResponse tenantResponse = user.ToTenantResponse();
            return tenantResponse;
        }
        public UserResponse AddUser(UserAddRequest? Userrequest)
        {
            if(Userrequest == null)
            {
               throw new ArgumentNullException(nameof(Userrequest), "UserAddRequest cannot be null.");
            }
            ValidationHelper.ModelValidation(Userrequest);
            User user = Userrequest.toUser();
            user.UserID = Guid.NewGuid();
            _users.Users.Add(user);
            _users.SaveChanges();
            return convertUserIntoUserResponse(user);
        }
        public TenantResponse AddTenant(TenantAddRequest? Tenantrequest)
        {
            if (Tenantrequest == null)
            {
                throw new ArgumentNullException(nameof(Tenantrequest), "Tenantrequest cannot be null.");
            }
            ValidationHelper.ModelValidation(Tenantrequest);
            WhiteLabelTenant user = Tenantrequest.ToTenant();
            user.TenantID = Guid.NewGuid();
            _users.WhiteLabelTenants.Add(user);
            _users.SaveChanges();
            return convertTenantIntoUserResponse(user);
        }
        public List<UserResponse> ListAllUser()
        {
            var test = _users.Users.Include("AssociatedTenant").ToList();
            // Get regular users
            var regularUsers = _users.Users.ToList().Select(u => convertUserIntoUserResponse(u))
           .ToList();

            return regularUsers;
        }
        public List<TenantResponse> ListAllTenantUsers()
        {
            return _users.WhiteLabelTenants.ToList()
                .Select(temp => convertTenantIntoUserResponse(temp))
                .ToList();
        }

        public UserResponse? GetUserByID(Guid? UserID)
        {
            if (UserID == null)
                return null;
            return _users.Users
                .FirstOrDefault(u => u.UserID == UserID)
                ?.ToPersonResponse();
        }
        public TenantResponse? GetUserByTenantID(Guid? TenantID)
        {
            if (TenantID == null)
                return null;

            var tenant = _users.WhiteLabelTenants.FirstOrDefault(temp => temp.TenantID == TenantID);

            return tenant?.ToTenantResponse();
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
                case nameof(UserResponse.AssociatedTenantName):
                    MatchedResult = result.Where(temp =>
                    (!string.IsNullOrEmpty(temp.AssociatedTenantName) ?
                    temp.AssociatedTenantName.Contains(SearchString, StringComparison.OrdinalIgnoreCase) :
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
                throw new ArgumentNullException(nameof(request));

            // Validation
            ValidationHelper.ModelValidation(request);

            // Get matching user object to update based on role

           User? matchingUser = _users.Users.FirstOrDefault(temp => temp.UserID == request.UserId);
                if (matchingUser == null)
                {
                    throw new ArgumentException("Given id doesn't exist");
                }

                // Update user details
                UpdateCommonFields(matchingUser, request);
                _users.SaveChanges();
            return matchingUser.ToPersonResponse();
        }

        private void UpdateCommonFields(User user, UserUpdateRequest request)
        {
            if (!string.IsNullOrWhiteSpace(request.UserName))
                user.UserName = request.UserName;
            if (!string.IsNullOrWhiteSpace(request.FirstName))
                user.FirstName = request.FirstName;
            if (!string.IsNullOrWhiteSpace(request.LastName))
                user.LastName = request.LastName;
            if (!string.IsNullOrWhiteSpace(request.Email))
                user.Email = request.Email;
            if (request.DateOfBirth.HasValue)
                user.DateOfBirth = request.DateOfBirth;
            if (request.UserRole.HasValue)
                user.UserRole = request.UserRole.Value.ToString();
            if (!string.IsNullOrWhiteSpace(request.Address))
                user.Address = request.Address;
            if (request.emailVerified.HasValue)
                user.emailVerified = request.emailVerified.Value;

            if (request.AssociatedTenantID.HasValue)
                user.AssociatedTenantID = request.AssociatedTenantID;
            else
                user.AssociatedTenantID = null;
        }

        public bool DeleteUser(Guid? UserID)
        {
            if (UserID == null)
            {
                throw new ArgumentNullException(nameof(UserID));
            }
            var user = _users.Users.FirstOrDefault(u => u.UserID == UserID);
            if (user == null)
                return false;

            _users.Users.Remove(user);
            _users.SaveChanges();
            return true;
        }
        public bool DeleteTenant(Guid? TenantId)
        {
            if (TenantId == null)
            {
                throw new ArgumentNullException(nameof(TenantId));
            }

            var tenant = _users.WhiteLabelTenants.FirstOrDefault(t => t.TenantID == TenantId);
            if (tenant != null)
            {
                _users.WhiteLabelTenants.Remove(tenant);
                _users.SaveChanges();
                return true;
            }
            else
                return false;
        }

        public TenantResponse UpdateTenant(TenantUpdateRequest? tenantRequest)
        {
            if (tenantRequest == null)
                throw new ArgumentNullException(nameof(tenantRequest));

            // Validation (includes RequireAtLeastOneAdditionalField)
            ValidationHelper.ModelValidation(tenantRequest);

            WhiteLabelTenant? matchingTenant =
                _users.WhiteLabelTenants.FirstOrDefault(t => t.TenantID == tenantRequest.TenantID);

            if (matchingTenant == null)
                throw new ArgumentException("Given id doesn't exist");

            // PATCH update: only apply provided fields
            if (tenantRequest.TenantName != null)
                matchingTenant.TenantName = tenantRequest.TenantName;

            if (tenantRequest.Slug != null)
            {
                bool slugExists = _users.WhiteLabelTenants
                    .Any(t => t.Slug == tenantRequest.Slug && t.TenantID != tenantRequest.TenantID);

                if (slugExists)
                    throw new ArgumentException("Slug already exists");
            }

            if (tenantRequest.ContactEmail != null)
                matchingTenant.ContactEmail = tenantRequest.ContactEmail;

            if (tenantRequest.AddressLine1 != null)
                matchingTenant.AddressLine1 = tenantRequest.AddressLine1;

            if (tenantRequest.IsActive.HasValue)
                matchingTenant.IsActive = tenantRequest.IsActive.Value;

            _users.SaveChanges();
            return matchingTenant.ToTenantResponse();
        }
    }
}
