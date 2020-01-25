using System.Collections.Generic;

namespace Common
{
    public interface IPersonRepository
    {
        IEnumerable<Person> GetPeople();
        Person GetPerson(int id);
    }
}
