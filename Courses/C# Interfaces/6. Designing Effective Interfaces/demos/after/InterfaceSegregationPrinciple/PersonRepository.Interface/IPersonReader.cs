using System.Collections.Generic;

namespace PersonRepository.Interface
{
    public interface IPersonReader
    {
        IEnumerable<Person> GetPeople();
        Person GetPerson(int id);
    }
}