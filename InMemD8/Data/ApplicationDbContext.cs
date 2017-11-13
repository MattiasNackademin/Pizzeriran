﻿using InMemD8.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace InMemD8.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser> 
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<DishIngredient>()
                .HasKey(di => new {di.Id, di.IngredientId});

            builder.Entity<DishIngredient>()
                .HasOne(di => di.Dish)
                .WithMany(d => d.DishIngredients)
                .HasForeignKey(di => di.Id);

            builder.Entity<DishIngredient>()
                .HasOne(di => di.Ingredient)
                .WithMany(i => i.DishIngredients)
                .HasForeignKey(di => di.IngredientId);

           

            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
        public DbSet<Dish> Dishes { get; set; }
        public DbSet<Ingredient>Ingredients { get; set; }
        public DbSet<DishIngredient>DishIngredients { get; set;  }
        public DbSet<Category> Category { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<CartItemIngredient> CartItemIngredients { get; set; }

    }
    }

