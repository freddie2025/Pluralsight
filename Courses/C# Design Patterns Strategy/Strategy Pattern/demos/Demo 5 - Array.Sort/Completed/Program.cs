using Strategy_Pattern_Using_different_shipping_providers.Business.Models;
using Strategy_Pattern_Using_different_shipping_providers.Business.Strategies.Invoice;
using Strategy_Pattern_Using_different_shipping_providers.Business.Strategies.SalesTax;
using Strategy_Pattern_Using_different_shipping_providers.Business.Strategies.Shipping;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Strategy_Pattern_Using_different_shipping_providers
{
    class Program
    {
        static void Main(string[] args)
        {
            var orders = new[] {
                new Order
                {
                    ShippingDetails = new ShippingDetails
                    {
                        OriginCountry = "Sweden"
                    }
                },
                new Order
                {
                    ShippingDetails = new ShippingDetails
                    {
                        OriginCountry = "USA"
                    }
                },
                new Order
                {
                    ShippingDetails = new ShippingDetails
                    {
                        OriginCountry = "Sweden"
                    }
                },
                new Order
                {
                    ShippingDetails = new ShippingDetails
                    {
                        OriginCountry = "USA"
                    }
                },
                new Order
                {
                    ShippingDetails = new ShippingDetails
                    {
                        OriginCountry = "Singapore"
                    }
                }
            };

            Print(orders);

            Console.WriteLine();
            Console.WriteLine("Sorting..");
            Console.WriteLine();

            /// TODO: Sort array

            Array.Sort(orders, new OrderAmountComparer());

            Print(orders);
        }

        static void Print(IEnumerable<Order> orders)
        {
            foreach (var order in orders)
            {
                Console.WriteLine(order.ShippingDetails.OriginCountry);
            }
        }
    }

    public class OrderAmountComparer : IComparer<Order>
    {
        public int Compare(Order x, Order y)
        {
            var xTotal = x.TotalPrice;
            var yTotal = y.TotalPrice;
            if (xTotal == yTotal)
            {
                return 0;
            }
            else if (xTotal > yTotal)
            {
                return 1;
            }

            return -1;
        }
    }
    public class OrderOriginComparer : IComparer<Order>
    {
        public int Compare(Order x, Order y)
        {
            var xDest = x.ShippingDetails.OriginCountry.ToLowerInvariant();
            var yDest = y.ShippingDetails.OriginCountry.ToLowerInvariant();
            if (xDest == yDest)
            {
                return 0;
            }
            else if(xDest[0] > yDest[0])
            {
                return 1;
            }

            return -1;
        }
    }
}
