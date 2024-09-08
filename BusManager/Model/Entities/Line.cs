using BusManager.Model.Entities;
using System.ComponentModel.DataAnnotations;

public class Line
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public string Code { get; set; }
    [Required]
    public string Origin { get; set; }
    [Required]
    public string Destiny { get; set; }
    [Required]
    public string Description { get; set; }
    [Required]
    public bool Active { get; set; }

    public ICollection<Stop> Stops { get; set; }
}