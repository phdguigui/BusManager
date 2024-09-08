using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusManager.Model.Entities
{
    [Table("Buses")]
    public class Bus
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Model { get; set; }
        [Required]
        public string Plate { get; set; }
        [Required]
        public bool Active { get; set; }
    }
}
