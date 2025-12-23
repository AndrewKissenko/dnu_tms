using System.Threading.Tasks;

namespace tms.DataSeeders
{
    public interface IDataSeeder
    {
        Task SeedAsync();
    }
}
