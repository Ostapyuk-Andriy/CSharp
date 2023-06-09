#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;

namespace ProductsAndCategories.Models;
public class Product
{
    [Key]
    public int ProductId { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public string Description { get; set; }

    [Required]
    [Range(1, int.MaxValue, ErrorMessage ="Price can't be zero")]
    public int Price { get; set; }

    public List<Association> Associations { get; set; } = new List<Association>();
    
    // Setting deafault timestamps
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
}