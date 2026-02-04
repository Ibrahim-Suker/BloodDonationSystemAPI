using System.ComponentModel.DataAnnotations;

namespace BloodDonationSystemAPI.Models
{
    public class Country
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        public ICollection<City> Cities { get; set; } = new List<City>();
    }

}
