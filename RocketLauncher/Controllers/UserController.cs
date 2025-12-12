using DataTransferObject;
using DataTransferObject.DTO;
using DataTransferObject.Enums;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
namespace RocketLauncher.Controllers
{
    public class UserController : Controller
    {
        private readonly UserIService _userService;
        private readonly TenantIService _tenantService;
        public UserController(UserIService userService, TenantIService tenantService)
        {
            _userService = userService;
            _tenantService = tenantService;
        }
        [Route("users/index")]
        [Route("/")]
        public IActionResult Index(string searchBy, string? searchString, string sortBy = nameof(UserResponse.UserName), SortOrderOption sortOrder = SortOrderOption.ASC)
        {
            ViewBag.SearchFields = new Dictionary<string, string>()
            {
                { nameof(UserResponse.UserName), "User Name" },
                { nameof(UserResponse.FirstName), "First Name" },
                { nameof(UserResponse.LastName), "Last Name" },
                { nameof(UserResponse.TenantName), "Tenant Name" },
                { nameof(UserResponse.Email), "Email" },
                { nameof(UserResponse.UserRole), "Role" }
            }
            ;
            List<UserResponse> userList = _userService.SearchUserBy(searchBy, searchString);
            ViewBag.CurrentSearchBy = searchBy;
            ViewBag.CurrentSearchString = searchString;
            //sort
            ViewBag.CurrentSortBy = sortBy;
            ViewBag.CurrentSortOrder = sortOrder;
            List<UserResponse> SorteduserList = _userService.GetSortedUser(userList, sortBy, sortOrder);
            return View(SorteduserList);
        }
    }
}
