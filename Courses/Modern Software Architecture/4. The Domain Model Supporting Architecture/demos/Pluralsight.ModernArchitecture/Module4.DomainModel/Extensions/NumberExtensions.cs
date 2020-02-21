namespace Module4.DomainModel.Extensions
{
    public static class NumberExtensions
    {
        public static bool BetweenWith(this int number, int lower, int upper)
        {
            return number >= lower && number <= upper;
        }

        public static int Increment(this int number, int max = 99)
        {
            var temp = number + 1;
            return temp <= max ? temp : number;
        }
    }
}
