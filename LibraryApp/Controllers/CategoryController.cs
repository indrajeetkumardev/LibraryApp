using LibraryApp.Data;
using LibraryApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryApp.Controllers
{
    public class CategoryController : Controller
    {
        private readonly AppDbContext _context;

        public CategoryController(AppDbContext context)
        {
            _context = context;
        }

        // GET: /Category
        public async Task<IActionResult> Index()
        {
            // Books count bhi saath mein lao
            var categories = await _context.Categories
                .Include(c => c.Books)
                .ToListAsync();
            return View(categories);
        }

        // GET: /Category/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Category/Create
        [HttpPost]
        public async Task<IActionResult> Create(Category category)
        {
            if (ModelState.IsValid)
            {
                _context.Categories.Add(category);
                await _context.SaveChangesAsync();
                TempData["Success"] = $"'{category.Name}' category add ho gayi!";
                return RedirectToAction("Index");
            }
            return View(category);
        }

        // GET: /Category/Edit/1
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null) return NotFound();
            return View(category);
        }

        // POST: /Category/Edit
        [HttpPost]
        public async Task<IActionResult> Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                _context.Categories.Update(category);
                await _context.SaveChangesAsync();
                TempData["Success"] = $"'{category.Name}' update ho gayi!";
                return RedirectToAction("Index");
            }
            return View(category);
        }

        // GET: /Category/Delete/1
        public async Task<IActionResult> Delete(int id)
        {
            var category = await _context.Categories
                .Include(c => c.Books)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (category == null) return NotFound();

            // Agar books hain toh delete mat karo
            if (category.Books.Any())
            {
                TempData["Error"] = $"'{category.Name}' delete nahi ho sakti — isme books hain!";
                return RedirectToAction("Index");
            }

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            TempData["Success"] = "Category delete ho gayi!";
            return RedirectToAction("Index");
        }
    }
}
