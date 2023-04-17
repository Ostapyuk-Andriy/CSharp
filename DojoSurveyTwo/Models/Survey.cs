using System.ComponentModel.DataAnnotations;

public class Survey
{
    [Required(ErrorMessage="Name is required!")]
    [MinLength(2, ErrorMessage ="Name Should be at least 2 characters long")]
    public string Name{get; set;}
    [Required(ErrorMessage ="Please select a Location")]
    public string Location{get; set;}
    [Required(ErrorMessage ="Please select a Language")]
    public string Language{get; set;}
    
    [MinLength(20, ErrorMessage ="Must be at least 20")]
    public string? Comment{get; set;}

}