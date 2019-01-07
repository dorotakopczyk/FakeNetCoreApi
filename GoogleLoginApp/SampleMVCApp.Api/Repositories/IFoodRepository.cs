using System.Collections.Generic;
using System.Linq;
using SampleMVCApp.Api.Models;

namespace SampleMVCApp.Api.Repositories
{
    public interface IFoodRepository
    {
        Food GetSingleFood(int id);
        void Add(Food item);
        void Delete(int id);
        Food Update(int id, Food item);
        IQueryable<Food> GetAll(QueryParameters queryParameters);

        ICollection<Food> MakeMeal();
        int Count();

        bool Save();
    }
}
