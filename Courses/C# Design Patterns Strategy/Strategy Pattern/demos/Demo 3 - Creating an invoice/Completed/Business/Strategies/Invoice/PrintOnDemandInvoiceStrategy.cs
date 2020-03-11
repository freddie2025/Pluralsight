using System;
using System.Net.Http;
using Newtonsoft.Json;
using Strategy_Pattern_Creating_an_invoice.Business.Models;

namespace Strategy_Pattern_Creating_an_invoice.Business.Strategies.Invoice
{
    public class PrintOnDemandInvoiceStrategy : IInvoiceStrategy
    {
        public void Generate(Order order)
        {
            using (var client = new HttpClient())
            {
                var content = JsonConvert.SerializeObject(order);

                client.BaseAddress = new Uri("https://pluralsight.com");

                client.PostAsync("/print-on-demand", new StringContent(content));
            }
        }
    }
}
