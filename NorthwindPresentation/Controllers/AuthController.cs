using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace NorthwindPresentation.Controllers
{
    [Route("auth")]
    public class AuthController : Controller
    {

        [Authorize]
        [HttpGet("login")]
        public IActionResult Login()
        {
            return Redirect("/customers");
        }
        
        
        [Authorize]
        [HttpGet("currentuser")]
        public ActionResult<string> CurrentUser()
        {
            return Ok(User.Identity.Name);
        }
        
        
    }
}