using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using tms.DataAccess;
using tms.Models;

namespace tms.Controllers
{
    public class DriversController : Controller
    {
        private readonly PortalContext _context;
        SelectList _trucks;
        SelectList _trailers;
        SelectList _users;
        static int? oldTruckId;
        static int? oldTrailerId;
        public DriversController(PortalContext context)
        {
            _context = context;
            _trucks = new SelectList(_context.Trucks.Where(x=>x.IsAvailable), "Id", "TruckId");
            _trailers = new SelectList(_context.Trailers.Where(x => x.IsAvailable), "Id", "TrailerNumber");
            _users = new SelectList(_context.Users, "Id", "FirstName");
        }

        // GET: Drivers
        public async Task<IActionResult> Index()
        {
            var drivers = await _context.Drivers.Include(x=>x.Trailer).Include(x=>x.Truck).Include(x=>x.User).ToListAsync();
            return View(drivers);
        }

        // GET: Drivers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var driver = await _context.Drivers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (driver == null)
            {
                return NotFound();
            }

            return View(driver);
        }
        [Authorize(Roles = "admin")]
        // GET: Drivers/Create
        public IActionResult Create()
        {
            
            ViewBag.Trucks = _trucks;
            
            ViewBag.Trailers = _trailers;
           
            ViewBag.Users = _users;

            return View();
        }

        // POST: Drivers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Phone,Truck, Trailer, User")] Driver driver)
        {
            var trailer = await _context.Trailers.FirstOrDefaultAsync(x => x.Id == driver.Trailer.Id);
            var truck = await _context.Trucks.FirstOrDefaultAsync(x => x.Id == driver.Truck.Id);
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == driver.User.Id);
            driver.Trailer = trailer;
            driver.Truck = truck;
            driver.User = user;


            _context.Add(driver);
                trailer.IsAvailable = false;
                truck.IsAvailable = false;
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
        }

        // GET: Drivers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var driver = await _context.Drivers.FirstOrDefaultAsync(x => x.Id == id);

            oldTruckId = driver.TruckId;
            oldTrailerId = driver.TrailerId;

            List<Truck> allAvailaibleTrucks = new List<Truck>();
            allAvailaibleTrucks.Add(new Truck() { TruckId = "NO truck", Id = -1 });
            allAvailaibleTrucks.AddRange(await _context.Trucks.Where(x => x.IsAvailable).ToListAsync());
            var currentTruck = await _context.Trucks.FirstOrDefaultAsync(x => x.Id == driver.TruckId);
            if (currentTruck != null) allAvailaibleTrucks.Add(currentTruck);
            ViewBag.Trucks = new SelectList(allAvailaibleTrucks, "Id", "TruckId",driver.TruckId != null ? driver.TruckId : -1);

            List<Trailer> allAvailaibleTrailers = new List<Trailer>();
            allAvailaibleTrailers.Add(new Trailer() { TrailerNumber = "NO trailer", Id = -1 }) ;
            allAvailaibleTrailers.AddRange(await _context.Trailers.Where(x => x.IsAvailable).ToListAsync());
    
            var currentTrailer = await _context.Trailers.FirstOrDefaultAsync(x => x.Id == driver.TrailerId);
            if (currentTrailer != null) allAvailaibleTrailers.Add(currentTrailer);

            ViewBag.Trailers = new SelectList(allAvailaibleTrailers, "Id", "TrailerNumber",driver.TrailerId != null? driver.TrailerId : -1);
         
            ViewBag.Users = _users;
            if (id == null)
            {
                return NotFound();
            }

            // var driver = await _context.Driver.Include(x => x.Trailer).Include(x => x.Truck).Include(x => x.User).FirstOrDefaultAsync(x=>x.Id==id);
            
            if (driver == null)
            {
                return NotFound();
            }
            return View(driver);
        }

        // POST: Drivers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Phone,Truck, Trailer, User")] Driver driver)
        {
            if (id != driver.Id)
            {
                return NotFound();
            }

            driver.Trailer = await _context.Trailers.FirstOrDefaultAsync(x => x.Id == driver.Trailer.Id);
            driver.Truck = await _context.Trucks.FirstOrDefaultAsync(x => x.Id == driver.Truck.Id);
            driver.User = await _context.Users.FirstOrDefaultAsync(x => x.Id == driver.User.Id);

            if (driver.Trailer == null)
            {
                var trailer = await _context.Trailers.FirstOrDefaultAsync(x => x.Id == oldTrailerId);
                if (trailer != null) trailer.IsAvailable = true;
            }

            if (driver.Truck == null)
            {
                var truck = await _context.Trucks.FirstOrDefaultAsync(x => x.Id == oldTruckId);
                if (truck != null) truck.IsAvailable = true;
            }

            if (driver.Truck != null && oldTruckId != driver.Truck.Id ) 
            {
                var oldTruckSelection = await _context.Trucks.FirstOrDefaultAsync(x => x.Id == oldTruckId);
                if(oldTruckSelection!=null) oldTruckSelection.IsAvailable = true;
                driver.Truck.IsAvailable = false;
                oldTruckId = -1;
            }

            if (driver.Trailer != null && oldTrailerId != driver.Trailer.Id)
            {
                var oldTrailerSelection = await _context.Trailers.FirstOrDefaultAsync(x => x.Id == oldTrailerId);
                if (oldTrailerSelection != null) oldTrailerSelection.IsAvailable = true;
                driver.Trailer.IsAvailable = false;
                oldTrailerId = -1;
            }


            try
            {
                _context.Update(driver);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DriverExists(driver.Id))
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

        // GET: Drivers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var driver = await _context.Drivers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (driver == null)
            {
                return NotFound();
            }

            return View(driver);
        }

        // POST: Drivers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var driver = await _context.Drivers.FindAsync(id);
            var truck = await _context.Trucks.FirstOrDefaultAsync(x=>x.Id == driver.TruckId);
            var trailer = await _context.Trailers.FirstOrDefaultAsync(x => x.Id == driver.TrailerId);
            if (truck != null)
                truck.IsAvailable = true;
            if (trailer != null)
                 trailer.IsAvailable = true;

              _context.Drivers.Remove(driver);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DriverExists(int id)
        {
            return _context.Drivers.Any(e => e.Id == id);
        }
    }
}
