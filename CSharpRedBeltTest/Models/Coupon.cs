#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSharpRedBelt.Models;
public class Coupon
{
    [Key]
    public int CouponId { get; set; }

    [Required]
    [MinLength(4, ErrorMessage ="Code must be at leat 4 characters")]
    public string Code { get; set; } 

    [Required(ErrorMessage ="Website is Required")]
    public string Website { get; set; } 

    [Required(ErrorMessage ="Description is Required")]
    [MinLength(10, ErrorMessage ="Description must be at leat 10 characters")]
    public string Description { get; set; } 

    public int CreatorId { get; set; }
    public User? Creator { get; set; }
    public List<UsedCoupon> CouponsUsed { get; set; } = new List<UsedCoupon>();


    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
}