#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CSharpRedBelt.Models;
public class UsedCoupon
{
    [Key]
    public int UsedCouponId { get; set; }

    public int UserId { get; set; }
    public int CouponId { get; set; }

    public User? User { get; set; } 
    public Coupon? Coupon { get; set; } 

    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
}