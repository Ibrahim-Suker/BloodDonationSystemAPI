using BloodDonationSystemAPI.DTOs.BloodGroup;
using BloodDonationSystemAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BloodDonationSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BloodGroupsController : ControllerBase
    {
     private readonly IBloodGroupService _bloodGroupService;
        public BloodGroupsController(IBloodGroupService bloodGroupService)
        {
            _bloodGroupService = bloodGroupService;
        }

        [HttpGet]
        public async Task<ActionResult<List<BloodGroupResponse>>> GetAllBloodGroups()
        {
            var bloodGroups = await _bloodGroupService.GetBloodGroupsAsync();
            return Ok(bloodGroups);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BloodGroupResponse>> GetBloodGroupById(int id)
        {
            var bloodGroup = await _bloodGroupService.GetBloodGroupByIdAsync(id);
            return bloodGroup is null ? NotFound($"Blood group with id {id} not found") : Ok(bloodGroup);
        }

        [HttpPost]
        public async Task<ActionResult<BloodGroupResponse>> AddBloodGroup(CreateBloodGroupRequest bloodGroup)
        {
            var newBloodGroup = await _bloodGroupService.AddBloodGroupAsync(bloodGroup);

            if (newBloodGroup == null)
                return BadRequest($"Blood group '{bloodGroup.GroupName}' already exists.");

            return CreatedAtAction(nameof(GetBloodGroupById), new { id = newBloodGroup.Id }, newBloodGroup);
        }

       [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBloodGroup(int id, UpdateBloodGroupRequest bloodGroup)
        {

            var updated = await _bloodGroupService.UpdateBloodGroupAsync(id, bloodGroup);
            if (!updated)
            {
                var exists = await _bloodGroupService.GetBloodGroupByIdAsync(id);
                if (exists == null)
                    return NotFound($"Blood group with id {id} not found.");
                else
                    return BadRequest($"Blood group '{bloodGroup.GroupName}' already exists.");
            }

            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBloodGroup(int id)
        {
            var result = await _bloodGroupService.DeleteBloodGroupAsync(id);
            if (!result)
                return NotFound($"Blood group with id {id} not found.");

            return NoContent();
        }
    }
}
