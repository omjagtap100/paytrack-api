using Microsoft.AspNetCore.Mvc;
using paytrack_api.Models;
using paytrack_api.Services;
using paytrack_api.Services.Interfaces;

namespace paytrack_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController:ControllerBase
    {
        public readonly IEmployeeService _employeeService;
        public EmployeeController(IEmployeeService employeeService)
        {
            this._employeeService = employeeService;
        }
        [HttpPost("AddEmployee")]
        public async Task<ActionResult<IEnumerable<bool>>> Add([FromBody] Employee employee)
        {

            return Ok(await _employeeService.Add(employee));
        }
    }
}
