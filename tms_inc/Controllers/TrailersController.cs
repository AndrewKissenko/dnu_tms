using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using tms.DataAccess;
using tms.Models;

namespace tms.Controllers
{
    public class TrailersController : Controller
    {
        private readonly PortalContext _context;

        public TrailersController(PortalContext context)
        {
            _context = context;
        }

        // GET: Trailers
        public async Task<IActionResult> Index()
        {
            return View(await _context.Trailers.ToListAsync());
        }

        // GET: Trailers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trailer = await _context.Trailers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (trailer == null)
            {
                return NotFound();
            }

            return View(trailer);
        }

        // GET: Trailers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Trailers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TrailerNumber")] Trailer trailer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(trailer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(trailer);
        }

        // GET: Trailers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trailer = await _context.Trailers.FindAsync(id);
            if (trailer == null)
            {
                return NotFound();
            }
            return View(trailer);
        }

        // POST: Trailers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TrailerNumber")] Trailer trailer)
        {
            if (id != trailer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(trailer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TrailerExists(trailer.Id))
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
            return View(trailer);
        }

        public async Task<IActionResult> GetOpenTrailers()
        {
            var trailers = await _context.Trailers.Where(x => x.IsAvailable).ToListAsync();
            return View(trailers);
        }



        // GET: Trailers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trailer = await _context.Trailers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (trailer == null)
            {
                return NotFound();
            }

            return View(trailer);
        }

        // POST: Trailers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var trailer = await _context.Trailers.FindAsync(id);
            _context.Trailers.Remove(trailer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TrailerExists(int id)
        {
            return _context.Trailers.Any(e => e.Id == id);
        }
    }
}
