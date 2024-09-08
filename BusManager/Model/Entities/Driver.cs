using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusManager.Model.Entities
{
    [Table("Drivers")]
    public class Driver
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string CPF { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        public DateTime Birthday { get; set; }
        public DateTime HireDate { get; set; }
        [Required]
        public bool Active { get; set; }
    }
}
