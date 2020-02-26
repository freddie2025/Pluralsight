using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Paladin.Models;

namespace Paladin.Models
{
    public class Applicant
    {
        public int ApplicantId { get; set; }
        public Guid ApplicantTracker { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? Dob { get; set; }
        public double? Phone { get; set; }
        public string Email { get; set; }

        public string MaritalStatus { get; set; }
        public string HighestEducation { get; set; }
        public string LicenseStatus { get; set; }
        public string YearsLicensed { get; set; }

        public virtual List<Address> Addresses { get; set; }
        public virtual List<Employment> Employment { get; set; }
        public virtual List<Vehicle> Vehicle { get; set; }
        public virtual List<Products> Products { get; set; }

        public int WorkFlowStage { get; set; }
    }
}