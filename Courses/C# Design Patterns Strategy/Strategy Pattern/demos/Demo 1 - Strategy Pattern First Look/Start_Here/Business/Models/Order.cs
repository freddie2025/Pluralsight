using System.Collections.Generic;
using System.Linq;

namespace Strategy_Pattern_First_Look.Business.Models
{
    public class Order
    {
        public Dictionary<Item, int> LineItems { get; } = new Dictionary<Item, int>();

        public IList<Payment> SelectedPayments { get; } = new List<Payment>();

        public IList<Payment> FinalizedPayments { get; } = new List<Payment>();

        public decimal AmountDue => TotalPrice - FinalizedPayments.Sum(payment => payment.Amount);

        public decimal TotalPrice => LineItems.Sum(item => item.Key.Price * item.Value);

        public ShippingStatus ShippingStatus { get; set; } = ShippingStatus.WaitingForPayment;

        public ShippingDetails ShippingDetails { get; set; }

        public decimal GetTax()
        {
            var destination = ShippingDetails.DestinationCountry.ToLowerInvariant();

            if(destination == "sweden")
            {
                if (destination == ShippingDetails.OriginCountry.ToLowerInvariant())
                {
                    return TotalPrice * 0.25m;
                }

                #region Tax per item
                //if (destination == ShippingDetails.OriginCountry.ToLowerInvariant())
                //{
                //    decimal totalTax = 0m;
                //    foreach (var item in LineItems)
                //    {
                //        switch (item.Key.ItemType)
                //        {
                //            case ItemType.Food:
                //                totalTax += (item.Key.Price * 0.06m) * item.Value;
                //                break;

                //            case ItemType.Literature:
                //                totalTax += (item.Key.Price * 0.08m) * item.Value;
                //                break;

                //            case ItemType.Service:
                //            case ItemType.Hardware:
                //                totalTax += (item.Key.Price * 0.25m) * item.Value;
                //                break;
                //        }
                //    }

                //    return totalTax;
                //}
                #endregion

                return 0;
            }

            if (destination == "us")
            {
                switch (ShippingDetails.DestinationState.ToLowerInvariant())
                {
                    case "la": return TotalPrice * 0.095m;
                    case "ny": return TotalPrice * 0.04m;
                    case "nyc": return TotalPrice * 0.045m;
                    default: return 0m;
                }
            }

            return 0m;
        }
    }

    public class ShippingDetails
    {
        public string Receiver { get; set; }

        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }

        public string PostalCode { get; set; }

        public string DestinationCountry { get; set; }
        public string DestinationState { get; set; }

        public string OriginCountry { get; set; }
        public string OriginState { get; set; }
    }

    public enum ShippingStatus 
    { 
        WaitingForPayment,
        ReadyForShippment,
        Shipped
    }

    public enum PaymentProvider
    {
        Paypal,
        CreditCard,
        Invoice
    }

    public class Payment
    {
        public decimal Amount { get; set; }
        public PaymentProvider PaymentProvider { get; set; }
    }

    public class Item
    {
        public string Id { get; }
        public string Name { get; }
        public decimal Price { get; }

        public ItemType ItemType { get; set; }

        public decimal GetTax()
        {
            switch (ItemType)
            {
                case ItemType.Service:
                case ItemType.Food:
                case ItemType.Hardware:
                case ItemType.Literature:
                default:
                    return 0m;
            }
        }

        public Item(string id, string name, decimal price, ItemType type)
        {
            Id = id;
            Name = name;
            Price = price;
            ItemType = type;
        }
    }

    public enum ItemType
    {
        Service,
        Food,
        Hardware,
        Literature
    }
}