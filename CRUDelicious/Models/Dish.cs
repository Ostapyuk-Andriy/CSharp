#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;

namespace CRUDelicious.Models;
public class Dish
{
    [Key]
    public int DishId { get; set; }
    public string DishName { get; set; }
    public string ChefName { get; set; } 
    public int Tastiness { get; set; }
    public int Calories { get; set; }
    public string Description { get; set; }
    
    // Setting deafault timestamps
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
}