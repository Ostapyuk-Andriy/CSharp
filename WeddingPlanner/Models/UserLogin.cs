#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;

namespace WeddingPlanner.Models;
public class UserLogin
{
    [Required]
    [EmailAddress]
    [Display(Name = "Email")]
    public string? EmailLogin { get; set; } 

    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "Password")]
    public string PasswordLogin { get; set; } 

    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
}