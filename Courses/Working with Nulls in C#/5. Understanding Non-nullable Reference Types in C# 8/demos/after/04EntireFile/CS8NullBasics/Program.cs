#nullable enable

using System;

namespace CS8NullBasics
{
    class Program
    {
        static void Main(string[] args)
        {
#nullable disable
            string message = null;
#nullable enable

            Console.WriteLine(message);

            Console.WriteLine("Press enter to end.");
            Console.ReadLine();
        }
    }
}
