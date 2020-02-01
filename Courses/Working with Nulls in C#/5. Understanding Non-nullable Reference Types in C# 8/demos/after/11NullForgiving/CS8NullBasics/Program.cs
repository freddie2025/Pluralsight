using System;

namespace CS8NullBasics
{
    class Program
    {
        static void Main(string[] args)
        {

            Message message = new Message
            {
                Text = null!,
                From = null
            };

            MessagePopulator.Populate(message);

            Console.WriteLine(message.Text);
            Console.WriteLine(message.From);
            Console.WriteLine(message.From!.Length);
            Console.WriteLine(message.ToUpperFrom());

            Console.WriteLine("Press enter to end.");
            Console.ReadLine();
        }
    }
}
