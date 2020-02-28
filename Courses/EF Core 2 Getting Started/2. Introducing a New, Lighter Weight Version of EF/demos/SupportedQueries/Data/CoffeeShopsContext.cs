using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using System;
using WiredBrainCoffeeShops.DomainClasses;

namespace WiredBrainCoffeeShops.Data
{
    public class CoffeeShopContext : DbContext
    {
        public DbSet<Location> Locations { get; set; }
        public DbSet<Unit> Units { get; set; }

           protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("DataSource=..\\..\\..\\Data\\CoffeeShops.db").UseLoggerFactory(CommandsLoggerFactory).EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Unit>().Property(u => u.Acquired).HasColumnType("Date");

            modelBuilder.Entity<Location>().HasData(
                      new Location { LocationId = 1, StreetAddress = "1 Main", OpenTime = new TimeSpan(6, 0, 0), CloseTime = new TimeSpan(18, 0, 0) },
                      new Location { LocationId = 2, StreetAddress = "2 Main", OpenTime = TimeSpan.FromHours(7), CloseTime = TimeSpan.FromHours(19) },
                      new Location { LocationId = 3, StreetAddress = "3 Main", OpenTime = TimeSpan.FromHours(7.5), CloseTime = TimeSpan.FromHours(19.5) }
                      );

            modelBuilder.Entity<Employee>().HasData(
                    new Employee { EmployeeId = 1, Name = "Leia", LocationId = 1, Barista = true },
                    new Employee { EmployeeId = 2, Name = "Rey", LocationId = 2 },
                    new Employee { EmployeeId = 3, Name = "Gamora", LocationId = 2, Barista = true },
                    new Employee { EmployeeId = 4, Name = "Bobbie", LocationId = 3, Barista = true },
                    new Employee { EmployeeId = 5, Name = "Chrisjen", LocationId = 3 }
                    );

            modelBuilder.Entity<BrewerType>().HasData(
                   new BrewerType { BrewerTypeId = 1, Description = "Glass Hourglass Drip" },
                   new BrewerType { BrewerTypeId = 2, Description = "Hand Press" },
                   new BrewerType { BrewerTypeId = 3, Description = "Cold Brew" }
                   );
            modelBuilder.Entity<Unit>().HasData(
                new Unit { UnitId = 1, Acquired = new DateTime(2018, 6, 1), LocationId = 1, BrewerTypeId = 2, Cost=10.00m},
                new Unit { UnitId = 2, Acquired = new DateTime(2018, 6, 2), LocationId = 1, BrewerTypeId = 3,Cost = 30.00m },
                new Unit { UnitId = 3, Acquired = new DateTime(2018, 6, 3), BrewerTypeId = 1, Cost = 25.00m },
                new Unit { UnitId = 4, Acquired = new DateTime(2018, 6, 4), LocationId = 2, BrewerTypeId = 1 , Cost = 42.50m }
                );

            modelBuilder.Entity<Recipe>().HasData(
                 new
                 {
                     RecipeId = 1,
                     BrewerTypeId = 1,
                     BrewMinutes = 3,
                     GrindSize = 2,
                     GrindOunces = 2,
                     WaterOunces = 9,
                     WaterTemperatureF = 130,
                     Description = "So good!"
                 });
            modelBuilder.Entity<Recipe>().HasData(
               new
               {
                   RecipeId = 2,
                   BrewerTypeId = 2,
                   BrewMinutes = 1,
                   GrindSize = 2,
                   GrindOunces = 2,
                   WaterOunces = 9,
                   WaterTemperatureF = 130,
                   Description = "Love a hand pressed coffee!"
               });
            modelBuilder.Entity<Recipe>().HasData(
               new
               {
                   RecipeId = 3,
                   BrewerTypeId = 3,
                   BrewMinutes = 60,
                   GrindSize = 2,
                   GrindOunces = 2,
                   WaterOunces = 9,
                   WaterTemperatureF = 130,
                   Description = "Cold brew is worth the wait!"
               });
        }
        public static readonly LoggerFactory CommandsLoggerFactory
              = new LoggerFactory(new[] {
              new ConsoleLoggerProvider((category, level)
                => category == DbLoggerCategory.Database.Command.Name
               && level == LogLevel.Information, true) });

        public static readonly LoggerFactory DebugLoggerFactory
    = new LoggerFactory(new[] { new ConsoleLoggerProvider((_, level) => level == LogLevel.Debug, true) });


    }
}