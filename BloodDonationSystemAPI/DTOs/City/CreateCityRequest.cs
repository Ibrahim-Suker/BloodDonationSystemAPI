using System.ComponentModel.DataAnnotations;

namespace BloodDonationSystemAPI.DTOs.City
{
    public class CreateCityRequest
    {
        [Required]   
        public string Name { get; set; } = string.Empty;

        [Required]
        public int CountryId { get; set; }
    }
}
