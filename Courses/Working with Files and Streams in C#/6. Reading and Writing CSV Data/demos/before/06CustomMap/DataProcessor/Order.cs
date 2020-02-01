using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataProcessor
{
    public class Order
    {
        public int OrderNumber { get; set; }
        public int CustomerNumber { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
    }
}
