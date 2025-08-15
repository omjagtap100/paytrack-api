using Microsoft.AspNetCore.Mvc;
using paytrack_api.Models;
using paytrack_api.Services.Interfaces;

namespace paytrack_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HandleQueriesClientController : ControllerBase
    {
        private readonly IHandleQueriesEmployeeService _handleQueriesClientService;

        public HandleQueriesClientController(IHandleQueriesEmployeeService handleQueriesClientService)
        {
            _handleQueriesClientService = handleQueriesClientService;
        }

        [HttpGet("GetAllClientQueries")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var ClientQueries = await _handleQueriesClientService.GetAll();
                return Ok(ClientQueries);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("GetClientQueryById/{empId:int}")]
        public async Task<IActionResult> GetById(int empId)
        {
            if (empId <= 0)
            {
                return BadRequest("Invalid ClientQuery ID.");
            }

            try
            {
                var ClientQuery = await _handleQueriesClientService.GetById(empId);

                if (ClientQuery == null)
                {
                    return NotFound($"ClientQuery record with empId {empId} not found.");
                }

                return Ok(ClientQuery);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost("AddClientQuery")]
        public async Task<IActionResult> Add([FromBody] HandleQueriesEmployee ClientQuery)
        {
            if (ClientQuery == null)
            {
                return BadRequest("ClientQuery data is null.");
            }

            try
            {
                bool isAdded = await _handleQueriesClientService.Add(ClientQuery);

                if (isAdded)
                {
                    return Ok("ClientQuery added successfully.");
                }
                else
                {
                    return StatusCode(500, "Failed to add ClientQuery. Please try again.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("UpdateClientQuery")]
        public async Task<IActionResult> Update([FromBody] HandleQueriesEmployee ClientQuery)
        {
            if (ClientQuery == null)
            {
                return BadRequest("ClientQuery data is null.");
            }

            try
            {
                bool isUpdated = await _handleQueriesClientService.Update(ClientQuery);

                if (isUpdated)
                {
                    return Ok("ClientQuery updated successfully.");
                }
                else
                {
                    return NotFound($"ClientQuery with ID {ClientQuery.Id} not found or update failed.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


    }
}
