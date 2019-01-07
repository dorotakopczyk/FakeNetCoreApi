using System;
using System.Collections.Generic;
using System.Linq;
using SampleMVCApp.Api.Models;
using SampleMVCApp.Api.Utilities;

namespace SampleMVCApp.Api.Repositories
{
    public class FoodRepository : IFoodRepository
    {
        private readonly FoodDbContext _foodDbContext;

        public FoodRepository(FoodDbContext foodDbContext)
        {
            _foodDbContext = foodDbContext;
        }

        public Food GetSingleFood(int id)
        {
            return _foodDbContext.FoodItems.FirstOrDefault(x => x.Id == id);
        }

        public void Add(Food item)
        {
            _foodDbContext.FoodItems.Add(item);
        }

        public void Delete(int id)
        {
            Food foodItem = GetSingleFood(id);
            _foodDbContext.FoodItems.Remove(foodItem);
        }

        public Food Update(int id, Food item)
        {
            _foodDbContext.FoodItems.Update(item);
            return item;
        }

        IQueryable<Food> IFoodRepository.GetAll(QueryParameters queryParameters)
        {
            IQueryable<Food> _allItems = _foodDbContext.FoodItems;

            if (queryParameters.HasQuery())
            {
                _allItems = _allItems
                    .Where(x => x.Calories.ToString().Contains(queryParameters.Query.ToLowerInvariant())
                    || x.Name.ToLowerInvariant().Contains(queryParameters.Query.ToLowerInvariant()));
            }

            return _allItems
                .Skip(queryParameters.PageCount * (queryParameters.Page - 1))
                .Take(queryParameters.PageCount);
        }

        public int Count()
        {
            return _foodDbContext.FoodItems.Count();
        }

        public bool Save()
        {
            return (_foodDbContext.SaveChanges() >= 0);
        }

        public ICollection<Food> MakeMeal()
        {
            List<Food> toReturn = new List<Food>();

            toReturn.Add(GetRandomItem("Main"));
            toReturn.Add(GetRandomItem("Dessert"));

            return toReturn;
        }

        private Food GetRandomItem(string type)
        {
            return _foodDbContext.FoodItems
                .Where(x => x.Type == type)
                .OrderBy(o => Guid.NewGuid())
                .FirstOrDefault();
        }
    }
}
