using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using InMemD8.Data;
using InMemD8.Models;
using Microsoft.AspNetCore.Http;
using InMemD8.Services;

namespace InMemD8.Controllers
{
    public class DishesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IngredientService _ingredientService;

        public DishesController(ApplicationDbContext context, IngredientService ingredientService)
        {
            _context = context;
            _ingredientService = ingredientService;
        }

        // GET: Dishes
        public async Task<IActionResult> Index()
        {
            var catList = _context.Category.ToList();

            //var dishes = await _context.
            return View(await _context.Dishes
                .Include(d => d.DishIngredients)
                .ThenInclude(di => di.Ingredient)
                .ToListAsync());
        }

        // GET: Dishes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var catList = _context.Category.ToList();
            var dish = await _context.Dishes
                .Include(d => d.DishIngredients)
                .ThenInclude(di => di.Ingredient)
                .SingleOrDefaultAsync(m => m.DishId == id);

            if (dish == null)
            {
                return NotFound();
            }

            return View(dish);
        }

        // GET: Dishes/Create
        public IActionResult Create()
        {
            //ViewData["ExtraList"] = new SelectList(_context.Ingredients, "IngredientId", "Name");
            //var dish = 0; //= await _context.Dishes.SingleOrDefaultAsync(m => m.DishId == id);
            ViewData["CatList"] = new SelectList(_context.Category, "CategoryId", "Name");
            return View();
        }

        // POST: Dishes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DishId,Name,Price, CategoryId")] Dish dish, IFormCollection form)
        {
            if (ModelState.IsValid)
            {
               
                foreach (var ingredient in _ingredientService.GetIngredients())
                {
                    var dishIngredient = new DishIngredient
                    {
                        Ingredient = ingredient,
                        Dish = dish,
                        Checkbox = form.Keys.Any(x => x == $"checkboxes-{ingredient.IngredientId}")
                    };
                    _context.DishIngredients.Add(dishIngredient);
                }

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(dish);
        }

        // GET: Dishes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

           
            var dish = await _context.Dishes
                .Include(d => d.DishIngredients)
                .ThenInclude(di => di.Ingredient)
                .SingleOrDefaultAsync(m => m.DishId == id);

            ViewData["CatList"] = new SelectList(_context.Category, "CategoryId", "Name", dish.CategoryId);
            //
            if (dish == null)
            {
                return NotFound();
            }
            return View(dish);
        }

        // POST: Dishes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DishId,Name,Price,CategoryId")] Dish dish, IFormCollection form)
        {
            if (id != dish.DishId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var dishToEdit = await _context.Dishes.
                        Include(d => d.DishIngredients).
                        SingleOrDefaultAsync(m => m.DishId == id);
                    dishToEdit.Name = dish.Name;
                    dishToEdit.Price = dish.Price;
                    dishToEdit.CategoryId = dish.CategoryId;
                    foreach (var dishIngredient in dishToEdit.DishIngredients)
                    {
                        _context.Remove(dishIngredient);
                    }

                    await _context.SaveChangesAsync();
                   
                    foreach (var ingredient in _ingredientService.GetIngredients())
                    {
                        var dishIngredient = new DishIngredient
                        {
                            Ingredient = ingredient,
                            Dish = dish,
                            Checkbox = form.Keys.Any(x => x == $"checkboxes-{ingredient.IngredientId}")
                        };

                        //_context.Update(newDish);
                        _context.DishIngredients.Add(dishIngredient);
                        await _context.SaveChangesAsync();
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DishExists(dish.DishId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(dish);
        }

        // GET: Dishes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dish = await _context.Dishes
                .SingleOrDefaultAsync(m => m.DishId == id);
            if (dish == null)
            {
                return NotFound();
            }

            return View(dish);
        }

        // POST: Dishes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dish = await _context.Dishes.SingleOrDefaultAsync(m => m.DishId == id);
            _context.Dishes.Remove(dish);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DishExists(int id)
        {
            return _context.Dishes.Any(e => e.DishId == id);
        }


        // GET: Dishes/AddToCart/6
        public async Task<IActionResult> AddToCart(int? id)
        {
            var dish = await _context.Dishes
                .SingleOrDefaultAsync(m => m.DishId == id);
            if (dish == null)
            {
                return NotFound();
            }

            return View(dish);

          
        }

        // POST: Dishes/AddToCart/6
        [HttpPost, ActionName("AddToCart")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddToCart(int id)
        {
            var d = id.ToString();

            return RedirectToAction("Index");
           
        }
    }
}
