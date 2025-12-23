using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using FluentDateTime;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using tms.DataAccess;
using tms.Models;

namespace tms.Controllers
{
    public class SystemHomeController : Controller
    {
        private readonly PortalContext _context;
        private static DateTime weekStart;
        private readonly UserManager<User> _userManager;
        public SystemHomeController(PortalContext portalContext, UserManager<User> userManager)
        {
            _context = portalContext;
            _userManager = userManager;
          
        }
        
        static SystemHomeController()
        {
            SetThisSunday();
        }

       

        public async Task<IActionResult> Index()
        {
            var users = await GetUsersForWeek(false);

            return View(users);
        }
        [HttpPost]
        public async Task<IActionResult> Index(SaveDayViewModel saveDay)
        {
            int.TryParse(saveDay.DriverId.Trim(), out int driverId);
            if(saveDay.Date!= DateTimeOffset.MinValue || driverId != 0)
            {
                var driverCity = await _context.DriverCities.
                          FirstOrDefaultAsync(x => x.DriverId == driverId && x.Date == saveDay.Date);

                if (driverCity == null) return Ok("500");

                if (!string.IsNullOrWhiteSpace(saveDay.City) && !saveDay.City.Trim().Contains(">") && !saveDay.City.Trim().Contains(")") && !saveDay.City.Trim().Contains(","))
                {
                    return Ok("424");
                }
                else if (string.IsNullOrWhiteSpace(saveDay.City))
                {
                    driverCity.CityId = null;
                    driverCity.AtHome = false;
                }
                else if (!saveDay.City.Trim().Contains(">"))
                {   
                    if(saveDay.City.Length < 5) return Ok("404");
                    string cityFormatted = "";
                    string stateFormatted = "";
                    if (saveDay.City.Contains('('))
                    {
                        cityFormatted = saveDay.City.Split('(')[0].Trim();
                    }
                    else if (saveDay.City.Contains(','))
                    {
                        cityFormatted = saveDay.City.Split(',')[0].Trim();
                        stateFormatted = saveDay.City.Split(',')[1].Trim();
                    }
                    //to do contains change to equals 
                    var cityFound = await _context.Cities.FirstOrDefaultAsync(x => x.CityName.Contains(cityFormatted));

                    if (cityFound != null && cityFound.State.Contains(stateFormatted))
                    {
                        driverCity.CityId = cityFound.Id;
                        driverCity.AtHome = false;
                    }
                    else
                    {
                        var cityToAdd = new City { CityName = cityFormatted, State = stateFormatted };
                        await _context.Cities.AddAsync(cityToAdd);
                        await _context.SaveChangesAsync();
                        driverCity.CityId = cityToAdd.Id;
                        driverCity.AtHome = false;
                        await _context.SaveChangesAsync();
                        return Ok();
                    }
                }
                else if (saveDay.City.Trim().Contains(">")) 
                {
                    driverCity.AtHome = true;
                }
                
                _context.Entry(driverCity).State = EntityState.Modified;
               await  _context.SaveChangesAsync();  
            }
            return Ok();
        }

        public IActionResult GetCities()
        {
            return RedirectToAction("GetAll", "Cities");
        }

        public async Task<IActionResult> DispatchBoard()
        {
            var users = await GetUsersForWeek(true);

            return View(users);
        }

        public IActionResult GetData(string sign)
        {
            SetDate(sign);
            return RedirectToAction(nameof(Index));
        }
        public IActionResult GetDataDispatch(string sign)
        {
            SetDate(sign);
            return RedirectToAction(nameof(DispatchBoard));
        }
        private static void SetDate(string sign)
        {
            if(sign.Trim() == "0")
            {
                SetThisSunday();
            }
            else if (sign.Trim() == "+")
            {
              weekStart = weekStart.AddDays(7);
            }
            else if (sign.Trim() == "-")
            {
              weekStart = weekStart.AddDays(-7);
            }

        }

        public async Task<List<User>> GetUsersForWeek(bool showAll)
        {
            List<User> users = new List<User>();


            if (this.User.IsInRole("admin")) showAll = true;
            if (!showAll)
            {
              var user = await _userManager.FindByNameAsync(this.User.Identity.Name);
              users = await _context.Users.Where(x => x.Id == user.Id).Include(x => x.Drivers).ThenInclude(x => x.Truck).
                  Include(x => x.Drivers).ThenInclude(x => x.Trailer).ToListAsync();
            }
            else
            {
                users = await _context.Users.Include(x => x.Drivers).ThenInclude(x => x.Truck).
              Include(x => x.Drivers).ThenInclude(x => x.Trailer).ToListAsync();
            }

                await _context.DriverCities.Where(x => x.Date >= weekStart && x.Date < weekStart.AddDays(7)).
                  OrderBy(x => x.Date).Include(x => x.City).LoadAsync();


            await CreateCalendarNotExistsAsync(users, weekStart);
            return users;
        }
        
        private static void SetThisSunday()
        {
            if (DateTime.Today.DayOfWeek != DayOfWeek.Sunday)
                weekStart = DateTime.Now.Date.Previous(DayOfWeek.Sunday);
            else weekStart = DateTime.Now.Date;
        }

        private async Task CreateCalendarNotExistsAsync(List<User> users, DateTime weekStart)
        {
            //TODO: add unique contstraint to DriverId + Date in DriverCities table to avoid duplicates
            foreach (var user in users)
            {
                foreach (var driver in user.Drivers)
                {
                    if (!driver.DriverCities.Any())
                    {
                        var driverCities = new List<DriverCity>();
                        for (int i = 0; i < 7; i++)
                        {
                            driverCities.Add(
                                new DriverCity
                                {
                                    Date = weekStart.AddDays(i),
                                    DriverId = driver.Id
                                });
                        }
                        await _context.DriverCities.AddRangeAsync(driverCities);
                    }
                }
            }
           
            await _context.SaveChangesAsync();
        }

        public IActionResult Privacy()
        {
         
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

       

    }
}
