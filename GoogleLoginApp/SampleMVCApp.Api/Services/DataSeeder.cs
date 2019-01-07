﻿using System;
using System.Threading.Tasks;
using SampleMVCApp.Api.Models;
using SampleMVCApp.Api.Repositories;

namespace SampleMVCApp.Api.Services
{
    public class DataSeeder : IDataSeeder
    {
        public async Task Initialize(FoodDbContext context)
        {
            context.FoodItems.Add(new FoodItem() { Calories = 1000, Name = "Lasagne", Created = DateTime.Now });
            context.FoodItems.Add(new FoodItem() { Calories = 1100, Name = "Hamburger", Created = DateTime.Now });
            context.FoodItems.Add(new FoodItem() { Calories = 1200, Name = "Spaghetti", Created = DateTime.Now });
            context.FoodItems.Add(new FoodItem() { Calories = 1300, Name = "Pizza", Created = DateTime.Now });

            await context.SaveChangesAsync();
        }
    }
}
