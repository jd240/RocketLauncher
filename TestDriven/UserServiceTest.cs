using DataTransferObject;
using DataTransferObject.DTO;
using DataTransferObject.Enums;
using Entities;
using Service;
using Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using Xunit;
namespace TestDriven
{
    public class UserServiceTest
    {
        private readonly UserIService _userService;
        private readonly TenantIService _tenantsService;
        public UserServiceTest()
        {
            //_userService = new UserService(false);
            _tenantsService = new TenantService(false);
        }
        #region AddUser
        //When we supply null value as UserAddRequest, it should throw ArgumentNullException
        [Fact]
        public void AddUser_NullPerson()
        {
            //Arrange
            UserAddRequest? UserAddRequest = null;

            //Act
            Assert.Throws<ArgumentNullException>(() =>
            {
                _userService.AddUser(UserAddRequest);
            });
        }
        //When we supply invalid email format, it should throw ArgumentException
        [Fact]
        public void AddUser_InvalidEmail()
        {
            //Arrange
            UserAddRequest? UserAddRequest = new UserAddRequest()
            {
                UserName = "Sample Username",
                FirstName = "Du",
                LastName = "Duong",
                Email = "invalid-email-format", // Invalid email
                Address = "sample address",
                UserRole = DataTransferObject.Enums.Role.User,
                DateOfBirth = DateTime.Parse("2000-01-01"),
                emailVerified = true,
                AssociatedTenantID = new Guid()
            };
            //Act
            Assert.Throws<ArgumentException>(() =>
            {
                _userService.AddUser(UserAddRequest);
            });
        }
        //When we supply null value as PersonName, it should throw ArgumentException
        [Fact]
        public void AddUser_PersonNameIsNull()
        {
            //Arrange
            UserAddRequest? UserAddRequest = new UserAddRequest() { UserName = null };

            //Act
            Assert.Throws<ArgumentException>(() =>
            {
                _userService.AddUser(UserAddRequest);
            });
        }

        //When we supply proper person details, it should insert the person into the persons list; and it should return an object of UserResponse, which includes with the newly generated person id
        [Fact]
        public void AddUser_ProperPersonDetails()
        {
            //Arrange
            UserAddRequest? UserAddRequest = new UserAddRequest()
            {
                UserName = "Sample Username...",
                FirstName = "Du",
                LastName = "Duong"
                ,
                Email = "person@example.com",
                Address = "sample address",
                UserRole = DataTransferObject.Enums.Role.User,
                DateOfBirth = DateTime.Parse("2000-01-01"),
                emailVerified = true,
                AssociatedTenantID = new Guid()
            };

            //Act
            UserResponse person_response_from_add = _userService.AddUser(UserAddRequest);

            List<UserResponse> persons_list = _userService.ListAllUser();

            //Assert
            Assert.True(person_response_from_add.UserID != Guid.Empty);

            Assert.Contains(person_response_from_add, persons_list);
        }

