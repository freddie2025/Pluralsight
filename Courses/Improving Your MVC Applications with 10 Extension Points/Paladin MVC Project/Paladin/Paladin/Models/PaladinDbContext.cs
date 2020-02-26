using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Paladin.Models
{
    public class PaladinDbContext : DbContext
    {
        public PaladinDbContext()
            : base("name=Paladin")
        {
            Database.SetInitializer<PaladinDbContext>(new DropCreateDatabaseIfModelChanges<PaladinDbContext>());
        }

        public DbSet<Applicant> Applicants { get; set; }
        public DbSet<Employment> Employment { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Products> Products { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<EWeeklyReport> WeeklyReports{ get; set; }
        public DbSet<EMonthlyReport> MonthlyReports { get; set; }
    }

    public class PaladinInitializer : DropCreateDatabaseIfModelChanges<PaladinDbContext>
    {
        protected override void Seed(PaladinDbContext context)
        {
            base.Seed(context);
        }
    }
}