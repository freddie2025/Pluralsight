using ConferenceTracker.Entities;
using System.Collections.Generic;

namespace ConferenceTracker.Repositories
{
    public interface ISpeakerRepository
    {
        public void Create(Speaker speaker);
        public void Delete(Speaker speaker);
        public Speaker GetSpeaker(int id);
        public List<Speaker> GetAllSpeakers();
        public void Update(Speaker speaker);
    }
}
