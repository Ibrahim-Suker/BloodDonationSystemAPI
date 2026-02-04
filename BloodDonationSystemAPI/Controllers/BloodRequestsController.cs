using BloodDonationSystemAPI.DTOs.BloodRequest;
using BloodDonationSystemAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BloodDonationSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BloodRequestsController : ControllerBase
    {
        private readonly IBloodRequestService _bloodRequestService;
        public BloodRequestsController(IBloodRequestService bloodRequestService)
        {
            _bloodRequestService = bloodRequestService;
        }

        [HttpGet]
        public async Task<ActionResult<List<BloodRequestResponse>>> GetBloodRequests()
        {
            var bloodRequest = await _bloodRequestService.GetBloodRequestsAsync();
            return bloodRequest is null ? NotFound($"nOT Blood Request ") : Ok(bloodRequest);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BloodRequestResponse>> GetBloodRequestById(int id)
        {
            var bloodRequest = await _bloodRequestService.GetBloodRequestByIdAsync(id);
            return bloodRequest is null ? NotFound($"Blood request with id {id} not found.") : Ok(bloodRequest);
        }

        [HttpPost]
        public async Task<ActionResult<BloodRequestResponse>> AddBloodRequest([FromBody] CreateBloodRequest request)
        {
            try
            {
                var newRequest = await _bloodRequestService.AddBloodRequestAsync(request);
                return CreatedAtAction(nameof(GetBloodRequestById), new { id = newRequest.Id }, newRequest);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBloodRequest(int id, [FromBody] UpdateBloodRequest request)
        {
            var updated = await _bloodRequestService.UpdateBloodRequestAsync(id, request);
            if (!updated)
                return NotFound($"Blood request with id {id} not found.");

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBloodRequest(int id)
        {
            var deleted = await _bloodRequestService.DeleteBloodRequestAsync(id);
            return deleted ? NoContent() : NotFound($"Blood request with id {id} not found.");
        }
    }
}
