using Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Data.Common;

namespace Data
{
    public class WeatherDbContext : DbContext
    {
        public DbSet<City> Cities { get; set; }

        public WeatherDbContext(DbContextOptions<WeatherDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var cities = LoadCities();
            modelBuilder.Entity<City>().HasData(cities);
        }

        private List<City> LoadCities()
        {
            FileStream fileStream = new FileStream("cities.json", FileMode.Open, FileAccess.Read);
            using StreamReader reader = new StreamReader(fileStream);
            string json = reader.ReadToEnd();
            try
            {
                var cities = System.Text.Json.JsonSerializer.Deserialize<List<City>>(json);
                if (cities != null)
                {
                    return cities;
                }
            }
            finally
            {
                reader.Close();
                fileStream.Close();
            }

            return new List<City>();
        }
    }
}
