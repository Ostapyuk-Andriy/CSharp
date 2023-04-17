#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WeddingPlanner.Models;
public class User
{
    [Key]
    public int UserId { get; set; }

    [Required]
    [MinLength(2, ErrorMessage ="First Name must be at leat 2 characters")]
    public string FirstName { get; set; } 

    [Required]
    [MinLength(2, ErrorMessage ="Last Name must be at leat 2 characters")]
    public string LastName { get; set; } 

    [Required]
    [EmailAddress]
    [UniqueEmail]
    public string? Email { get; set; } 

    [Required]
    [DataType(DataType.Password)]
    [MinLength(8, ErrorMessage ="Password must be at leat 8 characters")]
    public string Password { get; set; } 

    [Required]
    [NotMapped]
    [DataType(DataType.Password)]
    [Compare("Password")]
    [Display(Name = "Confirm Password")]
    public string Confirm { get; set; } 

    public List<Attendance> AttendedWeddings { get; set; } = new List<Attendance>();

    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
}

public class UniqueEmailAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if(value == null)
        {
            return new ValidationResult("Email is Required");
        }
        MyContext _context = (MyContext)validationContext.GetService(typeof(MyContext));
        if(_context.Users.Any(u => u.Email == value.ToString()))
        {
            return new ValidationResult("Email must be unique");
        }
        else
        {
            return ValidationResult.Success;
        }
    }
}
