using Microsoft.EntityFrameworkCore;
using PeopleViewer.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PersonDataReader.SQL
{
    public class SQLReader : IPersonReader
    {
        DbContextOptions<PersonContext> options;

        public SQLReader()
        {
            var optionsBuilder = new DbContextOptionsBuilder<PersonContext>();
            optionsBuilder.UseSqlite("Data Source=people.db");
            options = optionsBuilder.Options;
        }

        public IEnumerable<Person> GetPeople()
        {
            using (var context = new PersonContext(options))
            {
                return context.People.ToList();
            }
        }

        public Person GetPerson(int id)
        {
            return GetPeople().FirstOrDefault(p => p.Id == id);
        }
    }
}
