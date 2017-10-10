using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InMemD8.Models
{
    public class CartItemEditViewModel
    {
        public CartItem CartItem { get; set; }
        public List<CartItemIngredient> Extras { get; set; }
    }
}
