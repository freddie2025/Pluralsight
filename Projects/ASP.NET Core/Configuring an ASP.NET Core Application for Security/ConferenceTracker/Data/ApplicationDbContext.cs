using ConferenceTracker.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace ConferenceTracker.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Speaker> Speakers { get; set; }
        public virtual DbSet<Presentation> Presentations { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            if (builder == null) throw new ArgumentNullException(nameof(builder));

            builder.Entity<Speaker>().HasData(
                    new Speaker { Id = 1, FirstName = "Jane", LastName = "Doe", Description = "Place Holder Author One." },
                    new Speaker { Id = 2, FirstName = "John", LastName = "Doe", Description = "Place Holder Author Two." },
                    new Speaker { Id = 3, FirstName = "Emily", LastName = "Smith", Description = "Place Holder Author Three." }
                );
            builder.Entity<Presentation>().HasData(
                new Presentation { Id = 1, Name = "Test Session One", StartDateTime = DateTime.Now, EndDateTime = DateTime.Now, Description = "First example of a test session", SpeakerId = 2 },
                new Presentation { Id = 2, Name = "Test Session Two", StartDateTime = DateTime.Now, EndDateTime = DateTime.Now, Description = "Second example of a test session", SpeakerId = 3 },
                new Presentation { Id = 3, Name = "Test Session Three", StartDateTime = DateTime.Now, EndDateTime = DateTime.Now, Description = "Third example of a test session", SpeakerId = 1 }
                );

            base.OnModelCreating(builder);
        }
    }
}
