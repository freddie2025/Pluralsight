using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Paladin.Models
{
    public class Products
    {
        public int ProductsId { get; set; }
        public int ApplicantId { get; set; }
        public double Liability { get; set; }
        public bool RoadSideAssistance { get; set; }
        public double PropertyDamage { get; set; }
        public double Collision { get; set; }
        public double Comprehensive { get; set; }
        public bool Rental { get; set; }
        public bool LoanPayoff { get; set; }
        public bool DriverRewards { get; set; }

        public virtual Applicant Applicant { get; set; }
    }
}