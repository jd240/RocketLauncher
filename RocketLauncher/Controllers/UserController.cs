using Microsoft.AspNetCore.Mvc;

namespace RocketLauncher.Controllers
{
    public class UserController : Controller
    {
        [Route("user/index")]
        [Route("/")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
