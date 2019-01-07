using System;
using System.Collections.Generic;
using System.Linq;
using SampleMVCApp.Api.Models;

namespace SampleMVCApp.Api.Repositories
{
    public interface IFoodRepository
    {
        FoodItem GetSingle(int id);
        void Add(FoodItem item);
        void Delete(int id);
        FoodItem Update(int id, FoodItem item);
        IQueryable<FoodItem> GetAll(QueryParameters queryParameters);

        ICollection<FoodItem> GetRandomMeal();
        int Count();

        bool Save();
    }
}
