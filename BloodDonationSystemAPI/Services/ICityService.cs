using BloodDonationSystemAPI.DTOs.City;

namespace BloodDonationSystemAPI.Services
{
    public interface ICityService
    {
        Task<List<CityResponse>> GetAllCitiesAsync();
        Task<CityResponse?> GetCityByIdAsync(int id);
        Task<CityResponse> AddCityAsync(CreateCityRequest request);
        Task<bool> UpdateCityAsync(int id, UpdateCityRequest request);
        Task<bool> DeleteCityAsync(int id);
    }
}
