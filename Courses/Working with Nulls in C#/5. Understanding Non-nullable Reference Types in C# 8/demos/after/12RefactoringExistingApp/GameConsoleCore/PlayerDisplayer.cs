using System;


namespace GameConsoleCore
{
    static class PlayerDisplayer
    {
        public static void Write(PlayerCharacter? player)
        {
            if (player is null)
            {
                Console.WriteLine("No player specified.");
                return;
            }

            Console.WriteLine(player.Name);

            int days = player.DaysSinceLastLogin ?? -1;
            Console.WriteLine($"{days} days since last login");

            if (player.DateOfBirth == null)
            {
                Console.WriteLine("No date of birth specified");
            }
            else
            {
                Console.WriteLine(player.DateOfBirth);
            }

            if (player.IsNoob == null)
            {
                Console.WriteLine("Player newbie status is unknown");
            }
            else if (player.IsNoob == true)
            {
                Console.WriteLine("Player is a newbie");
            }
            else
            {
                Console.WriteLine("Player is experienced");
            }
        }
    }
}
