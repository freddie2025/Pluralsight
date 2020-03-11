using Strategy_Pattern_Creating_an_invoice.Business.Models;

namespace Strategy_Pattern_Creating_an_invoice.Business.Strategies.SalesTax
{
    public interface ISalesTaxStrategy
    {
        public decimal GetTaxFor(Order order);
    }
}
