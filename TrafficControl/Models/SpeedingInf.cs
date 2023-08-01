using System.ComponentModel.DataAnnotations;

namespace TrafficControl.Models
{
    public class SpeedingInf
    {
        [Required]
        public DateTime date { get; set; }

        [Required]
        public string? vehicleNum { get; set; }

        [Required]
        public double speed { get; set; }
    }
}
