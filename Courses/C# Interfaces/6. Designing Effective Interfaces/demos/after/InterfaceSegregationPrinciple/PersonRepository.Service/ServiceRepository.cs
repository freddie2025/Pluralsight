using Newtonsoft.Json;
using PersonRepository.Interface;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace PersonRepository.Service
{
    public class ServiceRepository : IPersonReader
    {
        WebClient client = new WebClient();
        string baseUri = "http://localhost:9874/api/people";

        public IEnumerable<Person> GetPeople()
        {
            string result = client.DownloadString(baseUri);
            var people = JsonConvert.DeserializeObject<IEnumerable<Person>>(result);
            return people;
        }

        public Person GetPerson(int id)
        {
            return GetPeople().FirstOrDefault(p => p.Id == id);
        }
    }
}
