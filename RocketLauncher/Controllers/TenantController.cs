using DataTransferObject;
using DataTransferObject.DTO;
using DataTransferObject.Enums;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Reflection;
namespace RocketLauncher.Controllers
{
    [Route("tenants")]
    public class TenantController : Controller
    {
        private readonly UserIService _userService;
        private readonly TenantIService _tenantService;
        public TenantController(UserIService userService, TenantIService tenantService)
        {
            _userService = userService;
            _tenantService = tenantService;
        }
        [Route("index")]
        public IActionResult Index()
        {
            List<TenantResponse> SorteduserList = _userService.ListAllTenantUsers();
            return View(SorteduserList);
        }
        [Route("create")]
        [HttpGet]
        public IActionResult CreateTenant()
        {
            //List<TenantResponse> tenants = _userService.ListAllTenantUsers();
            //ViewBag.Tenants = tenants;
            return View();
        }
        [HttpPost]
        [Route("create")]
        public IActionResult CreateUser(TenantAddRequest tenantAddRequest)
        {
            if (!ModelState.IsValid)
            {
                //List<TenantResponse> tenants = _userService.ListAllTenantUsers();
                //ViewBag.Tenants = tenants;
                ViewBag.Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return View();
            }

            //call the service method
            TenantResponse userResponse = _userService.AddTenant(tenantAddRequest);

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

            List<TenantResponse> tenants = _userService.ListAllTenantUsers();
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
                List<TenantResponse> tenants = _userService.ListAllTenantUsers();
                ViewBag.Tenants = tenants.Select(temp =>
                new SelectListItem() { Text = temp.TenantName, Value = temp.TenantID.ToString() });

                ViewBag.Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return View(userUpdateRequest);
            }
        }
        [HttpGet]
        [Route("delete/{userID}")] //Eg: /persons/edit/1
        public IActionResult DeleteUser(Guid userId)
        {
            UserResponse? userResponse = _userService.GetUserByID(userId);
            return View(userResponse);
        }

        [HttpPost]
        [Route("delete/{userID}")]
        public IActionResult DeleteUser(UserUpdateRequest userUpdateRequest)
        {
            UserResponse? userResponse = _userService.GetUserByID(userUpdateRequest.UserId);
            _userService.DeleteUser(userUpdateRequest.UserId);
            return RedirectToAction("Index");

        }
    }
}
