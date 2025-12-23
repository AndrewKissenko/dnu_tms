using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using tms.DataAccess;
using tms.Models;
using tms.Models.ViewModels.User;
using tms.Requests;

namespace tms.Controllers
{
    [Authorize(Roles = "admin")]
    public class UsersController : Controller
    {
        private readonly PortalContext _context;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        RoleManager<IdentityRole> _roleManager;

        public UsersController(PortalContext context, UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
            var vm = await _context.UserRoles
                           .AsNoTracking()
                           .Join(_context.Roles,
                               ur => ur.RoleId,
                               r => r.Id,
                               (ur, r) => new { ur.UserId, RoleName = r.NormalizedName })
                           .Join(_context.Users,
                               urr => urr.UserId,
                               u => u.Id,
                               (urr, u) => new { u, urr.RoleName })
                           .Select(x => new UserVM
                           {
                               Id = x.u.Id,
                               FirstName = x.u.FirstName,
                               LastName = x.u.LastName,
                               UserName = x.u.UserName,
                               Login = x.u.Login,
                               Password = x.u.Password,
                               RoleName = x.RoleName
                           })
                           .ToListAsync();

            return View(vm);
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: Users/Create
        public async Task<IActionResult> Create()
        {
            var roles = await _roleManager.Roles
                .AsNoTracking()
                .Select(x => new { x.NormalizedName, RoleId = x.Id })
                .ToListAsync();

            ViewBag.Roles = await GetRolesSelectList();
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateUserRequest user)
        {
            user.Login = user.UserName;
            var createResult = await _userManager.CreateAsync(user, user.Password);

            if (!createResult.Succeeded)
            {
                foreach (var err in createResult.Errors)
                    ModelState.AddModelError(string.Empty, err.Description);
                return View(await GetDataForUsersView(user));
            }

            // 2) Get role by ID
            var role = await _roleManager.FindByIdAsync(user.RoleId);
            if (role == null)
            {
                ModelState.AddModelError(string.Empty, "Selected role does not exist.");
                await _userManager.DeleteAsync(user); // rollback
                return View(await GetDataForUsersView(user));
            }

            // 3) Assign user to role (uses role NAME internally)
            var roleResult = await _userManager.AddToRoleAsync(user, role.Name);
            if (!roleResult.Succeeded)
            {
                foreach (var error in roleResult.Errors)
                    ModelState.AddModelError(string.Empty, error.Description);

                await _userManager.DeleteAsync(user); // rollback
                return View(await GetDataForUsersView(user));
            }

            return RedirectToAction(nameof(Index));

        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,UserName,Password,FirstName,LastName")] User user)
        {
            if (id != user.Id)
            {
                return NotFound();
            }
            try
            {
                var userOld = await _context.Users.FirstOrDefaultAsync(x=>x.Id==user.Id);
                    if (userOld == null) return NotFound();
                   
                if(userOld.Password != user.Password)
                {
                    await _userManager.ChangePasswordAsync(userOld, userOld.Password, user.Password);
                }

                userOld.FirstName = user.FirstName;
                userOld.LastName = user.LastName;
                userOld.Login = user.UserName;
                userOld.Password = user.Password;

                var res = await _userManager.UpdateAsync(userOld);
                if(!res.Succeeded) 
                    return NotFound();

            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(user.Id))
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
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        [Authorize(Roles = "admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(string id)
        {
            return _context.Users.Any(e => e.Id == id);
        }

        private async Task<SelectList> GetRolesSelectList()
        {
            var roles = await _roleManager.Roles
                 .AsNoTracking()
                 .Select(x => new { x.NormalizedName, RoleId = x.Id })
                 .ToListAsync();

            return new SelectList(roles, "RoleId", "NormalizedName");
        }

        private async Task<CreateUserVM> GetDataForUsersView(CreateUserRequest user)
        {
            var userVm = new CreateUserVM
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName,
                Password = user.Password,
            };
            ViewBag.Roles = await GetRolesSelectList();

            return userVm;
        }
    }
}
