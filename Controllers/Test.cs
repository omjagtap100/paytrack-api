using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace paytrack_api.Controllers
{
 
    [ApiController]
    [Route("api/[controller]")]
    public class TestController : ControllerBase
    {
        //[Authorize]
        [HttpGet("{id}")]
        public IActionResult ValidateToken()
        {
            var tokenHeader = Request.Headers["Authorization"].ToString();
            Console.WriteLine(tokenHeader);
            var userName = HttpContext.User.Identity?.Name ?? "Unknown";
            var claims = HttpContext.User.Claims.Select(c => new { c.Type, c.Value });

            return Ok(new
            {
                Message = "Token validated successfully!",
                User = userName,
                AuthorizationHeader = tokenHeader,
                Claims = claims
            });
        }
        [Authorize]
        [HttpGet()]
        public IActionResult ValidateTokenAuthorized()
        {
            var tokenHeader = Request.Headers["Authorization"].ToString();
            Console.WriteLine(tokenHeader);
            var userName = HttpContext.User.Identity?.Name ?? "Unknown";
            var claims = HttpContext.User.Claims.Select(c => new { c.Type, c.Value });

            return Ok(new
            {
                Message = "Token validated successfully!",
                User = userName,
                AuthorizationHeader = tokenHeader,
                Claims = claims
            });
        }

    }
}
