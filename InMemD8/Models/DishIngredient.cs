using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Threading.Tasks;

namespace InMemD8.Models
{
    public class DishIngredient
    {
        public int Id { get; set; }
        public Dish Dish { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int IngredientId { get; set; }
        public Ingredient Ingredient { get; set; }

        public bool Checkbox { get; set; }
       


    }
}
