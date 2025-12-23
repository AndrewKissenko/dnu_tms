using Microsoft.AspNetCore.Identity;
using tms.DataAccess;
using tms.Models;
using System;
using System.Threading.Tasks;
using tms.Constants;

namespace tms.DataSeeders
{
    public class AppRolesSeeder: IDataSeeder
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public AppRolesSeeder(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task SeedAsync()
        { //Can expand on this for sure, for simplicity of the run, let's hardcode one admin user,
            //admin can change the default pass in the app later
            string adminEmail = "admin";
            string password = "admin";
            if (await _roleManager.FindByNameAsync(AppConstants.AdminRoleName) == null)
            {
                try
                {
                    await _roleManager.CreateAsync(new IdentityRole(AppConstants.AdminRoleName));
                }
                catch (Exception e)
                {

                    throw;
                }
            }

            if (await _userManager.FindByNameAsync(adminEmail) == null)
            {
                User admin = new User 
                { 
                    Email = adminEmail, 
                    UserName = adminEmail, 
                    FirstName = "Admin",
                    LastName = "Admin", 
                    Login = adminEmail, 
                    Password = adminEmail 
                };
                IdentityResult result = await _userManager.CreateAsync(admin, password);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(admin, "admin");
                }
            }

            if (await _roleManager.FindByNameAsync(AppConstants.DispatcherRoleName) == null)
            {
                await _roleManager.CreateAsync(new IdentityRole(AppConstants.DispatcherRoleName));
            }

            if (await _roleManager.FindByNameAsync(AppConstants.VideoViewer) == null)
            {
                await _roleManager.CreateAsync(new IdentityRole(AppConstants.VideoViewer));
            }
        }
    }
}
