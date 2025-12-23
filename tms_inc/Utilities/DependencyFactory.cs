using Microsoft.Extensions.DependencyInjection;
using tms.DataSeeders;
using tms.Services;

namespace tms.Utilities
{
    public static class DependencyFactory
    {
        public static IServiceCollection AddTransient(this IServiceCollection services)
        {
            services.AddTransient<S3Service>();
            services.AddTransient<PersonFileService>();
            services.AddTransient<ApplicatService>();
            return services;
        }
        public static IServiceCollection AddScoped(this IServiceCollection services)
        {
            services.AddScoped<AmazonS3ClientFactory>();
            services.AddScoped<AppRolesSeeder>();
            services.AddScoped<CitiesSeeder>();
            services.AddScoped<StatesSeeder>();
            services.AddScoped<SeedersFacade>();
            return services;
        }
    }
}
