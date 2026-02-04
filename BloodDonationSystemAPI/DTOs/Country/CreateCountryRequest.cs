using System.ComponentModel.DataAnnotations;

namespace BloodDonationSystemAPI.DTOs.Country
{
    public class CreateCountryRequest
    {
        [Required]
        public string Name { get; set; } = string.Empty;
    }
}
