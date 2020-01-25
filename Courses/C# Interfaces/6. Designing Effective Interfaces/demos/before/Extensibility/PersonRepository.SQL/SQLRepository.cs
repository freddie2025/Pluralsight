using Microsoft.EntityFrameworkCore;
using PersonRepository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PersonRepository.SQL
{
    public class SQLRepository : IPersonRepository
    {
        DbContextOptions<PersonContext> options;

        public SQLRepository()
        {
            var optionsBuilder = new DbContextOptionsBuilder<PersonContext>();
            optionsBuilder.UseSqlite("Data Source=People.db");
            options = optionsBuilder.Options;
        }

        public IEnumerable<Person> GetPeople()
        {
            using (var context = new PersonContext(options))
            {
                return context.People.ToArray();
            }
        }

        public Person GetPerson(int id)
        {
            using (var context = new PersonContext(options))
            {
                return context.People.Where(p => p.Id == id).FirstOrDefault();
            }
        }

        public void AddPerson(Person newPerson)
        {
            using (var context = new PersonContext(options))
            {
                context.People.Add(newPerson);
                context.SaveChanges();
            }
        }

        public void UpdatePerson(int id, Person updatedPerson)
        {
            using (var context = new PersonContext(options))
            {
                var person = context.People.FirstOrDefault(p => p.Id == id);
                if (person != null)
                {
                    person.Id = updatedPerson.Id;
                    person.GivenName = updatedPerson.GivenName;
                    person.FamilyName = updatedPerson.FamilyName;
                    person.StartDate = updatedPerson.StartDate;
                    person.Rating = updatedPerson.Rating;
                    person.FormatString = updatedPerson.FormatString;
                    context.SaveChanges();
                }
            }
        }

        public void DeletePerson(int id)
        {
            using (var context = new PersonContext(options))
            {
                var person = context.People.FirstOrDefault(p => p.Id == id);
                if (person != null)
                {
                    context.People.Remove(person);
                    context.SaveChanges();
                }
            }
        }
    }
}
