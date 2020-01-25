using Common;
using System;
using System.Collections.Generic;

namespace PeopleViewer.Presentation.Tests
{
    public static class TestData
    {
        public static List<Person> testPeople = 
            new List<Person>()
            {
                new Person() {Id = 1,
                    GivenName = "John", FamilyName = "Smith",
                    Rating = 7, StartDate = new DateTime(2000, 10, 1)},
                new Person() {Id = 2,
                    GivenName = "Mary", FamilyName = "Thomas",
                    Rating = 9, StartDate = new DateTime(1971, 7, 23)},
            };
    }
}
