using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Paladin.Models
{
    public class Vehicle
    {
        public int VehicleId { get; set; }
        public int ApplicantId { get; set; }
        public double Year { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string BodyType { get; set; }
        public string PrimaryUse { get; set; }
        public string OwnLease { get; set; }
        
        public virtual Applicant Applicant { get; set; }
    }
}