using Microsoft.AspNetCore.Mvc;
using paytrack_api.Models;
using paytrack_api.Services.Interfaces;

namespace paytrack_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SalariesController : ControllerBase
    {
        private readonly ISalariesService _salariesService;

        public SalariesController(ISalariesService salariesService)
        {
            _salariesService = salariesService;
        }

        [HttpGet("GetAllSalaries")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var salaries = await _salariesService.GetAll();
                return Ok(salaries);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("GetSalaryById/{empId:int}")]
        public async Task<IActionResult> GetById(int empId)
        {
            if (empId <= 0)
            {
                return BadRequest("Invalid salary ID.");
            }

            try
            {
                var salary = await _salariesService.GetById(empId);

                if (salary == null)
                {
                    return NotFound($"Salary record with empId {empId} not found.");
                }

                return Ok(salary);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost("AddSalary")]
        public async Task<IActionResult> Add([FromBody] Salaries salary)
        {
            if (salary == null)
            {
                return BadRequest("Salary data is null.");
            }

            try
            {
                bool isAdded = await _salariesService.Add(salary);

                if (isAdded)
                {
                    return Ok("Salary added successfully.");
                }
                else
                {
                    return StatusCode(500, "Failed to add salary. Please try again.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("UpdateSalary")]
        public async Task<IActionResult> Update([FromBody] Salaries salary)
        {
            if (salary == null)
            {
                return BadRequest("Salary data is null.");
            }

            try
            {
                bool isUpdated = await _salariesService.Update(salary);

                if (isUpdated)
                {
                    return Ok("Salary updated successfully.");
                }
                else
                {
                    return NotFound($"Salary with ID {salary.Id} not found or update failed.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("DeleteSalary/{empId:int}")]
        public async Task<IActionResult> Delete(int empId)
        {
            if (empId <= 0)
            {
                return BadRequest("Invalid salary empId.");
            }

            try
            {
                var salary = await _salariesService.GetByEmpId(empId);
                if (salary == null)
                {
                    return NotFound($"Salary record with ID {empId} not found.");
                }

                bool isDeleted = await _salariesService.Delete(salary);

                if (isDeleted)
                {
                    return Ok("Salary deleted successfully.");
                }
                else
                {
                    return StatusCode(500, "Failed to delete salary. Please try again.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