        #endregion
        #region GetUserByID
        [Fact]
        public void GetUserByID_UserIDIsNull()
        {
            //Arrange
            Guid? UserID = null;
            //Act
            UserResponse? user_response = _userService.GetUserByID(UserID);
            //Assert
            Assert.Null(user_response);
        }
        [Fact]
        public void GetUserByID_UserIDNotFound()
        {
            //Arrange
            Guid? UserID = Guid.NewGuid();
            //Act
            UserResponse? user_response = _userService.GetUserByID(UserID);
            //Assert
            Assert.Null(user_response);
        }
        [Fact]
        public void GetUserByID_UserIDFound()
        {
            //Arange
            TenantAddRequest tenant_request = new TenantAddRequest() { TenantName = "ABC Law School" };
            TenantResponse tenant_response = _tenantsService.AddTenant(tenant_request);

            UserAddRequest user_Request = new UserAddRequest()
            {
                UserName = "sample username 2...",
                FirstName = "Dua",
                LastName = "Myster"
    ,
                Email = "Email@example.com",
                Address = "sample address 2",
                UserRole = DataTransferObject.Enums.Role.User,
                DateOfBirth = DateTime.Parse("1999-01-01"),
                emailVerified = true,
                AssociatedTenantID = tenant_response.TenantID
            };

            UserResponse user_response_from_add = _userService.AddUser(user_Request);

            UserResponse? person_response_from_get = _userService.GetUserByID(user_response_from_add.UserID);

            //Assert
            Assert.Equal(user_response_from_add, person_response_from_get);
        }
        #endregion
        #region getAllUsers
        [Fact]
        public void ListAllUser_AddFewUsers()
        {
            TenantAddRequest tenant_request_1 = new TenantAddRequest() { TenantName = "ABC Law School" };
            TenantResponse tenant_response_1 = _tenantsService.AddTenant(tenant_request_1);
            TenantAddRequest tenant_request_2 = new TenantAddRequest() { TenantName = "DEF Law School" };
            TenantResponse tenant_response_2 = _tenantsService.AddTenant(tenant_request_2);
            UserAddRequest user_Request_1 = new UserAddRequest()
            {
                UserName = "DuaBlaster...",
                FirstName = "Dual",
                LastName = "Mystic"
    ,
                Email = "Duall@example.com",
                Address = "Dual address",
                UserRole = DataTransferObject.Enums.Role.User,
                DateOfBirth = DateTime.Parse("1998-02-02"),
                emailVerified = true,
                AssociatedTenantID = tenant_response_1.TenantID
            };
            UserAddRequest user_Request_2 = new UserAddRequest()
            {
                UserName = "Kialaster...",
                FirstName = "Kia",
                LastName = "Burger"
,
                Email = "Kia@example.com",
                Address = "Kia address",
                UserRole = DataTransferObject.Enums.Role.User,
                DateOfBirth = DateTime.Parse("2004-04-04"),
                emailVerified = true,
                AssociatedTenantID = tenant_response_2.TenantID
            };
            UserAddRequest user_Request_3 = new UserAddRequest()
            {
                UserName = "NerfBlaster...",
                FirstName = "Nerf",
                LastName = "Machine"
,
                Email = "Nerf@example.com",
                Address = "Nerf address",
                UserRole = DataTransferObject.Enums.Role.User,
                DateOfBirth = DateTime.Parse("2001-11-08"),
                emailVerified = true,
                AssociatedTenantID = tenant_response_1.TenantID
            };
            List<UserAddRequest> user_requests = new List<UserAddRequest>()
            {
                user_Request_1,
                user_Request_2,
                user_Request_3
            };
            List<UserResponse> expected_user_responses = new List<UserResponse>();
            foreach (var user_request in user_requests)
            {
                UserResponse response = _userService.AddUser(user_request);
                expected_user_responses.Add(response);
            }
            //Act
            List<UserResponse> actual_user_responses = _userService.ListAllUser();
            //Assert
            foreach (var expected_user_response in expected_user_responses)
            {
                Assert.Contains(expected_user_response, actual_user_responses);
            }
        }
        #endregion
        #region SearchUserBy
        [Fact]
        public void searchUserby_EmptySearchString()
        {
            TenantAddRequest tenant_request_1 = new TenantAddRequest() { TenantName = "ABC Law School" };
            TenantResponse tenant_response_1 = _tenantsService.AddTenant(tenant_request_1);
            TenantAddRequest tenant_request_2 = new TenantAddRequest() { TenantName = "DEF Law School" };
            TenantResponse tenant_response_2 = _tenantsService.AddTenant(tenant_request_2);
            UserAddRequest user_Request_1 = new UserAddRequest()
            {
                UserName = "DuaBlaster...",
                FirstName = "Dual",
                LastName = "Mystic"
    ,
                Email = "Duall@example.com",
                Address = "Dual address",
                UserRole = DataTransferObject.Enums.Role.User,
                DateOfBirth = DateTime.Parse("1998-02-02"),
                emailVerified = true,
                AssociatedTenantID = tenant_response_1.TenantID
            };
            UserAddRequest user_Request_2 = new UserAddRequest()
            {
                UserName = "Kialaster...",
                FirstName = "Kia",
                LastName = "Burger"
,
                Email = "Kia@example.com",
                Address = "Kia address",
                UserRole = DataTransferObject.Enums.Role.User,
                DateOfBirth = DateTime.Parse("2004-04-04"),
                emailVerified = true,
                AssociatedTenantID = tenant_response_2.TenantID
            };
            UserAddRequest user_Request_3 = new UserAddRequest()
            {
                UserName = "NerfBlaster...",
                FirstName = "Nerf",
                LastName = "Machine"
,
                Email = "Nerf@example.com",
                Address = "Nerf address",
                UserRole = DataTransferObject.Enums.Role.User,
                DateOfBirth = DateTime.Parse("2001-11-08"),
                emailVerified = true,
                AssociatedTenantID = tenant_response_1.TenantID
            };
            List<UserAddRequest> user_requests = new List<UserAddRequest>()
            {
                user_Request_1,
                user_Request_2,
                user_Request_3
            };
            List<UserResponse> expected_user_responses = new List<UserResponse>();
            foreach (var user_request in user_requests)
            {
                UserResponse response = _userService.AddUser(user_request);
                expected_user_responses.Add(response);
            }
            //Act
            List<UserResponse> actual_user_responses_from_search = _userService.SearchUserBy(nameof(User.UserName), "");
            //Assert
            foreach (var expected_user_response in expected_user_responses)
            {
                Assert.Contains(expected_user_response, actual_user_responses_from_search);
            }
        }
        [Fact]
        public void searchUserby_ValidSearchString()
        {
            TenantAddRequest tenant_request_1 = new TenantAddRequest() { TenantName = "ABC Law School" };
            TenantResponse tenant_response_1 = _tenantsService.AddTenant(tenant_request_1);
            TenantAddRequest tenant_request_2 = new TenantAddRequest() { TenantName = "DEF Law School" };
            TenantResponse tenant_response_2 = _tenantsService.AddTenant(tenant_request_2);
            UserAddRequest user_Request_1 = new UserAddRequest()
            {
                UserName = "DuaBlaster...",
                FirstName = "Dual",
                LastName = "Mystic"
    ,
                Email = "Duall@example.com",
                Address = "Dual address",
                UserRole = DataTransferObject.Enums.Role.User,
                DateOfBirth = DateTime.Parse("1998-02-02"),
                emailVerified = true,
                AssociatedTenantID = tenant_response_1.TenantID
            };
            UserAddRequest user_Request_2 = new UserAddRequest()
            {
                UserName = "Kialaster...",
                FirstName = "Kia",
                LastName = "Burger"
,
                Email = "Kia@example.com",
                Address = "Kia address",
                UserRole = DataTransferObject.Enums.Role.User,
                DateOfBirth = DateTime.Parse("2004-04-04"),
                emailVerified = true,
                AssociatedTenantID = tenant_response_2.TenantID
            };
            UserAddRequest user_Request_3 = new UserAddRequest()
            {
                UserName = "NerfBlaster...",
                FirstName = "Nerf",
                LastName = "Machine"
,
                Email = "Nerf@example.com",
                Address = "Nerf address",
                UserRole = DataTransferObject.Enums.Role.User,
                DateOfBirth = DateTime.Parse("2001-11-08"),
                emailVerified = true,
                AssociatedTenantID = tenant_response_1.TenantID
            };
            List<UserAddRequest> user_requests = new List<UserAddRequest>()
            {
                user_Request_1,
                user_Request_2,
                user_Request_3
            };
            List<UserResponse> expected_user_responses = new List<UserResponse>();
            foreach (var user_request in user_requests)
            {
                UserResponse response = _userService.AddUser(user_request);
                expected_user_responses.Add(response);
            }
            //Act
            List<UserResponse> actual_user_responses_from_search = _userService.SearchUserBy(nameof(User.DateOfBirth), "2001-11-08");
            //Assert
            foreach (var expected_user_response in expected_user_responses)
            {
                if (expected_user_response.UserName != null && expected_user_response.UserName.Contains("2001-11-08", StringComparison.OrdinalIgnoreCase))
                {
                    Assert.Contains(expected_user_response, actual_user_responses_from_search);
                }
            }
        }
        #endregion
        #region getSortedUsers
        [Fact]
        public void getSortedUsers_DESC()
        {
            TenantAddRequest tenant_request_1 = new TenantAddRequest() { TenantName = "ABC Law School" };
            TenantResponse tenant_response_1 = _tenantsService.AddTenant(tenant_request_1);
            TenantAddRequest tenant_request_2 = new TenantAddRequest() { TenantName = "DEF Law School" };
            TenantResponse tenant_response_2 = _tenantsService.AddTenant(tenant_request_2);
            UserAddRequest user_Request_1 = new UserAddRequest()
            {
                UserName = "DuaBlaster...",
                FirstName = "Dual",
                LastName = "Mystic"
    ,
                Email = "Duall@example.com",
                Address = "Dual address",
                UserRole = DataTransferObject.Enums.Role.User,
                DateOfBirth = DateTime.Parse("1998-02-02"),
                emailVerified = true,
                AssociatedTenantID = tenant_response_1.TenantID
            };
            UserAddRequest user_Request_2 = new UserAddRequest()
            {
                UserName = "Kialaster...",
                FirstName = "Kia",
                LastName = "Burger"
,
                Email = "Kia@example.com",
                Address = "Kia address",
                UserRole = DataTransferObject.Enums.Role.User,
                DateOfBirth = DateTime.Parse("2004-04-04"),
                emailVerified = true,
                AssociatedTenantID = tenant_response_2.TenantID
            };
            UserAddRequest user_Request_3 = new UserAddRequest()
            {
                UserName = "NerfBlaster...",
                FirstName = "Nerf",
                LastName = "Machine"
,
                Email = "Nerf@example.com",
                Address = "Nerf address",
                UserRole = DataTransferObject.Enums.Role.User,
                DateOfBirth = DateTime.Parse("2001-11-08"),
                emailVerified = true,
                AssociatedTenantID = tenant_response_1.TenantID
            };
            List<UserAddRequest> user_requests = new List<UserAddRequest>()
            {
                user_Request_1,
                user_Request_2,
                user_Request_3
            };
            List<UserResponse> expected_user_responses = new List<UserResponse>();
            foreach (var user_request in user_requests)
            {
                UserResponse response = _userService.AddUser(user_request);
                expected_user_responses.Add(response);
            }
            //Act
            List<UserResponse> actual_user_responses_from_unsorted = _userService.ListAllUser();
            List<UserResponse> actual_user_responses_from_search = _userService.GetSortedUser(actual_user_responses_from_unsorted, nameof(User.UserName), SortOrderOption.DESC);
            expected_user_responses = expected_user_responses.OrderByDescending(user => user.UserName).ToList();
            //Assert
            for (int i = 0; i < expected_user_responses.Count; i++)
            {
                Assert.Equal(expected_user_responses[i], actual_user_responses_from_search[i]);
            }
        }
        #endregion
        #region updateUser
        [Fact]
        public void updateUser_InvalidRequest()
        {
            UserUpdateRequest? user_request = null;

            Assert.Throws<ArgumentNullException>(() => { _userService.UpdateUser(user_request); });
        }
        [Fact]
        public void UserUpdateRequest_WithOnlyUserId_FailsValidation()
        {
            // Arrange
            var dto = new UserUpdateRequest
            {
                UserId = Guid.NewGuid()
                // all other props null / default
            };

            var context = new ValidationContext(dto);
            var results = new List<ValidationResult>();

            // Act
            bool isValid = Validator.TryValidateObject(dto, context, results, validateAllProperties: true);

            // Assert
            Assert.False(isValid);
            Assert.Contains(results, r =>
                r.ErrorMessage != null &&
                r.ErrorMessage.Contains("at least one field", StringComparison.OrdinalIgnoreCase));
        }
        [Fact]
        public void UserUpdateRequest_WithOnlyUserId_SuccessValidation()
        {
            // Arrange
            var dto = new UserUpdateRequest
            {
                UserId = Guid.NewGuid(),
                UserName = "Tim"
                // all other props null / default
            };

            var context = new ValidationContext(dto);
            var results = new List<ValidationResult>();

            // Act
            bool isValid = Validator.TryValidateObject(dto, context, results, validateAllProperties: true);

            // Assert
            Assert.True(isValid);

        }
        [Fact]
        public void UpdatePerson_UserFullDetailsUpdation()
        {
            //Arrange
            TenantAddRequest tenant_add_request = new TenantAddRequest() { TenantName = "A Corp" };
            TenantResponse tenant_response_from_add = _tenantsService.AddTenant(tenant_add_request);

            UserAddRequest user_add_request = new UserAddRequest() { 
                UserName = "JohnW",
                FirstName = "John",
                LastName = "Wick",
                AssociatedTenantID = tenant_response_from_add.TenantID, 
                Address = "Abc road", 
                DateOfBirth = DateTime.Parse("2000-01-01"), 
                Email = "abc@example.com", 
                UserRole = Role.User, 
                emailVerified = true };

            UserResponse user_response_from_add = _userService.AddUser(user_add_request);

            UserUpdateRequest user_update_request = user_response_from_add.toUserUpdateRequest();
            user_update_request.UserName = "WilliamJ";
            user_update_request.Email = "william@example.com";

            //Act
            UserResponse user_response_from_update = _userService.UpdateUser(user_update_request);

            UserResponse? user_response_from_get = _userService.GetUserByID(user_response_from_update.UserID);

            //Assert
            Assert.Equal(user_response_from_get, user_response_from_update);

        }
        #endregion
        #region deleteUser
        //If you supply an valid PersonID, it should return true
        [Fact]
        public void DeletePerson_ValidPersonID()
        {
            //Arrange
            TenantAddRequest tenant_add_request = new TenantAddRequest() { TenantName = "A Corp" };
            TenantResponse tenant_response_from_add = _tenantsService.AddTenant(tenant_add_request);

            UserAddRequest user_add_request = new UserAddRequest()
            {
                UserName = "JohnW",
                FirstName = "John",
                LastName = "Wick",
                AssociatedTenantID = tenant_response_from_add.TenantID,
                Address = "Abc road",
                DateOfBirth = DateTime.Parse("2000-01-01"),
                Email = "abc@example.com",
                UserRole = Role.User,
                emailVerified = true
            };

            UserResponse user_response_from_add = _userService.AddUser(user_add_request);

            //Act
            bool isDeleted = _userService.DeleteUser(user_response_from_add.UserID);

            //Assert
            Assert.True(isDeleted);
        }


        //If you supply an invalid PersonID, it should return false
        [Fact]
        public void DeletePerson_InvalidPersonID()
        {
            //Act
            bool isDeleted = _userService.DeleteUser(Guid.NewGuid());

            //Assert
            Assert.False(isDeleted);
        }
        #endregion
    }
}