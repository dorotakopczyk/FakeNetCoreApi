using System.Threading.Tasks;
using SampleMVCApp.Api.Repositories;

namespace SampleMVCApp.Api.Services
{
    public interface IDataSeeder
    {
        Task Initialize(FoodDbContext context);
    }
}