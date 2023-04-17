#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;

namespace ChefsNDishes.Models;
public class Dish
{
    [Key]
    public int DishId { get; set; }

    [Required]
    public string DishName { get; set; }

    [Required]
    [Range(1, 5)]
    public int Tastiness { get; set; }

    [Required]
    [Range(1, int.MaxValue)]
    public int Calories { get; set; }

    public int ChefId {get; set;}

    public Chef? Chef {get; set;}
    
    // Setting deafault timestamps
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
}