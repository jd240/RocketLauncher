using DataTransferObject;
using DataTransferObject.DTO;
using System;

namespace Service
{
    public class UserService : UserIService
    {
        public UserResponse AddUser(UserAddRequest? request)
        {
            throw new NotImplementedException();
            //if(request == null)
            //{
            //   throw new ArgumentNullException(nameof(request), "UserAddRequest cannot be null.");
            //}
            //if (string.IsNullOrEmpty(request.UserName))
            //{
            //  throw new ArgumentException("UserName cannot be null.", nameof(request.UserName));
            //}
        }

        public List<UserResponse> ListAllUser()
        {
            throw new NotImplementedException();
        }
    }
}
