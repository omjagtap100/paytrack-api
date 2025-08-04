using Microsoft.AspNetCore.Mvc;
using paytrack_api.Models;
using paytrack_api.Services;
using paytrack_api.Services.Interfaces;

namespace paytrack_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
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

        [HttpGet("GetEmployeeById/{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
      
            if (id <= 0)
            {
                return BadRequest("Invalid employee ID.");
            }

            try
            {
                var employee = await _employeeService.GetById(id);

                if (employee == null)
                {
                    return NotFound($"Employee with ID {id} not found.");
                }
                else
                {
                    return Ok(employee);
                }
            }
            catch (Exception ex)
            {
               
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        [HttpPost("AddEmployee")]
        public async Task<IActionResult> Add([FromBody] Employee employee)
        {
        
            if (employee == null)
            {
                return BadRequest("Employee data is null.");
            }

            try
            {
                bool isAdded = await _employeeService.Add(employee);

                if (isAdded)
                {
                    return Ok("Employee added successfully.");
                }
                else
                {
                    return StatusCode(500, "Failed to add employee. Please try again.");
                }
            }
            catch (Exception ex)
            {
               
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("UpdateEmployee")]
        public async Task<IActionResult> Update([FromBody] Employee employee)
        {
            
            if (employee == null)
            {
                return BadRequest("Employee data is null.");
            }

            try
            {
                bool isUpdated = await _employeeService.Update(employee);

                if (isUpdated)
                {
                    return Ok("Employee updated successfully.");
                }
                else
                {
                    return NotFound($"Employee with ID {employee.Id} not found or update failed.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("DeleteEmployee/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {

            if (id <= 0)
            {
                return BadRequest("Invalid employee ID.");
            }

            try
            {
                
                var employee = await _employeeService.GetById(id);
                if (employee == null)
                {
                    return NotFound($"Employee with ID {id} not found.");
                }

                bool isDeleted = await _employeeService.Delete(employee);

                if (isDeleted)
                {
                    return Ok("Employee deleted successfully.");
                }
                else
                {
                    return StatusCode(500, "Failed to delete employee. Please try again.");
                }
            }
            catch (Exception ex)
            {
                
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


    }
}
