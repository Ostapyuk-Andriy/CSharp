#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;

namespace ChefsNDishes.Models;
public class Chef
{
    [Key]
    public int ChefId { get; set; }

    [Required]
    public string FirstName { get; set; }

    [Required]
    public string LastName { get; set; }

    [Required]
    [MinAge(18)]
    public DateTime DateOfBirth {get; set;}

    public List<Dish> AllDishes {get; set;} = new List<Dish>();
    
    // Setting deafault timestamps
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
}

public class MinAgeAttribute : ValidationAttribute
{
    private readonly int _minimumAge;
    public MinAgeAttribute(int minimumAge)
    {
        _minimumAge = minimumAge;
    }
    protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
    {
        var Dob = (DateTime)value;
        var age = DateTime.Today.Year - Dob.Year;
        if(Dob > DateTime.Today.AddYears(-age))
        {
            age --;
        }
        if(age < _minimumAge)
        {
            return new ValidationResult($"You must be at least {_minimumAge} years old to register");
        }
        return ValidationResult.Success;
    }
}