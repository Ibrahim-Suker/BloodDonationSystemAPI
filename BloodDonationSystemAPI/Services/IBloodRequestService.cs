using BloodDonationSystemAPI.DTOs.BloodRequest;

namespace BloodDonationSystemAPI.Services
{
    public interface IBloodRequestService
    {
        Task<List<BloodRequestResponse>> GetBloodRequestsAsync();
        Task<BloodRequestResponse?> GetBloodRequestByIdAsync(int id);
        Task<BloodRequestResponse> AddBloodRequestAsync(CreateBloodRequest request);
        Task<bool> UpdateBloodRequestAsync(int  id, UpdateBloodRequest request);
        Task<bool> DeleteBloodRequestAsync(int id);
    }
}
