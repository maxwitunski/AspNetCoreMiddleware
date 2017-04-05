using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace WebApiWithCorsExample.Controllers
{
    [Route("api/data")]
    [EnableCors("MySingleOriginCorsPolicy")]
    public class DataController : Controller
    {
        [HttpGet]
        [DisableCors]
        public string GetSomeData()
        {
            return "some data here...";
        }
    }
}
