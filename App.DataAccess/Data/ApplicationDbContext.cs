using App.Models;
using Microsoft.EntityFrameworkCore;

namespace App.DataAccess.Data;

public class ApplicationDbContext : DbContext
{
    public DbSet<Category> Category { get; set; }
    public DbSet<FoodType> FoodTypes { get; set; }
    public DbSet<MenuItem>MenuItems { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

}
