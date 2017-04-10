using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Http.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AuthenticationCookie.Controllers
{
    [Route("account")]
    public class AccountController : Controller
    {
        [HttpGet("signin")] // http://localhost:5000/account/signin
        public async Task SignIn()
        {
            await HttpContext.Authentication.ChallengeAsync(OpenIdConnectDefaults.AuthenticationScheme, new AuthenticationProperties { RedirectUri = "/" });
        }

        [HttpGet("signout")] // http://localhost:5000/account/signout
        public async Task SignOut()
        {
            await HttpContext.Authentication.SignOutAsync(OpenIdConnectDefaults.AuthenticationScheme);
            await HttpContext.Authentication.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }
    }
}
