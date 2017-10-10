using System;
using System.Collections.Generic;
using System.Linq;
using InMemD8.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace InMemD8
{
    public class CartService
    {
        private readonly IHttpContextAccessor _accessor;

        public CartService(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }

        public void AddItem(Dish item)
        {
            if (_accessor.HttpContext.Session.GetString("CartItems") == null)
            {

                var list = new List<CartItem>();

                list.Add(new CartItem()
                {
                    Id = Guid.NewGuid(),
                    Name = item.Name,
                    Price = item.Price,
                    Ingredients = item.DishIngredients.Select(x=> new CartItemIngredient(){Id = x.Ingredient.IngredientId, Included = true, Name = x.Ingredient.Name, Price = x.Ingredient.Price, Selected = true}).ToList(),
                    CategoryName = item.Category.Name
                });

                var serialized = JsonConvert.SerializeObject(list);
                _accessor.HttpContext.Session.SetString("CartItems", serialized);
            }
            else
            {
                var list = _accessor.HttpContext.Session.GetString("CartItems");
                var deserialized = JsonConvert.DeserializeObject<List<CartItem>>(list);

                deserialized.Add(new CartItem()
                {
                    Id = Guid.NewGuid(),
                    Name = item.Name,
                    Price = item.Price,
                    Ingredients = item.DishIngredients.Select(x => new CartItemIngredient() { Id = x.Ingredient.IngredientId, Included = true, Name = x.Ingredient.Name, Price = x.Ingredient.Price, Selected = true }).ToList(),
                    CategoryName = item.Category.Name
                    
                });

                var serialized = JsonConvert.SerializeObject(deserialized);
                _accessor.HttpContext.Session.SetString("CartItems", serialized);
            }
        }

        public List<CartItem> GetAllItems()
        {
            var list = _accessor.HttpContext.Session.GetString("CartItems");
            var deserialized = JsonConvert.DeserializeObject<List<CartItem>>(list);

            return deserialized;
        }

        public CartItem GetItem(Guid id)
        {
            var list = _accessor.HttpContext.Session.GetString("CartItems");
            var deserialized = JsonConvert.DeserializeObject<List<CartItem>>(list);

            return deserialized.FirstOrDefault(x => x.Id == id);
        }

        public void RemoveItem(Guid id)
        {
            var list = _accessor.HttpContext.Session.GetString("CartItems");
            var deserialized = JsonConvert.DeserializeObject<List<CartItem>>(list);

            if (deserialized.Any(x => x.Id == id))
            {
                deserialized.RemoveAll(x => x.Id == id);

                var serialized = JsonConvert.SerializeObject(deserialized);
                _accessor.HttpContext.Session.SetString("CartItems", serialized);
            }
        }

        public void ChangeItem(CartItem item, List<CartItemIngredient> extras = null)
        {
            var list = _accessor.HttpContext.Session.GetString("CartItems");
            var deserialized = JsonConvert.DeserializeObject<List<CartItem>>(list);

            var itemToChange = deserialized.FirstOrDefault(x => x.Id == item.Id);

            foreach (var ingredient in item.Ingredients)
            {
                if (!ingredient.Selected)
                {
                    itemToChange.Ingredients.FirstOrDefault(x => x.Id == ingredient.Id).Selected = false;
                }
                else
                {
                    itemToChange.Ingredients.FirstOrDefault(x => x.Id == ingredient.Id).Selected = true;
                }
            }

            if (extras != null)
            {
                itemToChange.Ingredients.AddRange(extras);
            }

            var serialized = JsonConvert.SerializeObject(deserialized);
            _accessor.HttpContext.Session.SetString("CartItems", serialized);
        }

        public decimal CalculateCartTotal()
        {
            var list = _accessor.HttpContext.Session.GetString("CartItems");
            var deserialized = JsonConvert.DeserializeObject<List<CartItem>>(list);


            decimal total = 0;

            foreach (CartItem item in deserialized)
            {
                total += item.Price;
                total += item.Ingredients.Where(x => !x.Included && x.Selected).Sum(x=> x.Price);
            }

            return total;
        }

        public void ClearCart()
        {
            _accessor.HttpContext.Session.Clear();
        }

    }
}