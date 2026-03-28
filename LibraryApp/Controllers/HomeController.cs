using LibraryApp.Data;
using LibraryApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace LibraryApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            // Dashboard ke liye stats
            ViewBag.TotalBooks = await _context.Books.CountAsync();
            ViewBag.AvailableBooks = await _context.Books
                .CountAsync(b => b.IsAvailable);
            ViewBag.TotalCategories = await _context.Categories.CountAsync();
            ViewBag.TotalValue = await _context.Books.SumAsync(b => b.Price);

            // Recent 5 books
            var recentBooks = await _context.Books
                .Include(b => b.Category)
                .OrderByDescending(b => b.Id)
                .Take(5)
                .ToListAsync();

            return View(recentBooks);
        }
    }
}
