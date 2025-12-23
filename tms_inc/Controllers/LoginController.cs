using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using tms_inc.Services;
using tms.DataAccess;
using tms.Models;
using tms.Constants;

namespace tms.Controllers
{
    public class LoginController : Controller
    {
        private readonly PortalContext _context;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UpdatesService _updatesService;
        public LoginController(PortalContext context, UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<IdentityRole> roleManager, UpdatesService updatesService)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _updatesService = updatesService;
        }
        [Route("Login/Index")]
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Login(string login, string password)
        {
          //  await InitializeAsync(_userManager, _roleManager);
            if (string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(password)) return RedirectToAction("Index", "Login");

            var res = await _signInManager.PasswordSignInAsync(login, password, false, false);

            if (res.Succeeded)
            {
                await _updatesService.UpdateNumbers();
                // 1) Get the signed-in user
                var user = await _userManager.FindByNameAsync(login);

                if (user == null)
                {
                    // extremely rare, but safe to handle
                    await _signInManager.SignOutAsync();
                    ModelState.AddModelError("", "User not found.");
                    return View();
                }

                // 2) Get user roles
                var roles = await _userManager.GetRolesAsync(user);

                var role = roles.FirstOrDefault();
                if (role == null)
                {
                    // extremely rare, but safe to handle
                    await _signInManager.SignOutAsync();
                    ModelState.AddModelError("", "User has no assigned roles.");
                    return View();
                }

                if (role != AppConstants.VideoViewer)
                {
                    return RedirectToAction("DispatchBoard", "SystemHome");
                }
                return RedirectToAction("Index", "DriverApp");
            }
            else
            {
                ModelState.AddModelError("", "Неправильний логін чи пароль");
            }
            return RedirectToAction("Index", "Login");
        }

        public async Task<IActionResult> Logout()
        {
            // удаляем аутентификационные куки
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Login");
        }

        //private async Task ParseCities(PortalContext context)
        //{
        //    if (context.Cities.Count() > 1000) return;
        //    List<City> cities = new List<City>();
        //    var doc = new HtmlWeb().Load($"https://www.britannica.com/topic/list-of-cities-and-towns-in-the-United-States-2023068#ref326632");

        //    var stateCounter = 2;


        //    for (int i = 0; i < 60; i++)
        //    {
        //        var state = doc.DocumentNode.SelectSingleNode($"/html/body/main/div/div/div[1]/article/div[3]/div[2]/div/div[2]/div/section[{stateCounter}]/h2/a")?.InnerText;
        //        if (!string.IsNullOrEmpty(state))
        //        {
        //            var cityCounter = 1;
        //            for (; ; )
        //            {
        //                var cityName = doc.DocumentNode.SelectSingleNode($"/html/body/main/div/div/div[1]/article/div[3]/div[2]/div/div[2]/div/section[{stateCounter}]/ul/li[{cityCounter++}]/div/a")?.InnerText;
        //                if (!string.IsNullOrEmpty(cityName))
        //                {
        //                    cities.Add(new City { State = state, CityName = cityName });
        //                }
        //                else
        //                {
        //                    stateCounter++;
        //                    break;
        //                }
        //            }

        //        }
        //        else break;
        //    }
        //    await context.Cities.AddRangeAsync(cities);
        //    await context.SaveChangesAsync();

        //}

        //private async Task ParseStateAsync(List<City> cities)
        //{
        //    List<State> states = new List<State>();

        //    var doc = new HtmlWeb().Load($"http://www.americancities.ru/info/capital_cities/");

        //    var stateCounter = 2;


        //    for (int i = 0; i < 60; i++)
        //    {
        //        var stateFull = doc.DocumentNode.SelectSingleNode($"/html/body/div[2]/div[2]/div[2]/table/tbody/tr[{stateCounter}]/td[1]")?.InnerText.Split('(')[0].Trim();
        //        var stateShort = doc.DocumentNode.SelectSingleNode($"/html/body/div[2]/div[2]/div[2]/table/tbody/tr[{stateCounter}]/td[3]")?.InnerText;
        //        if (!string.IsNullOrEmpty(stateFull) && !string.IsNullOrEmpty(stateShort))
        //        {
        //            states.Add(new State { StateFull = stateFull, StateShort = stateShort });
        //            stateCounter++;
        //        }
        //        else break;
        //    }

        //    foreach (var city in cities)
        //    {
        //        var state = states.FirstOrDefault(x => x.StateFull == city.State);
        //        if(state != null)
        //        {
        //            city.State = state.StateShort;
        //        }
        //    }
        //    await _context.SaveChangesAsync();

        //}
        //private async Task InitializeAsync(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        //{
        //    //Can expand on this for sure, for simplicity of the run, let's hardcode one admin user,
        //    //admin can change the default pass in the app later
        //    string adminEmail = "admin";
        //    string password = "admin";
        //    if (await roleManager.FindByNameAsync(AppConstants.AdminRoleName) == null)
        //    {
        //        try
        //        {
        //            await roleManager.CreateAsync(new IdentityRole(AppConstants.AdminRoleName));
        //        }
        //        catch (Exception e)
        //        {

        //            throw;
        //        }

        //    }
        //    if (await userManager.FindByNameAsync(adminEmail) == null)
        //    {
        //        User admin = new User { Email = adminEmail, UserName = adminEmail, FirstName = "Admin", LastName = "Admin", Login = adminEmail, Password = adminEmail };
        //        IdentityResult result = await userManager.CreateAsync(admin, password);

        //        if (result.Succeeded)
        //        {
        //            await userManager.AddToRoleAsync(admin, "admin");
        //        }
        //    }

        //    if (await roleManager.FindByNameAsync(AppConstants.DispatcherRoleName) == null)
        //    {
        //        await roleManager.CreateAsync(new IdentityRole(AppConstants.DispatcherRoleName));
        //    }

        //    if (await roleManager.FindByNameAsync(AppConstants.VideoViewer) == null)
        //    {
        //        await roleManager.CreateAsync(new IdentityRole(AppConstants.VideoViewer));
        //    }
        //}
    }

    //public class State
    //{
    //    public string StateFull { get; set; }
    //    public string StateShort { get; set; }
    //}
}

