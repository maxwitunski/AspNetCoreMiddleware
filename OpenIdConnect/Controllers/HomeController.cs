using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationCookie.Controllers
{
    [Route("")]
    public class HomeController : Controller
    {

        [HttpGet] // http://localhost:5000
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("forbidden")]
        public IActionResult Forbidden()
        {
            return View();
        }

        [HttpGet("private")] // http://localhost:5000/private
        [Authorize(ActiveAuthenticationSchemes = "OurAuthenticationCookie")]
        public IActionResult Private()
        {
            return View();
        }

    }
}
