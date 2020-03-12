using System;

namespace ShoppingCart.Business.Models
{
    public class Product
    {
        public string ArticleId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

        public Product()
        {

        }

        public Product(string articleId, string name, decimal price)
        {
            ArticleId = articleId;
            Name = name;
            Price = price;
        }
    }
}
