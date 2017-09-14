using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InMemD8.Models
{
    public class CartViewModel
    {
        public decimal Total { get; set; }
        public List<CartItem> CartItems { get; set; }

    }
}
