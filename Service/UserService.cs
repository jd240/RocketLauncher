using DataTransferObject;
using DataTransferObject.DTO;
using Entities;
using Services;
using Services.Helpers;
using System;
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
                case nameof(User.Role):
                    MatchedResult = result.Where(temp =>
                    (!string.IsNullOrEmpty(temp.Role) ?
                    temp.Role.Contains(SearchString, StringComparison.OrdinalIgnoreCase) :
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

                default:
                    MatchedResult = result;
                    break;
            }
            return MatchedResult;
        }
    }
}
