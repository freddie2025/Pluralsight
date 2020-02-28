namespace WiredBrainCoffeeShops.DomainClasses
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        public string Name { get; set; }
        public bool Barista { get; set; }
        public int LocationId { get; set; }
    }
}