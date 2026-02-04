using System.ComponentModel.DataAnnotations;

namespace BloodDonationSystemAPI.DTOs.BloodRequest
{
    public class UpdateBloodRequest
    {
        [Required]
        public string Status { get; set; } = string.Empty;
    }

}
