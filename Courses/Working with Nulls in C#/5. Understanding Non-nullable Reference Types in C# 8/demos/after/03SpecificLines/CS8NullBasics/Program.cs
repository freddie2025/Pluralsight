using System;

namespace CS8NullBasics
{
    class Program
    {
        static void Main(string[] args)
        {
#nullable enable
            string message = null;
#nullable disable

            Console.WriteLine(message);

            Console.WriteLine("Press enter to end.");
            Console.ReadLine();
        }
    }
}
