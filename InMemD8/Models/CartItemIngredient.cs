using System;
using InMemD8.Models;

namespace InMemD8
{
    public class CartItemIngredient
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public bool Selected { get; set; }
        public bool Included { get; set; }
    }
}