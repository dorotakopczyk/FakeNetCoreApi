using System;
using Microsoft.EntityFrameworkCore;
using SampleMVCApp.Api.Models;

namespace SampleMVCApp.Api.Repositories
{
    public class FoodDbContext : DbContext
    {
        public FoodDbContext(DbContextOptions<FoodDbContext> options)
           : base(options)
        {

        }

        public DbSet<FoodItem> FoodItems { get; set; }

    }
}
