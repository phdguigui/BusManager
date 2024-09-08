using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusManager.Model.Entities
{
    [Table("Trips")]
    public class Trip
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [ForeignKey("Bus")]
        public int BusId { get; set; }
        public Bus Bus { get; set; }

        [Required]
        [ForeignKey("DriverId")]
        public int DriverId { get; set; }
        public Driver Driver { get; set; }

        [Required]
        [ForeignKey("LineId")]
        public int LineId { get; set; }
        public Line Line { get; set; }

        [Required]
        public DateTime StartTime { get; set; }
        [Required]
        public DateTime EndTime { get; set; }
    }
}
