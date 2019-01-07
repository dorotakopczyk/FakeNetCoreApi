using System;
using System.Threading.Tasks;
using SampleMVCApp.Api.Models;
using SampleMVCApp.Api.Repositories;

namespace SampleMVCApp.Api.Services
{
    public class DataSeeder : IDataSeeder
    {
        public async Task Initialize(FoodDbContext context)
        {
            context.FoodItems.Add(new Food() { Calories = 250, Name = "Apple", Type = "Fruit", Created = DateTime.Now });
            context.FoodItems.Add(new Food() { Calories = 500, Name = "Turkey Leg", Type = "Main", Created = DateTime.Now });
            context.FoodItems.Add(new Food() { Calories = 750, Name = "Avocado", Created = DateTime.Now });
            context.FoodItems.Add(new Food() { Calories = 1000, Name = "Cheesecake",Type = "Dessert", Created = DateTime.Now });
            context.FoodItems.Add(new Food() { Calories = 250, Name = "Banana", Type = "Fruit", Created = DateTime.Now });
            context.FoodItems.Add(new Food() { Calories = 500, Name = "Shepherds Pie", Type = "Main", Created = DateTime.Now });
            context.FoodItems.Add(new Food() { Calories = 750, Name = "Filet Mignon", Created = DateTime.Now });
            context.FoodItems.Add(new Food() { Calories = 1000, Name = "Chana Masala", Type = "Main", Created = DateTime.Now });
            await context.SaveChangesAsync();
        }
    }
}
