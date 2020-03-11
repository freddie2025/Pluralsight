using System;
using System.IO;
using Strategy_Pattern_Using_different_shipping_providers.Business.Models;

namespace Strategy_Pattern_Using_different_shipping_providers.Business.Strategies.Invoice
{
    public class FileInvoiceStrategy : InvoiceStrategy
    {
        public override void Generate(Order order)
        {
            var filename = $"invoice_{Guid.NewGuid()}.txt";
            using (var stream = new StreamWriter(filename))
            {
                stream.Write(GenerateTextInvoice(order));

                stream.Flush();

                Console.WriteLine($"Invoice for order saved to {filename}");
            }
        }
    }
}
