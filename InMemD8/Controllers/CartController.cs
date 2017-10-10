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

        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            var cartItem = _service.GetItem(id);

            var viewModel = new CartItemEditViewModel();
            viewModel.CartItem = cartItem;
            var extras = _context.Ingredients.Where(x => !cartItem.Ingredients.Any(k => k.Name.Equals(x.Name)))
                .ToList();

            var extrasViewModel = new List<CartItemIngredient>();
            foreach (var extra in extras)
            {
                extrasViewModel.Add(new CartItemIngredient(){Id = extra.IngredientId, Included = false, Name = extra.Name, Price = extra.Price,Selected = false});
            }

            viewModel.Extras = extrasViewModel;

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Edit(CartItemEditViewModel model)
        {
            if (model.Extras != null && model.Extras.Any(x => x.Selected))
            {
                _service.ChangeItem(model.CartItem, model.Extras.Where(x=> x.Selected).ToList());
            }
            else
            {
                _service.ChangeItem(model.CartItem);
            }

            return RedirectToAction("CartIndex");
        }
    }
}