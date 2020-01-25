using Microsoft.EntityFrameworkCore;
using PeopleViewer.Common;

namespace PersonDataReader.SQL
{
    public class PersonContext : DbContext
    {
        public PersonContext(DbContextOptions<PersonContext> options)
            : base(options)
        { }

        public DbSet<Person> People { get; set; }
    }
}
