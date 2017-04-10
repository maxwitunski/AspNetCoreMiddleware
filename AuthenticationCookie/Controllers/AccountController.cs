using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AuthenticationCookie.Controllers
{
    [Route("account")]
    public class AccountController : Controller
    {
        [HttpGet("signin")] // http://localhost:5000/account/signin
        public async Task<IActionResult> SignIn()
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, "My Name", ClaimValueTypes.String),
            };

            ClaimsIdentity identity = new ClaimsIdentity(claims, "Custom");
            ClaimsPrincipal principal = new ClaimsPrincipal(identity);
            await HttpContext.Authentication.SignInAsync("OurAuthenticationCookie", principal);
            var returnUrl = Request.Query["ReturnUrl"];
            return Redirect($"../..{returnUrl}");
        }
    }
}
