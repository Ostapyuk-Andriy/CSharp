#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSharpRedBelt.Models;
public class User
{
    [Key]
    public int UserId { get; set; }

    [Required]
    [MinLength(3, ErrorMessage ="First Name must be at leat 3 characters")]
    public string UserName { get; set; } 

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

    public List<UsedCoupon> CreatedCoupons { get; set; } = new List<UsedCoupon>();

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
