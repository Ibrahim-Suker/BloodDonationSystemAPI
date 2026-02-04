using BloodDonationSystemAPI.DTOs.City;
using BloodDonationSystemAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BloodDonationSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitiesController : ControllerBase
    {
        private readonly ICityService cityService;

        public CitiesController(ICityService cityService)
        {
            this.cityService = cityService;
        }

        [HttpGet]
        public async Task<ActionResult<List<CityResponse>>> GetAllCities()
        {
            var cities = await cityService.GetAllCitiesAsync();
            return Ok(cities);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CityResponse>> GetCityById(int id)
        {
            var city = await cityService.GetCityByIdAsync(id);
            return city is null ? NotFound($"City with ID {id} not found.") : Ok(city);
        }

        [HttpPost]
        public async Task<ActionResult<CityResponse>> AddCity([FromBody] CreateCityRequest request)
        {
            var newCity = await cityService.AddCityAsync(request);

            if (newCity == null)
                return BadRequest($"CountryId {request.CountryId} is not valid");

            return CreatedAtAction(nameof(GetCityById), new { id = newCity.Id }, newCity);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCity(int id, [FromBody] UpdateCityRequest request)
        {
            var isUpdated = await cityService.UpdateCityAsync(id, request);
            if (!isUpdated)
                return NotFound($"City with ID {id} not found or CountryId {request.CountryId} is not valid");
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCity(int id)
        {
            var result = await cityService.DeleteCityAsync(id);

            if (!result)
                return NotFound($"City with ID {id} not found");

            return NoContent();
        }
    }
}