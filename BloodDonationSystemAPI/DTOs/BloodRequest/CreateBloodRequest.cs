using System.ComponentModel.DataAnnotations;

namespace BloodDonationSystemAPI.DTOs.BloodRequest
{
    public class CreateBloodRequest
    {
        [Required]
        public int QuantityUnits { get; set; }

        public string? Reason { get; set; }

        public string Status { get; set; } = "Pending";
        public int CityId { get; set; }
        public int BloodGroupId { get; set; }
    }

}
