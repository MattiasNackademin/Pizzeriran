using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InMemD8.Models;
using InMemD8.Services;
using Microsoft.AspNetCore.Identity;


namespace InMemD8.Data
{
    public static class DbInitializer
    {

        public static void Initialize(ApplicationDbContext context, UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole>roleManager, IngredientService ingredientService)
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
                var cheese = new Ingredient {Name = "Ost", Price = 10};
                var tomatoe = new Ingredient { Name = "Tomat", Price = 10 };
                var ham = new Ingredient { Name = "Skinka", Price = 10 };
                var pineapple = new Ingredient { Name = "Annanas", Price = 10 };
                var mushrooms = new Ingredient {Name = "Champinjoner", Price = 10 };
                var chicken = new Ingredient {Name = "Kyckling", Price = 10 };
                var meatsauce = new Ingredient {Name = "Köttfärssås", Price = 10 };
                var spagetti = new Ingredient {Name = "Spagetti", Price = 10 };


                //Category
                var pizza = new Category { Name = "Pizza" };
                var salad = new Category {Name = "Sallad"};
                var pasta = new Category {Name = "Pasta"};

                //Dish
                var cappricciosa = new Dish { Category = pizza,  Name = "Capricciosa", Price=79};
                var margaritha = new Dish { Category = pizza, Name = "Margaritha", Price =89};
                var hawaii = new Dish {Category = pizza, Name = "Hawaii", Price = 75};

                var chickensalad = new Dish {Category = salad, Name = "Kyckling Sallad", Price = 65};

                var pastabolognese = new Dish {Category = pasta, Name = "Bolognese", Price = 99};

                //Cappricciosa
                var capricciosaCheese = new DishIngredient {Dish = cappricciosa, Ingredient = cheese, Checkbox = true};
                var capricciosaTomatoe = new DishIngredient { Dish = cappricciosa, Ingredient = tomatoe, Checkbox = true };
                var capricciosaHam = new DishIngredient { Dish = cappricciosa, Ingredient = ham, Checkbox = true };
                var capricciosaMushroom = new DishIngredient { Dish = cappricciosa, Ingredient = mushrooms, Checkbox = true };

                cappricciosa.DishIngredients = new List<DishIngredient>();
                cappricciosa.DishIngredients.Add(capricciosaCheese);
                cappricciosa.DishIngredients.Add(capricciosaTomatoe);
                cappricciosa.DishIngredients.Add(capricciosaHam);
                cappricciosa.DishIngredients.Add(capricciosaMushroom);

                //Margaritha
                var margarithaCheese = new DishIngredient { Dish = margaritha, Ingredient = cheese, Checkbox = true };
                var margarithaTomatoe = new DishIngredient { Dish = margaritha, Ingredient = tomatoe, Checkbox = true };

                margaritha.DishIngredients = new List<DishIngredient>();
                margaritha.DishIngredients.Add(margarithaCheese);
                margaritha.DishIngredients.Add(margarithaTomatoe);

                //Hawaii
                var hawaiiCheese = new DishIngredient { Dish = hawaii, Ingredient = cheese, Checkbox = true };
                var hawaiiTomatoe = new DishIngredient { Dish = hawaii, Ingredient = tomatoe, Checkbox = true };
                var hawaiiHam = new DishIngredient { Dish = hawaii, Ingredient = ham, Checkbox = true };
                var hawaiiPineapple = new DishIngredient { Dish = hawaii, Ingredient = pineapple, Checkbox = true };

                hawaii.DishIngredients = new List<DishIngredient>();
                hawaii.DishIngredients.Add(hawaiiCheese);
                hawaii.DishIngredients.Add(hawaiiTomatoe);
                hawaii.DishIngredients.Add(hawaiiHam);
                hawaii.DishIngredients.Add(hawaiiPineapple);

                //Chicken Salad
                var chickenCheese = new DishIngredient { Dish = chickensalad, Ingredient = cheese, Checkbox = true };
                var chickenTomatoe = new DishIngredient { Dish = chickensalad, Ingredient = tomatoe, Checkbox = true };
                var chickenChicken = new DishIngredient { Dish = chickensalad, Ingredient = chicken, Checkbox = true };
                var chickenPineapple = new DishIngredient { Dish = chickensalad, Ingredient = pineapple, Checkbox = true };

                chickensalad.DishIngredients = new List<DishIngredient>();
                chickensalad.DishIngredients.Add(chickenCheese);
                chickensalad.DishIngredients.Add(chickenTomatoe);
                chickensalad.DishIngredients.Add(chickenChicken);
                chickensalad.DishIngredients.Add(chickenPineapple);
                
                // Spagetti Bolognese

                var pastaSpagetti = new DishIngredient {Dish = pastabolognese, Ingredient = spagetti, Checkbox = true };
                var pastameatsauce = new DishIngredient {Dish = pastabolognese, Ingredient = meatsauce, Checkbox = true };

                pastabolognese.DishIngredients = new List<DishIngredient>();
                pastabolognese.DishIngredients.Add(pastaSpagetti);
                pastabolognese.DishIngredients.Add(pastameatsauce);




                context.Dishes.Add(cappricciosa);
                context.Dishes.Add(margaritha);
                context.Dishes.Add(hawaii);
                context.Dishes.Add(chickensalad);
                context.Dishes.Add(pastabolognese);
                context.AddRange(tomatoe, ham, cheese, mushrooms, pineapple, chicken, spagetti, meatsauce);

                context.SaveChanges();

            }
        }
    }
}
