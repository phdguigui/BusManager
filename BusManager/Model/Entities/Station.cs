using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusManager.Model.Entities
{
    [Table("Stations")]
    public class Station
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string Number { get; set; }
        [Required]
        public bool Active { get; set; }

        public ICollection<Stop> Stops { get; set; }
    }
}
