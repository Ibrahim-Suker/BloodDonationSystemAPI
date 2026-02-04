using BloodDonationSystemAPI.Data;
using BloodDonationSystemAPI.DTOs.BloodRequest;
using BloodDonationSystemAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BloodDonationSystemAPI.Services
{
    public class BloodRequestService : IBloodRequestService
    {
        private readonly AppDbContext context;
        public BloodRequestService(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<List<BloodRequestResponse>> GetBloodRequestsAsync()
        {
            return await context.BloodRequests
                .Include(br => br.City)
                .Include(br => br.BloodGroup)
                .Select(br => new BloodRequestResponse
                {
                    Id = br.Id,
                    QuantityUnits = br.QuantityUnits,
                    Reason = br.Reason,
                    Status = br.Status,
                    RequestedAt = br.RequestedAt,
                    FulfilledAt = br.FulfilledAt,
                    CityId = br.CityId ?? 0, // لو حابب تسيب 0 بدل null
                    CityName = br.City != null ? br.City.Name : string.Empty,
                    BloodGroupId = br.BloodGroupId ?? 0,
                    GroupName = br.BloodGroup != null ? br.BloodGroup.GroupName : string.Empty
                }).ToListAsync();
        }

        public async Task<BloodRequestResponse?> GetBloodRequestByIdAsync(int id)
        {
            var bloodRequest = await context.BloodRequests
                .Include(br => br.City)
                .Include(br => br.BloodGroup)
                .Where(br => br.Id == id)
                .Select(br => new BloodRequestResponse
                {
                    Id = br.Id,
                    QuantityUnits = br.QuantityUnits,
                    Reason = br.Reason,
                    Status = br.Status,
                    RequestedAt = br.RequestedAt,
                    FulfilledAt = br.FulfilledAt,
                    CityId = br.CityId ?? 0,
                    CityName = br.City != null ? br.City.Name : string.Empty,
                    BloodGroupId = br.BloodGroupId ?? 0,
                    GroupName = br.BloodGroup != null ? br.BloodGroup.GroupName : string.Empty
                })
                .FirstOrDefaultAsync();

            return bloodRequest;
        }



        public async Task<BloodRequestResponse> AddBloodRequestAsync(CreateBloodRequest request)
        {
            // تحقق من وجود الـ City و BloodGroup
            var city = await context.Cities.FindAsync(request.CityId);
            if (city == null)
                throw new InvalidOperationException($"City with id {request.CityId} not found.");

            var bloodGroup = await context.BloodGroups.FindAsync(request.BloodGroupId);
            if (bloodGroup == null)
                throw new InvalidOperationException($"Blood group with id {request.BloodGroupId} not found.");

            var newRequest = new BloodRequest
            {
                QuantityUnits = request.QuantityUnits,
                Reason = request.Reason,
                Status = string.IsNullOrEmpty(request.Status) ? "Pending" : request.Status,
                RequestedAt = DateTime.UtcNow,
                CityId = request.CityId,
                BloodGroupId = request.BloodGroupId
            };

            context.BloodRequests.Add(newRequest);
            await context.SaveChangesAsync();

            return new BloodRequestResponse
            {
                Id = newRequest.Id,
                QuantityUnits = newRequest.QuantityUnits,
                Reason = newRequest.Reason,
                Status = newRequest.Status,
                RequestedAt = newRequest.RequestedAt,
                FulfilledAt = newRequest.FulfilledAt,
                CityId = newRequest.CityId ?? 0,
                CityName = city.Name,
                BloodGroupId = newRequest.BloodGroupId ?? 0,
                GroupName = bloodGroup.GroupName
            };
        }

        public async Task<bool> UpdateBloodRequestAsync(int id, UpdateBloodRequest request)
        {
            var existingRequest = await context.BloodRequests.FindAsync(id);
            if (existingRequest == null)
                return false;

            // فقط تحديث الـ Status
            existingRequest.Status = request.Status;

            // إذا كان الـ Status هو "Fulfilled" ممكن تعيين FulfilledAt
            if (request.Status.Equals("Fulfilled", StringComparison.OrdinalIgnoreCase))
            {
                existingRequest.FulfilledAt = DateTime.UtcNow;
            }

            await context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> DeleteBloodRequestAsync(int id)
        {
            var existingRequest = await context.BloodRequests.FindAsync(id);
            if (existingRequest == null)
                return false;

            context.BloodRequests.Remove(existingRequest);
            await context.SaveChangesAsync();
            return true;
        }


    }
}
