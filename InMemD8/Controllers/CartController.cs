using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InMemD8.Data;
using InMemD8.Models;
using InMemD8.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InMemD8.Controllers
{
    public class CartController : Controller
    {
        private readonly CartService _service;
        private readonly ApplicationDbContext _context;
        public CartController(CartService cartService, ApplicationDbContext context)
        {
            _service = cartService;
            _context = context;
        }

        public IActionResult AddToCart(int id)
        {
            var dish = _context.Dishes.Include(x => x.Category).Include(x => x.DishIngredients)
                .ThenInclude(x => x.Ingredient).FirstOrDefault(x => x.DishId == id);

            _service.AddItem(dish);

            return RedirectToAction("Index", "Dishes");
        }

        public IActionResult CartIndex()
        {
            var total = _service.CalculateCartTotal();
            var cartItems = _service.GetAllItems();

            var viewModel = new CartViewModel();

            viewModel.CartItems = cartItems;
            viewModel.Total = total;

            return View(viewModel);
        }
    }
}