using Strategy_Pattern_Using_different_shipping_providers.Business.Models;
using Strategy_Pattern_Using_different_shipping_providers.Business.Strategies.Invoice;
using Strategy_Pattern_Using_different_shipping_providers.Business.Strategies.SalesTax;
using Strategy_Pattern_Using_different_shipping_providers.Business.Strategies.Shipping;
using System;

namespace Strategy_Pattern_Using_different_shipping_providers
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Input
            Console.WriteLine("Please select an origin country: ");
            var origin = Console.ReadLine().Trim();

            Console.WriteLine("Please select a destination country: ");
            var destination  = Console.ReadLine().Trim();
            
            Console.WriteLine("Choose one of the following shipping providers.");
            Console.WriteLine("1. PostNord (Swedish Postal Service)");
            Console.WriteLine("2. DHL");
            Console.WriteLine("3. USPS");
            Console.WriteLine("4. Fedex");
            Console.WriteLine("5. UPS");
            Console.WriteLine("Select shipping provider: ");
            var provider = Convert.ToInt32(Console.ReadLine().Trim());

            Console.WriteLine("Choose one of the following invoice delivery options.");
            Console.WriteLine("1. E-mail");
            Console.WriteLine("2. File (download later)");
            Console.WriteLine("3. Mail");
            Console.WriteLine("Select invoice delivery options: ");
            var invoiceOption = Convert.ToInt32(Console.ReadLine().Trim());
            #endregion

            var order = new Order
            {
                ShippingDetails = new ShippingDetails 
                { 
                    OriginCountry = origin,
                    DestinationCountry = destination
                },
                SalesTaxStrategy = GetSalesTaxStrategyFor(origin),
                InvoiceStrategy = GetInvoiceStrategyFor(invoiceOption),
                ShippingStrategy = GetShippingStrategyFor(provider)
            };

            order.SelectedPayments.Add(new Payment { PaymentProvider = PaymentProvider.Invoice });

            order.LineItems.Add(new Item("CSHARP_SMORGASBORD", "C# Smorgasbord", 100m, ItemType.Literature), 1);

            Console.WriteLine(order.GetTax());

            order.FinalizeOrder();
        }

        private static IInvoiceStrategy GetInvoiceStrategyFor(int option)
        {
            switch (option)
            {
                case 1: return new EmailInvoiceStrategy();
                case 2: return new FileInvoiceStrategy();
                case 3: return new PrintOnDemandInvoiceStrategy();
                default: throw new Exception("Unsupported invoice delivery option");

            }
        }

        private static IShippingStrategy GetShippingStrategyFor(int provider)
        {
            switch (provider)
            {
                case 1: return new SwedishPostalServiceShippingStrategy();
                case 2: return new DhlShippingStrategy();
                case 3: return new UnitedStatesPostalServiceShippingStrategy();
                case 4: return new FedexShippingStrategy();
                case 5: return new UpsShippingStrategy();
                default: throw new Exception("Unsupported shipping method");

            }
        }

        private static ISalesTaxStrategy GetSalesTaxStrategyFor(string origin)
        {
            if (origin.ToLowerInvariant() == "sweden")
            {
                return new SwedenSalesTaxStrategy();
            }
            else if (origin.ToLowerInvariant() == "usa")
            {
                return new USAStateSalesTaxStrategy();
            }
            else
            {
                throw new Exception("Unsupported shipping region");
            }
        }
    }
}
