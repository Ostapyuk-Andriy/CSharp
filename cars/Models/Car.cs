#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;

namespace cars.Models;
public class Car
{
    [Key]
    public int CarId { get; set; }
    public string Make { get; set; } 
    public string Model { get; set; }
    public int Year { get; set; }
    
    // Setting deafault timestamps
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
}
                
