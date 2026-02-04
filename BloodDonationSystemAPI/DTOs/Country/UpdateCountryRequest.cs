using System.ComponentModel.DataAnnotations;

namespace BloodDonationSystemAPI.DTOs.Country
{
    public class UpdateCountryRequest
    {
        [Required]
        public string Name { get; set; } = string.Empty;
    }
}
