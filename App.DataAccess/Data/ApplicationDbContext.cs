﻿using App.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace App.DataAccess.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public DbSet<Category> Category { get; set; }
    public DbSet<FoodType> FoodTypes { get; set; }
    public DbSet<MenuItem>MenuItems { get; set; }
    public DbSet<ApplicationUser> ApplicationUsers { get; set; }
    public DbSet<ShoppingCart>ShoppingCarts { get; set; }
    public DbSet<OrderDetails> OrderDetails { get; set; }
    public DbSet<OrderHeader> OrderHeaders { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

}
