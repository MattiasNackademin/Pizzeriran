using System;
using System.Collections.Generic;
using System.Text;
using InMemD8.Models;

namespace InMemD8
{
    public class CartItem
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Decimal Price { get; set; }
        public string CategoryName { get; set; }
        public List<CartItemIngredient> Ingredients { get; set; }
    }
}
//public int CartItemId { get; set; }
//public int CartId { get; set; }
//public Cart Cart { get; set; }
//public Dish Dish { get; set; }
//public int DishId { get; set; }
//public int Qantity { get; set; }
//public List<CartItemIngredienttemp> CartItemIngredients { get; set; }
