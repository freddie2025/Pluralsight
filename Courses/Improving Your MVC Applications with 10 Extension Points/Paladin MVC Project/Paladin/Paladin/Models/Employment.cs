using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Paladin.Models
{
    public class Employment
    {
        public int EmploymentId { get; set; }
        public int ApplicantId { get; set; }
        public string EmploymentType { get; set; }
        public string Employer { get; set; }
        public string Position { get; set; }
        public double GrossMonthlyIncome { get; set; }
        public DateTime? StartDate { get; set; }

        public bool IsPrimary { get; set; }
        
        public virtual Applicant Applicant { get; set; }
    }
}