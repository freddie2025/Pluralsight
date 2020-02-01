using System;

namespace GameConsoleCore
{
    class Program
    {
        static void Main(string[] args)
        {
            PlayerCharacter?[] players = 
            {
                new PlayerCharacter("Sarah"),
                new PlayerCharacter("Gentry"),
                null
            };


            PlayerDisplayer.Write(players[0]);
            PlayerDisplayer.Write(players[1]);
            PlayerDisplayer.Write(players[2]);
            PlayerDisplayer.Write(players[3]);

            Console.ReadLine();
        }
    }
}
