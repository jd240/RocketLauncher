using DataTransferObject;
using DataTransferObject.DTO;
using DataTransferObject.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Reflection;
namespace RocketLauncher.Controllers
{
    [Route("users")]
    public class UserController : Controller
    {
        private readonly UserIService _userService;
        private readonly TenantIService _tenantService;
        public UserController(UserIService userService, TenantIService tenantService)
        {
            _userService = userService;
            _tenantService = tenantService;
        }
        [Route("index")]
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
        [Route("create")]
        [HttpGet]
        public IActionResult CreateUser()
        {
            List<TenantResponse> tenants = _tenantService.ListAllTenant();
            ViewBag.Tenants = tenants;

            return View();
        }
        [HttpPost]
        [Route("create")]
        public IActionResult CreateUser(UserAddRequest userAddRequest)
        {
            if (!ModelState.IsValid)
            {
                List<TenantResponse> tenants = _tenantService.ListAllTenant();
                ViewBag.Tenants = tenants;

                ViewBag.Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return View();
            }

            //call the service method
            UserResponse userResponse = _userService.AddUser(userAddRequest);

            //navigate to Index() action method (it makes another get request to "users/index"
            return RedirectToAction("Index", "User");
        }
        [HttpGet]
        [Route("update/{userID}")] //Eg: /persons/edit/1
        public IActionResult UpdateUser(Guid userId)
        {
            UserResponse? userResponse = _userService.GetUserByID(userId);
            if (userResponse == null)
            {
                return RedirectToAction("Index");
            }

            UserUpdateRequest userUpdateRequest = userResponse.toUserUpdateRequest();

            List<TenantResponse> tenants = _tenantService.ListAllTenant();
            ViewBag.Tenants = tenants.Select(temp =>
            new SelectListItem() { Text = temp.TenantName, Value = temp.TenantID.ToString() });

            return View(userUpdateRequest);
        }

        [HttpPost]
        [Route("update/{userID}")]
        public IActionResult UpdateUser(UserUpdateRequest userUpdateRequest)
        {
            UserResponse? userResponse = _userService.GetUserByID(userUpdateRequest.UserId);

            if (userResponse == null)
            {
                return RedirectToAction("Index");
            }

            if (ModelState.IsValid)
            {
                UserResponse updatedUser = _userService.UpdateUser(userUpdateRequest);
                return RedirectToAction("Index");
            }
            else
            {
                List<TenantResponse> tenants = _tenantService.ListAllTenant();
                ViewBag.Tenants = tenants.Select(temp =>
                new SelectListItem() { Text = temp.TenantName, Value = temp.TenantID.ToString() });

                ViewBag.Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return View(userUpdateRequest);
            }
        }
    }
}
