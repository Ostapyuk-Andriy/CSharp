#pragma warning disable CS8618
using Microsoft.EntityFrameworkCore;
namespace cars.Models;
public class MyContext : DbContext 
{    
    public MyContext(DbContextOptions options) : base(options) { }    
    public DbSet<Car> Cars { get; set; } 
}
