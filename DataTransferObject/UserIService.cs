using System;
using DataTransferObject.DTO;
using DataTransferObject.Enum;
namespace DataTransferObject
{
    public interface UserIService
    {
        UserResponse AddUser (UserAddRequest? request);
        List<UserResponse> ListAllUser ();
        UserResponse? GetUserByID(Guid? UserID);
        List<UserResponse> SearchUserBy (string searchBy, string? SearchString);
        List<UserResponse> GetSortedUser (List<UserResponse> allusers, string SortBy, SortOrderOption sortorder);
    }
}
