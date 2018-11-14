using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using eBookcase_CoreMVC.Models;
using eBookcase_CoreMVC.Data;

namespace eBookcase_CoreMVC.Controllers
{
    public class BooksController : Controller
    {
        private readonly ApplicationDbContext _db;

        [BindProperty]
        public BookCoreMVC BookCoreMVC { get; set; }

        public BooksController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            return View(_db.BooksCoreMVC.ToList());
        }

        //GET Create
        public IActionResult Create()
        {
            return View();
        }

        //POST Create
        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePOST()
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            _db.BooksCoreMVC.Add(BookCoreMVC);
            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        //GET Edit
        public async Task<IActionResult> Edit(int id)
        {
            BookCoreMVC = await _db.BooksCoreMVC.FindAsync(id);

            if (BookCoreMVC == null)
            {
                return NotFound();
            }

            return View(BookCoreMVC);
        }

        //POST Edit
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPOST()
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            _db.BooksCoreMVC.Update(BookCoreMVC);
            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            BookCoreMVC = await _db.BooksCoreMVC.FindAsync(id);

            if (BookCoreMVC == null)
            {
                return NotFound();
            }

            _db.BooksCoreMVC.Remove(BookCoreMVC);
            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
