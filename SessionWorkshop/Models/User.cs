using System.ComponentModel.DataAnnotations;

public class User
{
    [Required(ErrorMessage ="Please Enter in a name")]
    public string Name {get; set;}
    public int Number{get; set;}
}