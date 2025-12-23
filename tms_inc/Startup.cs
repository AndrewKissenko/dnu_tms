using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using tms_inc.Services;
using tms.DataAccess;
using tms.Models;
using Microsoft.Extensions.Configuration.Xml;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System.Net.Mail;
using System.Net;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using tms.AppConfiguration;
using tms.Utilities;

namespace tms_inc
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // local for docker env...For local development, secrets should be stored in secrets.json on your local machine so they are not checked into Git.
            var connectionStr = @"Server=mssql,1433;Database=tms;User ID=sa;Password=YourStrong!Passw0rd;Encrypt=True;TrustServerCertificate=True;";
            //if you want to connect from local computer then the connection string should be 
            //Server=localhost,1433;Database=tms;User ID=sa;Password=YourStrong!Passw0rd;Encrypt=True;TrustServerCertificate=True;

            //The connection string for AWS RDS is configured in the hosting environment, stored in a secret manager
            //  var connectionStr = Configuration.GetConnectionString("DefaultConnection");

            //HTTPS enforcement is disabled for now because SSL is not configured.
            //services.AddMvc(options =>
            //{
            //    options.Filters.Add(new RequireHttpsAttribute());
            //});

            services.AddDbContext<PortalContext>(opt => opt.UseSqlServer(connectionStr));
            services.AddIdentity<User, IdentityRole>(op =>
            {
                op.Password.RequireDigit = false;
                op.Password.RequireLowercase = false;
                op.Password.RequireNonAlphanumeric = false;
                op.Password.RequireUppercase = false;
                op.Password.RequiredLength = 0;
                op.Password.RequiredUniqueChars = 0;
                op.SignIn.RequireConfirmedEmail = false;
                op.SignIn.RequireConfirmedEmail = false;
                op.User.AllowedUserNameCharacters = null;

            }).AddEntityFrameworkStores<PortalContext>();
            services.AddDistributedMemoryCache();
            services.AddMvc();
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigin", builder => {
                    builder.AllowAnyOrigin();
                    builder.WithMethods("GET", "PUT", "POST", "DELETE");
                    builder.AllowAnyHeader();
                });
            });

            services.AddTransient<UpdatesService>();

            services.Configure<SmtpOptions>(Configuration.GetSection("Smtp"));

            services.AddSingleton(Configuration.Get(typeof(AppSettings)) as AppSettings);

            services.AddSingleton(Configuration.Get(typeof(AwsConfiguration)) as AwsConfiguration);

            services.AddTransient<SmtpClient>(sp =>
            {
                var opts = sp.GetRequiredService<IOptions<SmtpOptions>>().Value;
                var client = new SmtpClient(opts.Host, opts.Port)
                {
                    Credentials = new NetworkCredential(opts.UserName, opts.Password)
                };
                return client;
            });

            services.Configure<KestrelServerOptions>(opts =>
            {
                opts.Limits.MaxRequestBodySize = 1L * 1024 * 1024 * 1024;
            });

            services.AddScoped();
            services.AddTransient();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
           

            if (env.IsDevelopment())
            {
               app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            //commented for now
           // app.UseHttpsRedirection();

            app.UseStatusCodePages();

            app.UseCors(opt => opt.AllowAnyOrigin());
            app.UseStaticFiles();

            app.UseRouting();


            app.UseAuthentication();
            app.UseAuthorization();
            app.UseStatusCodePagesWithReExecute("/Error/{0}");

            app.UseEndpoints(routes =>
            {
                routes.MapAreaControllerRoute(
                name: "tms_area",
                areaName: "tms",
                pattern: "{controller=Home}/{action=Index}/{id?}");

                routes.MapControllerRoute(
                name: "default",
                pattern: "{controller=Login}/{action=Index}/{id?}");

                
            });
        }
    }
}
