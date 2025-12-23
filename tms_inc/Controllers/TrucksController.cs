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
    public class TrucksController : Controller
    {
        private readonly PortalContext _context;

        public TrucksController(PortalContext context)
        {
            _context = context;
        }

        // GET: Trucks
        public async Task<IActionResult> Index()
        {
            return View(await _context.Trucks.ToListAsync());
        }
        public async Task<IActionResult> GetOpenTrucks()
        {
            var users = await _context.Users.ToListAsync();
            var today = DateTime.Now.Date;
            var driverCities = await _context.DriverCities.Where(x => x.Date == today && x.AtHome == false && x.CityId != null).
                Include(x => x.City).Include(x => x.Driver).ThenInclude(x => x.User).
                Include(x => x.Driver).ThenInclude(x => x.Truck).ToListAsync();
          //  var res2 = driverCities.GroupBy(x => x.Driver.User.FirstName);
            return View(driverCities);

        }
        [HttpPost]
        public async Task<IActionResult> SaveCommentAsync(SaveCommentDTO saveComment)
        {
            int.TryParse(saveComment.DriverId.Trim(), out int driverId);
            if (driverId != 0)
            {
                var driverCity = await _context.DriverCities.
                          FirstOrDefaultAsync(x => x.DriverId == driverId && x.Date == DateTime.Now.Date);
                if (driverCity == null) return Ok("500");

                if (!string.IsNullOrWhiteSpace(saveComment.Comment)) driverCity.Comment = saveComment.Comment.Trim();
                else driverCity.Comment = "";
                await _context.SaveChangesAsync();
            }
            return Ok();
        }


        // GET: Trucks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var truck = await _context.Trucks
                .FirstOrDefaultAsync(m => m.Id == id);
            if (truck == null)
            {
                return NotFound();
            }

            return View(truck);
        }

        // GET: Trucks/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Trucks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TruckId")] Truck truck)
        {
            if (ModelState.IsValid)
            {
                _context.Add(truck);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(truck);
        }

        // GET: Trucks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var truck = await _context.Trucks.FindAsync(id);
            if (truck == null)
            {
                return NotFound();
            }
            return View(truck);
        }

        // POST: Trucks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TruckId")] Truck truck)
        {
            if (id != truck.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(truck);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TruckExists(truck.Id))
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
            return View(truck);
        }

        // GET: Trucks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var truck = await _context.Trucks
                .FirstOrDefaultAsync(m => m.Id == id);
            if (truck == null)
            {
                return NotFound();
            }

            return View(truck);
        }

        // POST: Trucks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var truck = await _context.Trucks.FindAsync(id);
            _context.Trucks.Remove(truck);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TruckExists(int id)
        {
            return _context.Trucks.Any(e => e.Id == id);
        }
    }
}
