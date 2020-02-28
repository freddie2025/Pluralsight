namespace WiredBrainCoffeeShops.DomainClasses
{
    public class BrewerType
    {
        public int BrewerTypeId { get; set; }
        public string Description { get; set; }
        public Recipe Recipe { get; set; }
    }
}