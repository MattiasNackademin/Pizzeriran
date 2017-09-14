using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Threading.Tasks;

namespace InMemD8.Models
{
    public class Cart
    {
        public int CartId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public int ApplicationUserId { get; set; }
        public List<CartItem> CartItems{ get; set; }


       
    }

   
}
