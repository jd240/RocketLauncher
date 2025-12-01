using DataTransferObject;
using DataTransferObject.DTO;
using Service;
using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Xunit;
namespace TestDriven
{
    public class UserServiceTest
    {
        private readonly UserIService _userService;
        public UserServiceTest() {
            _userService = new UserService();
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
                Role = DataTransferObject.Enum.Role.User,
                DateOfBirth = DateTime.Parse("2000-01-01"),
                emailVerified = true
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
            UserAddRequest? UserAddRequest = new UserAddRequest() { UserName = "Sample Username...", FirstName = "Du", LastName = "Duong", Email = "person@example.com", Address = "sample address", Role = DataTransferObject.Enum.Role.User, DateOfBirth = DateTime.Parse("2000-01-01"), emailVerified = true };

            //Act
            UserResponse person_response_from_add = _userService.AddUser(UserAddRequest);

            List<UserResponse> persons_list = _userService.ListAllUser();

            //Assert
            Assert.True(person_response_from_add.UserID != Guid.Empty);

            Assert.Contains(person_response_from_add, persons_list);
        }

        #endregion

    }
}