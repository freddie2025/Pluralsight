using System;

namespace GameConsoleCore
{
    class Program
    {
        static void Main(string[] args)
        {
            PlayerCharacter[] players = 
            {
                new PlayerCharacter { Name = "Sarah" },
                new PlayerCharacter { Name = "Gentry" },
                new PlayerCharacter {  }, // name will be null
            };


            PlayerDisplayer.Write(players[0]);
            PlayerDisplayer.Write(players[1]);
            PlayerDisplayer.Write(players[2]);

            Console.ReadLine();
        }
    }
}
