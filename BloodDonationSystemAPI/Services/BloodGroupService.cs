using BloodDonationSystemAPI.Data;
using BloodDonationSystemAPI.DTOs.BloodGroup;
using BloodDonationSystemAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BloodDonationSystemAPI.Services
{
    public class BloodGroupService : IBloodGroupService
    {
        private readonly AppDbContext context;
        public BloodGroupService(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<List<BloodGroupResponse>> GetBloodGroupsAsync()
        {
            return await context.BloodGroups
                .Select(bg => new BloodGroupResponse
                {
                    Id = bg.Id,
                    GroupName = bg.GroupName
                })
                .ToListAsync();
        }

        public Task<BloodGroupResponse?> GetBloodGroupByIdAsync(int id)
        {
            var bloodGroup = context.BloodGroups
                .Where(bg => bg.Id == id)
                .Select(bg => new BloodGroupResponse
                {
                    Id = bg.Id,
                    GroupName = bg.GroupName
                })
                .FirstOrDefaultAsync();
            return bloodGroup;
        }


        public async Task<BloodGroupResponse> AddBloodGroupAsync(CreateBloodGroupRequest request)
        {
            // Check for duplicate
            var exists = await context.BloodGroups
        .AnyAsync(bg => bg.GroupName == request.GroupName);

            if (exists)
                return null;

            var bloodGroup = new BloodGroup
            {
                GroupName = request.GroupName
            };

            context.BloodGroups.Add(bloodGroup);
            await context.SaveChangesAsync();

            return new BloodGroupResponse
            {
                Id = bloodGroup.Id,
                GroupName = bloodGroup.GroupName
            };
        }

        public async Task<bool> UpdateBloodGroupAsync(int id, UpdateBloodGroupRequest request)
        {
            // Check if another blood group has the same name
            var exists = await context.BloodGroups
                .AnyAsync(bg => bg.GroupName == request.GroupName && bg.Id != id);

            if (exists)
                return false;

            var existingBloodGroup = await context.BloodGroups.FindAsync(id);
            if (existingBloodGroup == null)
                return false;

            existingBloodGroup.GroupName = request.GroupName;
            await context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteBloodGroupAsync(int id)
        {
            var bloodGroup = await context.BloodGroups.FindAsync(id);
            if (bloodGroup == null)
            {
                return false;
            }
            context.BloodGroups.Remove(bloodGroup);
            await context.SaveChangesAsync();
            return true;
        }
    }
}
