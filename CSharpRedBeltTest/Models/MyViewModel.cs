#pragma warning disable CS8618

namespace CSharpRedBelt.Models;
public class MyViewModel
{
    public User User {get; set;}
    public List<User> AllUsers {get; set;}

    public Coupon Coupon {get; set;}
    public List<Coupon> AllCoupons {get; set;}
    
    public UsedCoupon UsedCoupon {get; set;}
    public List<UsedCoupon> AllUsedCoupons {get; set;} 
}