using Microsoft.EntityFrameworkCore;
using PersonRepository.Interface;

namespace PersonRepository.SQL
{
    public class PersonContext : DbContext
    {
        public PersonContext(DbContextOptions<PersonContext> options)
            : base(options)
        { }

        public DbSet<Person> People { get; set; }
    }
}
