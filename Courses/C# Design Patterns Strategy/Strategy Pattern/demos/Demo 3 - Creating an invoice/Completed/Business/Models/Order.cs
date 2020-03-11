using Strategy_Pattern_Creating_an_invoice.Business.Strategies.Invoice;
using Strategy_Pattern_Creating_an_invoice.Business.Strategies.SalesTax;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Strategy_Pattern_Creating_an_invoice.Business.Models
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

        public ISalesTaxStrategy SalesTaxStrategy { get; set; }

        public IInvoiceStrategy InvoiceStrategy { get; set; }

        public decimal GetTax()
        {
            if(SalesTaxStrategy == null)
            {
                return 0m;
            }

            return SalesTaxStrategy.GetTaxFor(this);
        }

        public void FinalizeOrder()
        {
            if(SelectedPayments.Any(x => x.PaymentProvider == PaymentProvider.Invoice) &&
               AmountDue > 0 && 
               ShippingStatus == ShippingStatus.WaitingForPayment)
            {
                InvoiceStrategy.Generate(this);

                ShippingStatus = ShippingStatus.ReadyForShippment;
            }
            else if(AmountDue > 0)
            {
                throw new Exception("Unable to finalize order");
            }
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

        //public decimal GetTax()
        //{
        //    switch (ItemType)
        //    {
        //        case ItemType.Service:
        //        case ItemType.Food:
        //        case ItemType.Hardware:
        //        case ItemType.Literature:
        //        default:
        //            return 0m;
        //    }
        //}

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