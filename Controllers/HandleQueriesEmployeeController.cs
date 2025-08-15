using Microsoft.AspNetCore.Mvc;
using paytrack_api.Models;
using paytrack_api.Services.Interfaces;

namespace paytrack_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HandleQueriesEmployeeController : ControllerBase
    {
        private readonly IHandleQueriesEmployeeService _handleQueriesEmployeeService;

        public HandleQueriesEmployeeController(IHandleQueriesEmployeeService handleQueriesEmployeeService)
        {
            _handleQueriesEmployeeService = handleQueriesEmployeeService;
        }

        [HttpGet("GetAllEmployeeQueries")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var EmployeeQueries = await _handleQueriesEmployeeService.GetAll();
                return Ok(EmployeeQueries);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("GetEmpQueryById/{empId:int}")]
        public async Task<IActionResult> GetById(int empId)
        {
            if (empId <= 0)
            {
                return BadRequest("Invalid EmpQuery ID.");
            }

            try
            {
                var EmpQuery = await _handleQueriesEmployeeService.GetById(empId);

                if (EmpQuery == null)
                {
                    return NotFound($"EmpQuery record with empId {empId} not found.");
                }

                return Ok(EmpQuery);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost("AddEmpQuery")]
        public async Task<IActionResult> Add([FromBody] HandleQueriesEmployee EmpQuery)
        {
            if (EmpQuery == null)
            {
                return BadRequest("EmpQuery data is null.");
            }

            try
            {
                bool isAdded = await _handleQueriesEmployeeService.Add(EmpQuery);

                if (isAdded)
                {
                    return Ok("EmpQuery added successfully.");
                }
                else
                {
                    return StatusCode(500, "Failed to add EmpQuery. Please try again.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("UpdateEmpQuery")]
        public async Task<IActionResult> Update([FromBody] HandleQueriesEmployee EmpQuery)
        {
            if (EmpQuery == null)
            {
                return BadRequest("EmpQuery data is null.");
            }

            try
            {
                bool isUpdated = await _handleQueriesEmployeeService.Update(EmpQuery);

                if (isUpdated)
                {
                    return Ok("EmpQuery updated successfully.");
                }
                else
                {
                    return NotFound($"EmpQuery with ID {EmpQuery.Id} not found or update failed.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


    }
}
