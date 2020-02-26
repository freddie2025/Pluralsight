using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Paladin.Models
{
    public class Address
    {
        public int AddressId { get; set; }
        public int ApplicantId { get; set; }
        [Display(Name = "Street Address")]
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public bool IsMailing { get; set; }
        
        public virtual Applicant Applicant { get; set; }
    }
}