using BloodDonationSystemAPI.Data;
using BloodDonationSystemAPI.DTOs.Country;

namespace BloodDonationSystemAPI.Services
{
    public interface ICountryService
    {
        Task<List<CountryResponse>> GetAllCountriesAsync();
        Task<CountryResponse?> GetCountryByIdAsync(int id);
        Task<CountryResponse> AddCountryAsync(CreateCountryRequest country);
        Task<bool> UpdateCountryAsync(int id, UpdateCountryRequest country);
        Task<bool> DeleteCountryAsync(int id);
    }
}
