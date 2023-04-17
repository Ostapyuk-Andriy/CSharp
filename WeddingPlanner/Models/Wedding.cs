#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace WeddingPlanner.Models;
public class Wedding
{
    [Key]
    public int WeddingId { get; set; }

    [Required]
    public string WedderOne { get; set; } 

    [Required]
    public string WedderTwo { get; set; } 

    [Required]
    [FutureDate]
    public DateTime WeddDate { get; set; }

    [Required]
    public string WeddAddress { get; set; }
    public int PlannerId { get; set; }
    public List<Attendance> WeddingAttendees { get; set; } = new List<Attendance>();


    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
}

public class FutureDateAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if(((DateTime)value) <= DateTime.Today)
        {
            return new ValidationResult("Only future dates are allowed");
        }
        return ValidationResult.Success;
    }
}