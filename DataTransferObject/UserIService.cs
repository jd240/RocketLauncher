using System;
using DataTransferObject.DTO;
namespace DataTransferObject
{
    public interface UserIService
    {
        UserResponse AddUser (UserAddRequest? request);
        List<UserResponse> ListAllUser ();
        UserResponse? GetUserByID(Guid? UserID);
        List<UserResponse> SearchUserBy (string searchBy, string? SearchString);
    }
}
