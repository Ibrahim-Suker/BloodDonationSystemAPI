using System.ComponentModel.DataAnnotations;

namespace BloodDonationSystemAPI.Models
{
    public class BloodGroup
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string GroupName { get; set; }= string.Empty;

        public ICollection<BloodRequest> BloodRequests { get; set; } = new List<BloodRequest>();

    }
}
