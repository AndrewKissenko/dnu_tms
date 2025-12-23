using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using tms.DataAccess;
using tms.Utilities;

namespace tms_inc
{
    public class Program
    {
        public static void Main(string[] args)
        {

            var host = BuildWebHost(args);

            using (var scope = host.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<PortalContext>();
                db.Database.Migrate();

                var seeder = scope.ServiceProvider.GetRequiredService<SeedersFacade>();
                //no sense to make it async here since the Main method cannot be async and call is blocking anyway
                seeder.SeedAllAsync().Wait();
            }



            host.Run();

        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                 .ConfigureAppConfiguration((hostingContext, config) =>
                 {
                     config.AddEnvironmentVariables();

                     config.AddUserSecrets<Program>();
                 })
                .UseStartup<Startup>()
                .Build();
    }
}
