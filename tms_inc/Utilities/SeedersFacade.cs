using System.Threading.Tasks;
using tms.DataSeeders;

namespace tms.Utilities
{
    public class SeedersFacade
    {
        private readonly AppRolesSeeder _appRolesSeeder;
        private readonly CitiesSeeder _citiesSeeder;
        private readonly StatesSeeder _statesSeeder;
        public SeedersFacade(
            AppRolesSeeder appRolesSeeder,
            CitiesSeeder citiesSeeder,
            StatesSeeder statesSeeder
            )
        {
            _appRolesSeeder = appRolesSeeder;
            _citiesSeeder = citiesSeeder;
            _statesSeeder = statesSeeder;
        }

        public async Task SeedAllAsync()
        {
            await _appRolesSeeder.SeedAsync();
            await _citiesSeeder.SeedAsync();
            await _statesSeeder.SeedAsync();
        }
    }
}
