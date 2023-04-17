#pragma warning disable CS8618
using Microsoft.EntityFrameworkCore;
namespace CSharpRedBelt2.Models;
public class MyContext : DbContext 
{    
    public MyContext(DbContextOptions options) : base(options) { }    
    public DbSet<User>? Users { get; set; } 
    public DbSet<Post>? Posts{ get; set; } 
    public DbSet<LikedPost>? Likes { get; set; } 

}