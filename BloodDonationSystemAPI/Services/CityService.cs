using BloodDonationSystemAPI.Data;
using BloodDonationSystemAPI.DTOs.City;
using BloodDonationSystemAPI.Models;
using Microsoft.EntityFrameworkCore;
namespace BloodDonationSystemAPI.Services
{
    public class CityService : ICityService
    {
        private readonly AppDbContext context;
        public CityService(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<List<CityResponse>> GetAllCitiesAsync()
        {
            return await context.Cities
                .Include(c => c.Country)
                .Select(c => new CityResponse
                {
                    Id = c.Id,
                    Name = c.Name,
                    CountryId = c.CountryId,
                    CountryName = c.Country.Name
                }).ToListAsync();
        }

        public Task<CityResponse?> GetCityByIdAsync(int id)
        {
            var city = context.Cities
                .Include(c => c.Country)
                .Where(c => c.Id == id)
                .Select(c => new CityResponse
                {
                    Id = c.Id,
                    Name = c.Name,
                    CountryId = c.CountryId,
                    CountryName = c.Country.Name
                }).FirstOrDefaultAsync();
            return city;
        }

        public async Task<CityResponse?> AddCityAsync(CreateCityRequest request)
        {
            var country = await context.Countries.FindAsync(request.CountryId);
            if (country == null)
                return null;

            var newCity = new City
            {
                Name = request.Name,
                CountryId = request.CountryId
            };
            context.Cities.Add(newCity);
            await context.SaveChangesAsync();
           return new CityResponse
            {
                Id = newCity.Id,
                Name = newCity.Name,
                CountryId = newCity.CountryId,
                CountryName = country.Name
           };
        }

        public async Task<bool> UpdateCityAsync(int id, UpdateCityRequest request)
        {
            var existingCity = await context.Cities.FindAsync(id);
            if (existingCity is null)
                return false;

            var country = await context.Countries.FindAsync(request.CountryId);
            if (country is null)
                return false;

            existingCity.Name = request.Name;
            existingCity.CountryId = request.CountryId;

            await context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteCityAsync(int id)
        {
            var cityDeleted = await context.Cities.FindAsync(id);
            if (cityDeleted is null)
                return false;

            context.Cities.Remove(cityDeleted);
            await context.SaveChangesAsync();
            return true;
        }
    }
}
