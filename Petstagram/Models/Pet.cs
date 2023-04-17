

using System.ComponentModel.DataAnnotations;

public class Pet
{
    [Required(ErrorMessage ="Please Enter in a name")]
    public string Name {get; set;}
    [Range(0,Int32.MaxValue, ErrorMessage ="Please enter in age")]
    public int? Age {get; set;}
    [MinLength(3, ErrorMessage ="Please enter in Type")]
    public string Type {get; set;}
    [Required(ErrorMessage ="Please enter in Hair Color")]
    public string HairColor {get; set;}
}