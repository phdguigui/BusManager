using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusManager.Model.Entities
{
    [Table("Stops")]
    public class Stop
    {
        [Required]
        [ForeignKey("LineId")]
        public int LineId { get; set; }
        public Line Line { get; set; }

        [Required]
        [ForeignKey("StationId")]
        public int StationId { get; set; }
        public Station Station { get; set; }

        [Required]
        public DateTime Hour { get; set; }
    }
}
