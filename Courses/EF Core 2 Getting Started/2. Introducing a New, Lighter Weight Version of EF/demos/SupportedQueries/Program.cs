using System;
using System.Linq;
using WiredBrainCoffeeShops.Data;

namespace DemoStarter
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            SinglePropertyGroup();
            //MultiplePropertyGroupBy();
            //MultiplePropertyGroupByIntoKnownType();
            //GroupByScalarPropertyOfRelatedType();
            //MultiplePropertyGroupByNavigationInGroup();
            //OrderByAndFilterGrouping();
          }

        private static void SinglePropertyGroup()
        {
            using (var context = new CoffeeShopContext())
            {
                var unitGrouping = context.Units
                    .GroupBy(u=>u.BrewerTypeId)
                    .OrderBy(g=>g.Sum(u=>u.Cost))
                    .Where(g=>g.Count()>1)
                    .Select(g => new
                    { 
                        BrewerTypeId= g.Key,
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
        private static void OrderByAndFilterGrouping()
        {
            using (var context = new CoffeeShopContext())
            {
                var unitGrouping = context.Units
                    .GroupBy(o => 2)
                    .OrderBy(o => o.Sum(u => u.Cost))
                    .Where(o=>o.Count()>=2)
                    .Select(g => new
                    {
                        BrewerTypeId = g.Key,
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
        private class UnitGroup
        {
            public decimal Cost { get; set; }
            public int Count { get; set; }
            public int BrewerTypeId { get; set; }
            public string StreetAddress { get; set; }
        }
        private static void MultiplePropertyGroupByIntoKnownType()
        {
            using (var context = new CoffeeShopContext())
            {
                var unitGrouping = context.Units
                    .GroupBy(o => new { o.BrewerTypeId, o.Location.StreetAddress })
                    .Select(g => new UnitGroup
                    {
                        BrewerTypeId= g.Key.BrewerTypeId,StreetAddress=g.Key.StreetAddress,
                        Cost = g.Sum(u => u.Cost), Count = g.Count()
                    }).ToList();
                foreach (UnitGroup item in unitGrouping)
                {
                    Console.WriteLine($"LocationId: {item.StreetAddress}");
                    Console.WriteLine($"  TypeId: {item.BrewerTypeId}");
                    Console.WriteLine($"    # Units: {item.Count}");
                    Console.WriteLine($"    Total Cost: {item.Cost}");
                    Console.WriteLine($"    Average Cost: {item.Cost / item.Count}");
                }
            }
        }
        private static void  MultiplePropertyGroupBy()
        {
            using (var context = new CoffeeShopContext())
            {
                var unitGrouping = context.Units
                    .GroupBy(o => new { o.BrewerTypeId, o.Location.StreetAddress })
                    .Select(g => new 
                    {
                        g.Key.BrewerTypeId,
                        g.Key.StreetAddress,
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


        private static void GroupByScalarPropertyOfRelatedType()
        {
            using (var context = new CoffeeShopContext())
            {
                var unitGrouping = context.Units
                    .GroupBy(o =>  o.Location.StreetAddress )
                    .Select(g => new
                    {
                        LocationAddress=g.Key,
                        Cost = g.Sum(u => u.Cost),
                        Count = g.Count()
                    }).ToList();

                foreach (var item in unitGrouping)
                {
                    Console.WriteLine($"Location: {item.LocationAddress}");
                    Console.WriteLine($"    # Units: {item.Count}");
                    Console.WriteLine($"    Total Cost: {item.Cost}");
                    Console.WriteLine($"    Average Cost: {item.Cost / item.Count}");
                }
            }
        }

         private static void MultiplePropertyGroupByNavigationInGroup()
        {
            using (var context = new CoffeeShopContext())
            {
                var unitGrouping = context.Units
                    .GroupBy(o => new { o.BrewerTypeId, o.Location.StreetAddress })
                    .Select(g => new
                    {
                        g.Key.BrewerTypeId,
                        g.Key.StreetAddress,
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