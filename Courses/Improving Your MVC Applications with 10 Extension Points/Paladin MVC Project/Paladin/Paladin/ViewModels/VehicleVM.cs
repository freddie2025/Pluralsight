using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Paladin.ViewModels
{
    public class VehicleVM
    {
        public double Year { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        [Display(Name = "Body Type")]
        public string BodyType { get; set; }
        [Display(Name = "Primary Use")]
        public string PrimaryUse { get; set; }
        [Display(Name = "Own / Lease")]
        public string OwnLease { get; set; }
    }
}