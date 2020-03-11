using Strategy_Pattern_Using_different_shipping_providers.Business.Models;
using Strategy_Pattern_Using_different_shipping_providers.Business.Strategies.Invoice;
using Strategy_Pattern_Using_different_shipping_providers.Business.Strategies.SalesTax;
using System;

namespace Strategy_Pattern_Using_different_shipping_providers
{
    class Program
    {
        static void Main(string[] args)
        {
            var order = new Order
            {
                ShippingDetails = new ShippingDetails 
                { 
                    OriginCountry = "Sweden",
                    DestinationCountry = "Sweden"
                },
                SalesTaxStrategy = new SwedenSalesTaxStrategy(),
                InvoiceStrategy = new PrintOnDemandInvoiceStrategy()
            };

            order.SelectedPayments.Add(new Payment { PaymentProvider = PaymentProvider.Invoice });

            order.LineItems.Add(new Item("CSHARP_SMORGASBORD", "C# Smorgasbord", 100m, ItemType.Literature), 1);

            Console.WriteLine(order.GetTax());

            order.FinalizeOrder();
        }
    }
}
