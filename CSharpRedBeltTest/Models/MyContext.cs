#pragma warning disable CS8618
using Microsoft.EntityFrameworkCore;
namespace CSharpRedBelt.Models;
public class MyContext : DbContext 
{    
    public MyContext(DbContextOptions options) : base(options) { }    
    public DbSet<User>? Users { get; set; } 
    public DbSet<Coupon>? Coupons{ get; set; } 
    public DbSet<UsedCoupon>? Usages { get; set; } 

}