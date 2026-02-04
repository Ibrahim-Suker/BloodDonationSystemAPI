using BloodDonationSystemAPI.DTOs.BloodGroup;

namespace BloodDonationSystemAPI.Services
{
    public interface IBloodGroupService
    {
        Task<List<BloodGroupResponse>> GetBloodGroupsAsync();
        Task<BloodGroupResponse?> GetBloodGroupByIdAsync(int id);
        Task<BloodGroupResponse> AddBloodGroupAsync(CreateBloodGroupRequest request);
        Task<bool> UpdateBloodGroupAsync(int id, UpdateBloodGroupRequest request);
        Task<bool> DeleteBloodGroupAsync(int id);
    }
}
