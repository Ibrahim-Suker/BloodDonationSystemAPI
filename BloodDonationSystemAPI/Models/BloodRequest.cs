using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BloodDonationSystemAPI.Models
{
    public class BloodRequest
    {
        [Key]
        public int Id { get; set; }

        [Range(1, int.MaxValue)]
        public int QuantityUnits { get; set; }

        public string? Reason { get; set; }

        [Required]
         public string Status { get; set; } = string.Empty;

        public DateTime RequestedAt { get; set; } = DateTime.UtcNow;
        public DateTime? FulfilledAt { get; set; }


        public int? CityId { get; set; }
        [ForeignKey(nameof(CityId))]
        public City? City { get; set; }

        public int? BloodGroupId { get; set; }
        [ForeignKey(nameof(BloodGroupId))]
        public BloodGroup? BloodGroup { get; set; }
    }
}
