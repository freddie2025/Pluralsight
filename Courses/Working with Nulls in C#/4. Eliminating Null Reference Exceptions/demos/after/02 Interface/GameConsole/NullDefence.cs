namespace GameConsole
{
    class NullDefence : ISpecialDefence
    {
        public int CalculateDamageReduction(int totalDamage)
        {
            return 0; // no operation /  "do nothing" behavior
        }
    }
}
