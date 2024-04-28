#pragma warning disable CS8618
using Microsoft.EntityFrameworkCore;
namespace LoginAndRegistration.Models;

public class MyContext : DbContext 
{ 
    public MyContext(DbContextOptions options) : base(options) { }
    // the "Monsters" table name will come from the DbSet property name
    public DbSet<User> Users { get; set; } 
}