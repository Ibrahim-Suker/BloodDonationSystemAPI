using BloodDonationSystemAPI.DTOs.Country;
using BloodDonationSystemAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace BloodDonationSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
        private readonly ICountryService countryService;
        public CountriesController(ICountryService countryService)
        {
            this.countryService = countryService;
        }


        [HttpGet]
        public async Task<ActionResult<List<CountryResponse>>> GetAllCountries()
        {
            var countries = await countryService.GetAllCountriesAsync();
            return Ok(countries);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<CountryResponse>> GetCountryById(int id)
        {
            var country = await countryService.GetCountryByIdAsync(id);
            return country is null ? NotFound($"Country with id {id} not found") : Ok(country);
        }


        [HttpPost]
        public async Task<ActionResult<CountryResponse>> AddCountry(CreateCountryRequest country)
        {
            var newCountry = await countryService.AddCountryAsync(country);
            return CreatedAtAction(nameof(GetCountryById), new { id = newCountry.Id }, newCountry);
        }


        [HttpPut("{id}")]
        public async Task<ActionResult<CountryResponse>> UpdateCountry(int id, UpdateCountryRequest country)
        {
            var isUpdated = await countryService.UpdateCountryAsync(id, country);
            return isUpdated ? Ok(country) : NotFound($"Country with id {id} not found");
        }


    [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCountry(int id)
        {
            var isDeleted = await countryService.DeleteCountryAsync(id);
            return isDeleted ? NoContent() : NotFound($"Country with id {id} not found");
        }
    }
}
