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
       
        [HttpGet("GetAllEmployees")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var employees = await _employeeService.GetAll();
                return Ok(employees);
            }
            catch (Exception ex)
            {
               
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


    }
}
