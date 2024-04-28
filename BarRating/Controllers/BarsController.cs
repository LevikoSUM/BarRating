using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BarRating.Data;
using Microsoft.AspNetCore.Authorization;
using BarRating.Data.Utils;
using BarRating.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace BarRating.Controllers
{
    public class BarsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public BarsController(ApplicationDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        // GET: Bars
        [Authorize(Roles = "Admin,Member")]
        public async Task<IActionResult> Index(string searchString)
        {
            if (_context.Bars == null)
            {
                return Problem("Entity set 'MvcMovieContext.Movie'  is null.");
            }

            var bars = from b in _context.Bars
                         select b;

            if (!String.IsNullOrEmpty(searchString))
            {
                bars = bars.Where(s => s.Name!.Contains(searchString));
            }

            return View(await bars.ToListAsync());
            //return _context.Bars != null ? 
            //            View(await _context.Bars.ToListAsync()) :
            //            Problem("Entity set 'ApplicationDbContext.Bars'  is null.");
        }

        // GET: Bars/Details/5
        [Authorize(Roles = "Admin,Member")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Bars == null)
            {
                return NotFound();
            }

            var bar = await _context.Bars
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bar == null)
            {
                return NotFound();
            }

            return View(bar);
        }

        // GET: Bars/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Bars/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(/*[Bind("Id,Name,Description,PictureUrl")]*/BarCreateEditViewModel bar)
        {
            if (ModelState.IsValid)
            {
                if (bar.Picture != null && bar.Picture.Length > 0)
                {
                    var newFileName = await FileUpload.UploadAsync(bar.Picture, _environment.WebRootPath);
                    bar.PictureUrl = newFileName;
                }
                
                _context.Add(bar);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(bar);
        }

        // GET: Bars/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Bars == null)
            {
                return NotFound();
            }

            var bar = await _context.Bars.FindAsync(id);
            if (bar == null)
            {
                return NotFound();
            }
            BarCreateEditViewModel barC = new BarCreateEditViewModel()
            {
                Id = bar.Id,
                Name = bar.Name,
                Description = bar.Description,
                PictureUrl = bar.PictureUrl
            };
            return View(barC);
        }

        // POST: Bars/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, BarCreateEditViewModel bar)
        {
            if (id != bar.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if (bar.Picture != null && bar.Picture.Length > 0)
                {
                    var newFileName = await FileUpload.UploadAsync(bar.Picture, _environment.WebRootPath);
                    bar.PictureUrl = newFileName;
                }
                try
                {
                    _context.Update(bar);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BarExists(bar.Id))
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
            return View(bar);
        }

        // GET: Bars/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Bars == null)
            {
                return NotFound();
            }

            var bar = await _context.Bars
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bar == null)
            {
                return NotFound();
            }

            return View(bar);
        }

        // POST: Bars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Bars == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Bars'  is null.");
            }
            var bar = await _context.Bars.FindAsync(id);
            if (bar != null)
            {
                _context.Bars.Remove(bar);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BarExists(int id)
        {
          return (_context.Bars?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
