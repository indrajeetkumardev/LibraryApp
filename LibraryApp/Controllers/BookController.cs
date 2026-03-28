using LibraryApp.Data;
using LibraryApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace LibraryApp.Controllers
{
    public class BookController : Controller
    {
        private readonly AppDbContext _context;

        public BookController(AppDbContext context)
        {
            _context = context;
        }

        // DropdownList helper — baar baar use hoga
        private async Task LoadCategoriesAsync(int? selectedId = null)
        {
            var categories = await _context.Categories.ToListAsync();
            ViewBag.Categories = new SelectList(categories, "Id", "Name", selectedId);
        }

        // GET: /Book  — Search + Filter bhi handle karta hai
        public async Task<IActionResult> Index(string search, int? categoryId)
        {
            // Search aur filter values wapas view ko bhejo
            ViewBag.Search = search;
            ViewBag.CategoryId = categoryId;
            ViewBag.Categories = new SelectList(
                await _context.Categories.ToListAsync(), "Id", "Name", categoryId);

            // Query banao
            var query = _context.Books
                .Include(b => b.Category)
                .AsQueryable();

            // Search filter
            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(b =>
                    b.Title.Contains(search) ||
                    b.Author.Contains(search));
            }

            // Category filter
            if (categoryId.HasValue)
            {
                query = query.Where(b => b.CategoryId == categoryId);
            }

            var books = await query.OrderBy(b => b.Title).ToListAsync();
            return View(books);
        }

        // GET: /Book/Details/1
        public async Task<IActionResult> Details(int id)
        {
            var book = await _context.Books
                .Include(b => b.Category)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (book == null) return NotFound();
            return View(book);
        }

        // GET: /Book/Create
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            await LoadCategoriesAsync();
            return View();
        }

        // POST: /Book/Create
        [HttpPost]
        public async Task<IActionResult> Create(Book book)
        {
            if (ModelState.IsValid)
            {
                _context.Books.Add(book);
                await _context.SaveChangesAsync();
                TempData["Success"] = $"'{book.Title}' successfully add ho gayi!";
                return RedirectToAction("Index");
            }
            await LoadCategoriesAsync(book.CategoryId);
            return View(book);
        }

        // GET: /Book/Edit/1
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null) return NotFound();
            await LoadCategoriesAsync(book.CategoryId);
            return View(book);
        }

        // POST: /Book/Edit
        [HttpPost]
        public async Task<IActionResult> Edit(Book book)
        {
            if (ModelState.IsValid)
            {
                _context.Books.Update(book);
                await _context.SaveChangesAsync();
                TempData["Success"] = $"'{book.Title}' update ho gayi!";
                return RedirectToAction("Index");
            }
            await LoadCategoriesAsync(book.CategoryId);
            return View(book);
        }

        // Delete
        public async Task<IActionResult> Delete(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book != null)
            {
                _context.Books.Remove(book);
                await _context.SaveChangesAsync();
                TempData["Success"] = $"'{book.Title}' delete ho gayi!";
            }
            return RedirectToAction("Index");
        }
    }
}
