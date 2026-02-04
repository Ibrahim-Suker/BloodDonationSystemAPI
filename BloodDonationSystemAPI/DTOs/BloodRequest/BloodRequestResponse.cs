namespace BloodDonationSystemAPI.DTOs.BloodRequest
{
    public class BloodRequestResponse
    {
        public int Id { get; set; }
        public int QuantityUnits { get; set; }
        public string? Reason { get; set; }
        public string Status { get; set; } = string.Empty;

        public DateTime RequestedAt { get; set; } = DateTime.UtcNow;
        public DateTime? FulfilledAt { get; set; }

        public int? CityId { get; set; }
        public string CityName { get; set; } = string.Empty;
        public int? BloodGroupId { get; set; }
        public string GroupName { get; set;} = string.Empty;
    }

}
