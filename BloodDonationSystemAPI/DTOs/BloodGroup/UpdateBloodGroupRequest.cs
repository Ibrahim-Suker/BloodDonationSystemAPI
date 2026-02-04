using System.ComponentModel.DataAnnotations;

namespace BloodDonationSystemAPI.DTOs.BloodGroup
{
    public class UpdateBloodGroupRequest
    {
        [Required]
        [MaxLength(10)]
        [RegularExpression(@"^(A|B|AB|O)[+-]$")]

        public string GroupName { get; set; } = string.Empty;
    }
}
