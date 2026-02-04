using BloodDonationSystemAPI.Data;
using BloodDonationSystemAPI.DTOs.Country;
using BloodDonationSystemAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BloodDonationSystemAPI.Services
{
    public class CountryService : ICountryService
    {
        private readonly AppDbContext context;
        public CountryService(AppDbContext context)
        {
            this.context = context;
        }


        public async Task<List<CountryResponse>> GetAllCountriesAsync()
        {
            return await context.Countries
                .Select(c => new CountryResponse
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .ToListAsync();
        }

        public Task<CountryResponse?> GetCountryByIdAsync(int id)
        {
            var country = context.Countries
                .Where(c => c.Id == id)
                .Select(c => new CountryResponse
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .FirstOrDefaultAsync();
            return country;
        }

        public async Task<CountryResponse> AddCountryAsync(CreateCountryRequest country)
        {
            var newCountry = new Country
            {
                Name = country.Name
            };

            context.Countries.Add(newCountry);
            await context.SaveChangesAsync();

            return new CountryResponse
            {
                Id = newCountry.Id,
                Name = newCountry.Name
            };
        }


        public async Task<bool> UpdateCountryAsync(int id, UpdateCountryRequest country)
        {
            var existingCountry = await context.Countries.FindAsync(id);
            if (existingCountry == null)
                return false;

            existingCountry.Name = country.Name;
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteCountryAsync(int id)
        {
            var countryDeleted = context.Countries.Find(id);
            if (countryDeleted is null)
                return false;

            context.Countries.Remove(countryDeleted);
            await context.SaveChangesAsync();
            return true;
        }
    }
}
