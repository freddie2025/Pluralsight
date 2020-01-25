using System.Collections.Generic;

namespace PeopleViewer.Common
{
    public interface IPersonReader
    {
        IEnumerable<Person> GetPeople();
        Person GetPerson(int id);
    }
}
