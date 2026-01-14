using System;
using DataTransferObject.DTO;
using DataTransferObject.Enums;
namespace DataTransferObject
{
    public interface UserIService
    {
        UserResponse AddUser (UserAddRequest? request);
        TenantResponse AddTenant(TenantAddRequest? tenantRequest);
        UserResponse UpdateUser (UserUpdateRequest? request);
        TenantResponse UpdateTenant(TenantUpdateRequest? tenantRequest);
        bool DeleteUser (Guid? UserID);
        bool DeleteTenant(Guid? TenantId);
        List<UserResponse> ListAllUser ();
        UserResponse? GetUserByID(Guid? UserID);
        List<UserResponse> SearchUserBy (string searchBy, string? SearchString);
        List<UserResponse> GetSortedUser (List<UserResponse> allusers, string SortBy, SortOrderOption sortorder);
        List<TenantResponse> ListAllTenantUsers();
    }
}
