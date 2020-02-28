using System;
using System.Linq;
using WiredBrainCoffeeShops.Data;

namespace DemoStarter
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            SingleNavigationPropertyGroup();
            SinglePropertyGroupWithNavigationsinSelect();
            SinglePropertyGroupWithNonAggregateProjection();
            MultipleNavigationPropertyGroupBy();

        }

        //Evaluated on client
        private static void SingleNavigationPropertyGroup()
        {
            using (var context = new CoffeeShopContext())
            {
                var unitGrouping = context.Units
                    .GroupBy(u=>u.BrewerType)
                    .Select(g => new
                    { 
                        g.Key.BrewerTypeId,
                        Cost = g.Sum(u => u.Cost),
                        Count = g.Count()
                    }).ToList();

                foreach (var item in unitGrouping)
                {
                    Console.WriteLine($"  TypeId: {item.BrewerTypeId}");
                    Console.WriteLine($"    # Units: {item.Count}");
                    Console.WriteLine($"    Total Cost: {item.Cost}");
                    Console.WriteLine($"    Average Cost: {item.Cost / item.Count}");
                }
            }
        }
   
        //Evaluated on client. Extra queries (one per group) to retrieve navigations
        private static void SinglePropertyGroupWithNavigationsinSelect()
        {
            using (var context = new CoffeeShopContext())
            {
                var unitGrouping = context.Units
                    .GroupBy(o => o.BrewerTypeId)
                    .Select(g => new
                    {
                        BrewerTypeId = g.Key,
                        Brewer = g.Min(u => u.BrewerType.Description),
                        Cost = g.Sum(u => u.Cost),
                        Count = g.Count()
                    }).ToList();

                foreach (var item in unitGrouping)
                {
                    Console.WriteLine($"Brewer: {item.Brewer}");
                    Console.WriteLine($"    # Units: {item.Count}");
                    Console.WriteLine($"    Total Cost: {item.Cost}");
                    Console.WriteLine($"    Average Cost: {item.Cost / item.Count}");
                }
            }
        }

        //Won't compile
        private static void SinglePropertyGroupWithNonAggregateProjection()
        {
            using (var context = new CoffeeShopContext())
            {
                var unitGrouping = context.Units
                    .OrderBy(u=>u.Acquired)
                    .GroupBy(u => u.BrewerTypeId)
                    .Select(g => new
                    {
                        BrewerTypeId=g.Key,
                        Cost = g.Sum(u => u.Cost),
                        //First=g.FirstOrDefault(u=>u.Cost), <-won't compile
                        Count = g.Count()
                    }).ToList();

                foreach (var item in unitGrouping)
                {
                    Console.WriteLine($"  TypeId: {item.BrewerTypeId}");
                    Console.WriteLine($"    # Units: {item.Count}");
                    Console.WriteLine($"    Total Cost: {item.Cost}");
                    Console.WriteLine($"    Average Cost: {item.Cost / item.Count}");
                }
            }
        }

        //compiles but throws exception at runtime
         private static void  MultipleNavigationPropertyGroupBy()
        {
            using (var context = new CoffeeShopContext())
            {
                var unitGrouping = context.Units
                    .GroupBy(o => new { o.BrewerType, o.Location })
                    .Select(g => new 
                    {
                        g.Key.BrewerType.BrewerTypeId,
                        g.Key.Location.StreetAddress,
                        Cost = g.Sum(u => u.Cost),
                        Count = g.Count()
                    }).ToList();
                foreach (var item in unitGrouping)
                {
                    Console.WriteLine($"LocationId: {item.StreetAddress}");
                    Console.WriteLine($"  TypeId: {item.BrewerTypeId}");
                    Console.WriteLine($"    # Units: {item.Count}");
                    Console.WriteLine($"    Total Cost: {item.Cost}");
                    Console.WriteLine($"    Average Cost: {item.Cost / item.Count}");
                }
            }
        }

       
    }
}