using Microsoft.AspNetCore.Mvc;
using paytrack_api.Models;
using paytrack_api.Services.Interfaces;

namespace paytrack_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientController : ControllerBase
    {
        public readonly IClientService _clientService;

        public ClientController(IClientService clientService)
        {
            this._clientService = clientService;
        }

        [HttpGet("GetAllClients")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var clients = await _clientService.GetAll();
                return Ok(clients);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("GetClientById/{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid client ID.");
            }

            try
            {
                var client = await _clientService.GetById(id);

                if (client == null)
                {
                    return NotFound($"Client with ID {id} not found.");
                }
                else
                {
                    return Ok(client);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost("AddClient")]
        public async Task<IActionResult> Add([FromBody] Client client)
        {
            if (client == null)
            {
                return BadRequest("Client data is null.");
            }

            try
            {
                bool isAdded = await _clientService.Add(client);

                if (isAdded)
                {
                    return Ok("Client added successfully.");
                }
                else
                {
                    return StatusCode(500, "Failed to add client. Please try again.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("UpdateClient")]
        public async Task<IActionResult> Update([FromBody] Client client)
        {
            if (client == null)
            {
                return BadRequest("Client data is null.");
            }

            try
            {
                bool isUpdated = await _clientService.Update(client);

                if (isUpdated)
                {
                    return Ok("Client updated successfully.");
                }
                else
                {
                    return NotFound($"Client with ID {client.Id} not found or update failed.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("DeleteClient/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid client ID.");
            }

            try
            {
                var client = await _clientService.GetById(id);
                if (client == null)
                {
                    return NotFound($"Client with ID {id} not found.");
                }

                bool isDeleted = await _clientService.Delete(client);

                if (isDeleted)
                {
                    return Ok("Client deleted successfully.");
                }
                else
                {
                    return StatusCode(500, "Failed to delete client. Please try again.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
