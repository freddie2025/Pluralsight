using Strategy_Pattern_Using_different_shipping_providers.Business.Models;

namespace Strategy_Pattern_Using_different_shipping_providers.Business.Strategies.SalesTax
{
    public interface ISalesTaxStrategy
    {
        public decimal GetTaxFor(Order order);
    }
}
