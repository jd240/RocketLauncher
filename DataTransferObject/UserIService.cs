using System;
using DataTransferObject.DTO;
using DataTransferObject.Enums;
namespace DataTransferObject
{
    public interface UserIService
    {
        UserResponse AddUser (UserAddRequest? request);
        UserResponse UpdateUser (UserUpdateRequest? request);
        bool DeleteUser (Guid? UserID);
        List<UserResponse> ListAllUser ();
        UserResponse? GetUserByID(Guid? UserID);
        List<UserResponse> SearchUserBy (string searchBy, string? SearchString);
        List<UserResponse> GetSortedUser (List<UserResponse> allusers, string SortBy, SortOrderOption sortorder);
    }
}
