using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InMemD8.Models;
using Microsoft.AspNetCore.Identity;

namespace InMemD8.Data
{
    public static class DbInitializer
    {

        public static void Initialize(ApplicationDbContext context, UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole>roleManager)
        {
            var aUser = new ApplicationUser();
            aUser.UserName = "student@test.com";
            aUser.Email = "student@test.com";
            var r = userManager.CreateAsync(aUser, "Pa$$w0rd").Result;

            var adminRole = new IdentityRole {Name = "Admin"};
            var roleResult = roleManager.CreateAsync(adminRole).Result;

            var adminUser = new ApplicationUser();
            adminUser.UserName = "admin@test.com";
            adminUser.Email = "admin@test.com";
            var adminUserResult = userManager.CreateAsync(adminUser, "Pa$$w0rd").Result;
            userManager.AddToRoleAsync(adminUser, "Admin");

            if (context.Dishes.ToList().Count == 0)
            {
                var cheese = new Ingredient {Name = "Cheese"};
                var tomatoe = new Ingredient { Name = "Tomatoe" };
                var ham = new Ingredient { Name = "Ham" };
                var pineapple = new Ingredient { Name = "Pineapple" };
                var mushrooms = new Ingredient {Name = "Mushrooms"};




                var cappricciosa = new Dish {Name = "Capricciosa", Price=79};
                var margaritha = new Dish { Name = "Margaritha", Price =89};
                var hawaii = new Dish { Name = "Hawaii", Price = 75};

                //Cappricciosa
                var capricciosaCheese = new DishIngredient {Dish = cappricciosa, Ingredient = cheese};
                var capricciosaTomatoe = new DishIngredient { Dish = cappricciosa, Ingredient = tomatoe };
                var capricciosaHam = new DishIngredient { Dish = cappricciosa, Ingredient = ham };
                var capricciosaMushroom = new DishIngredient { Dish = cappricciosa, Ingredient = mushrooms };

                cappricciosa.DishIngredients = new List<DishIngredient>();
                cappricciosa.DishIngredients.Add(capricciosaCheese);
                cappricciosa.DishIngredients.Add(capricciosaTomatoe);
                cappricciosa.DishIngredients.Add(capricciosaHam);
                cappricciosa.DishIngredients.Add(capricciosaMushroom);

                //Margaritha
                var margarithaCheese = new DishIngredient { Dish = margaritha, Ingredient = cheese };
                var margarithaTomatoe = new DishIngredient { Dish = margaritha, Ingredient = tomatoe };

                margaritha.DishIngredients = new List<DishIngredient>();
                margaritha.DishIngredients.Add(margarithaCheese);
                margaritha.DishIngredients.Add(margarithaTomatoe);

                //Hawaii
                var hawaiiCheese = new DishIngredient { Dish = hawaii, Ingredient = cheese };
                var hawaiiTomatoe = new DishIngredient { Dish = hawaii, Ingredient = tomatoe };
                var hawaiiHam = new DishIngredient { Dish = hawaii, Ingredient = ham };
                var hawaiiPineapple = new DishIngredient { Dish = hawaii, Ingredient = pineapple };

                hawaii.DishIngredients = new List<DishIngredient>();
                hawaii.DishIngredients.Add(hawaiiCheese);
                hawaii.DishIngredients.Add(hawaiiTomatoe);
                hawaii.DishIngredients.Add(hawaiiHam);
                hawaii.DishIngredients.Add(hawaiiPineapple);



                context.Dishes.Add(cappricciosa);
                context.Dishes.Add(margaritha);
                context.Dishes.Add(hawaii);
                context.AddRange(tomatoe, ham, cheese, mushrooms, pineapple);

                context.SaveChanges();

            }
        }
    }
}
