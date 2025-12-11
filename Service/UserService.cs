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
        public UserService() 
        {
            _users = new List<User>();
            _tenantService = new TenantService();
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
            return _users.Select(user => user.ToPersonResponse()).ToList();
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
                case nameof(User.UserName):
                    MatchedResult = result.Where(temp =>
                    (!string.IsNullOrEmpty(temp.UserName) ?
                    temp.UserName.Contains(SearchString, StringComparison.OrdinalIgnoreCase) :
                    true)).ToList();
                    break;
                case nameof(User.FirstName):
                    MatchedResult = result.Where(temp =>
                    (!string.IsNullOrEmpty(temp.FirstName) ?
                    temp.FirstName.Contains(SearchString, StringComparison.OrdinalIgnoreCase) :
                    true)).ToList();
                    break;
                case nameof(User.LastName):
                    MatchedResult = result.Where(temp =>
                    (!string.IsNullOrEmpty(temp.LastName) ?
                    temp.LastName.Contains(SearchString, StringComparison.OrdinalIgnoreCase) :
                    true)).ToList();
                    break;
                case nameof(User.Email):
                    MatchedResult = result.Where(temp =>
                    (!string.IsNullOrEmpty(temp.Email) ?
                    temp.Email.Contains(SearchString, StringComparison.OrdinalIgnoreCase) :
                    true)).ToList();
                    break;
                case nameof(User.UserRole):
                    MatchedResult = result.Where(temp =>
                    (!string.IsNullOrEmpty(temp.UserRole) ?
                    temp.UserRole.Contains(SearchString, StringComparison.OrdinalIgnoreCase) :
                    true)).ToList();
                    break;
                case nameof(User.TenantID):
                    MatchedResult = result.Where(temp =>
                    (!string.IsNullOrEmpty(temp.TenantName) ?
                    temp.TenantName.Contains(SearchString, StringComparison.OrdinalIgnoreCase) :
                    true)).ToList();
                    break;
                case nameof(User.Address):
                    MatchedResult = result.Where(temp =>
                    (!string.IsNullOrEmpty(temp.Address) ?
                    temp.Address.Contains(SearchString, StringComparison.OrdinalIgnoreCase) :
                    true)).ToList();
                    break;
                case nameof(User.DateOfBirth):
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

            if (request.TenantID.HasValue)
                matchingUser.TenantID = request.TenantID;

            if (!string.IsNullOrWhiteSpace(request.Address))
                matchingUser.Address = request.Address;
            
            if (request.emailVerified.HasValue)
                matchingUser.emailVerified = request.emailVerified.Value;


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
