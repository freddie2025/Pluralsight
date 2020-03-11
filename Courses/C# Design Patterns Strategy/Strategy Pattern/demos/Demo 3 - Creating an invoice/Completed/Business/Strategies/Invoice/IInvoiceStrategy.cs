using Strategy_Pattern_Creating_an_invoice.Business.Models;

namespace Strategy_Pattern_Creating_an_invoice.Business.Strategies.Invoice
{
    public interface IInvoiceStrategy
    {
        public void Generate(Order order);
    }
}