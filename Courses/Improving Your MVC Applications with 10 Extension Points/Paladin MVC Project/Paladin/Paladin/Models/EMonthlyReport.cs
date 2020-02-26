using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Paladin.Models
{
    public class EMonthlyReport
    {
        public double Id { get; set; }
        public double NumberRead { get; set; }
        public double ClickThruRate { get; set; }
        public double NumberSent { get; set; }
        public double AverageQuote { get; set; }
        public double ProjectedConversationRate { get; set; }
    }
}