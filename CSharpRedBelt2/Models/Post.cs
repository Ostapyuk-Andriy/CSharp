#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSharpRedBelt2.Models;
public class Post
{
    [Key]
    public int PostId { get; set; }

    [Required(ErrorMessage ="Post image is required")]
    public string Image { get; set; } 

    [Required(ErrorMessage ="Title is Required")]
    public string Title { get; set; } 

    [Required(ErrorMessage ="Medium is Required")]
    public string Medium { get; set; } 

    
    public bool forSale { get; set; }

    public int CreatorId { get; set; }
    public User? Creator { get; set; }
    public List<LikedPost> LikedPosts { get; set; } = new List<LikedPost>();


    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
}