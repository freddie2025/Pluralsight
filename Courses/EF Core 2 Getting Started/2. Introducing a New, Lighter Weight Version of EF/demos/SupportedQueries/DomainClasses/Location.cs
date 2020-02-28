using System;
using System.Collections.Generic;

namespace WiredBrainCoffeeShops.DomainClasses
{
    public class Location
    {
        public Location()
        {
            Employees = new List<Employee>();
        }
        public int LocationId { get; set; }
        public string StreetAddress { get; set; }
        public TimeSpan OpenTime { get; set; }
        public TimeSpan CloseTime { get; set; }
        public string Hours =>
            $"{new DateTime().Add(OpenTime).ToString("h:mm tt")}-{new DateTime().Add(CloseTime).ToString("h:mm tt")}";
        public List<Unit> BrewingUnits { get; set; }
        public List<Employee> Employees { get; set; }
    }
}