using System;

namespace CS8NullBasics
{
    class Program
    {
        static void Main(string[] args)
        {

            Message message = new Message
            {
                Text = "Hello there!",
                From = null
            };

            Console.WriteLine(message.Text);
            Console.WriteLine(message.From ?? "no from");
            Console.WriteLine(message.ToUpperFrom());

            Console.WriteLine("Press enter to end.");
            Console.ReadLine();
        }
    }
}
