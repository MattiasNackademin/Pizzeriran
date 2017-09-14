using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InMemD8.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InMemD8.Services
{
    public class IngredientService
    {
        private readonly ApplicationDbContext _context;

        public IngredientService(ApplicationDbContext context)
        {
            _context = context;
        }
        // Get all Ingredients 
        public List<Models.Ingredient> GetIngredients()
        {
            return _context.Ingredients.ToList(); ;
        }

        // Get Checked Ingredients
        public string AddedIngredients(int id)
        {
            var ingredients = _context.DishIngredients.Include(di => di.Ingredient).Where(di => di.Id == id && di.Checkbox);
            string checkedIngredients = "";
            foreach (var i in ingredients)
            {
                checkedIngredients += i.Ingredient.Name + " ";
            }
            return checkedIngredients;
        }
    }
}