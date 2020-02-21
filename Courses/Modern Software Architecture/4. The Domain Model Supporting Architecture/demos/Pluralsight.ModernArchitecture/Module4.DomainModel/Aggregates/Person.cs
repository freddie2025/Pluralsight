using System;

namespace Module4.DomainModel.Aggregates
{
    public class Person : Party
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public DateTime? DateOfBirth { get; private set; }
        
        protected Person()
        {
            Id = Guid.NewGuid();
        }

        public static class Factory
        {
            public static Person CreateNew(string firstName, string lastName, DateTime? dateOfBirth)
            {
                if (String.IsNullOrWhiteSpace(firstName))
                    throw new ArgumentException("Can't be null or empty", "firstName");
                if (String.IsNullOrWhiteSpace(lastName))
                    throw new ArgumentException("Can't be null or empty", "lastName");

                var person = new Person
                {
                    FirstName = firstName,
                    LastName = lastName,
                    DateOfBirth = dateOfBirth
                };
                return person;
            }
        }
    }
}

