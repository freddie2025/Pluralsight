using System;
using System.IO;
using Strategy_Pattern_Creating_an_invoice.Business.Models;

namespace Strategy_Pattern_Creating_an_invoice.Business.Strategies.Invoice
{
    public class FileInvoiceStrategy : InvoiceStrategy
    {
        public override void Generate(Order order)
        {
            using (var stream = new StreamWriter($"invoice_{Guid.NewGuid()}.txt"))
            {
                stream.Write(GenerateTextInvoice(order));

                stream.Flush();
            }
        }
    }
}
