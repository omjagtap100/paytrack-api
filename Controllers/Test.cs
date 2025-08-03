using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using paytrack_api.Models;
using paytrack_api.Services.Interfaces;

namespace paytrack_api.Controllers
{
 
    [ApiController]
    [Route("api/[controller]")]
    public class TestController : ControllerBase
    {
        public readonly ICompanyService _companyService ;
        public TestController(ICompanyService companyService)
        {
            this._companyService = companyService;
        }
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
        [HttpGet("testFlow")]
        public async Task<ActionResult<IEnumerable<Company>>> testFlow()
        {

            return Ok(await _companyService.GetCompanyData());
        }
        [HttpGet("GetCompanyById")]
        public async Task<ActionResult<IEnumerable<Company>>> GetById()
        {

            return Ok( _companyService.GetById(1));
        }

    }
}
