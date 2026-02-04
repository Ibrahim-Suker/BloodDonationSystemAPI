using System.ComponentModel.DataAnnotations;

namespace BloodDonationSystemAPI.DTOs.BloodGroup
{
    public class CreateBloodGroupRequest
    {
        [Required]
        [MaxLength(10)]
        public string GroupName { get; set; } = string.Empty;

    }
}
