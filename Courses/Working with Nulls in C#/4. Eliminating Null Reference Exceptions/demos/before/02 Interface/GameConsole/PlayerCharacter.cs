using System;

namespace GameConsole
{
    class PlayerCharacter
    {
        private readonly ISpecialDefence _specialDefence;

        public PlayerCharacter(ISpecialDefence specialDefence)
        {
            _specialDefence = specialDefence;
        }
        
        public string Name { get; set; }
        public int Health { get; set; } = 100;

        public void Hit(int damage)
        {
            int damageReduction = 0;

            if (_specialDefence != null)
            {  
                damageReduction = _specialDefence.CalculateDamageReduction(damage);
            }

            int totalDamageTaken = damage - damageReduction;

            Health -= totalDamageTaken;

            Console.WriteLine($"{Name}'s health has been reduced by {totalDamageTaken} to {Health}.");
        }
    }
}