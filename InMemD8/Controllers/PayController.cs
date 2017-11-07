using InMemD8.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace InMemD8.Controllers
{
    public class PayController : Controller
    {
        private readonly CartService _service;
        private readonly UserManager<ApplicationUser> _userManager;

        public PayController(CartService service, UserManager<ApplicationUser> userManager)
        {
            _service = service;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var viewModel = new Payment();
            var user = _userManager.GetUserAsync(User).Result;

            viewModel.Postnr = user.Postnr;
            viewModel.Gata = user.Adress;
            viewModel.Namn = user.Förnamn + " " + user.Efternamn;
            viewModel.Tel = user.PhoneNumber;

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Index(Payment model)
        {
            if (ModelState.IsValid)
            {
                var total = _service.CalculateCartTotal();

               

                return View("PaySuccess", total);
            }

            return View(model);
        }
    }
}