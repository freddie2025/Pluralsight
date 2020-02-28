namespace WiredBrainCoffeeShops.DomainClasses
{
    public class Recipe
    {
        public int RecipeId { get; set; }
        public int BrewerTypeId { get; set; }
        public int WaterTemperatureF { get; set; }
        public int GrindSize { get; set; }
        public int GrindOunces { get; set; }
        public int WaterOunces { get; set; }
        public int BrewMinutes { get; set; }
        public string Description { get; set; }
    }
}