using ConferenceTracker.Entities;
using ConferenceTracker.Repositories;
using System;
using System.Collections.Generic;

namespace ConferenceTrackerTests.Helpers
{
    public class TestSpeakerRepository : ISpeakerRepository
    {
        public void Create(Speaker speaker)
        {
            return;
        }

        public void Delete(Speaker speaker)
        {
            throw new NotImplementedException();
        }

        public List<Speaker> GetAllSpeakers()
        {
            throw new NotImplementedException();
        }

        public Speaker GetSpeaker(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Speaker speaker)
        {
            throw new NotImplementedException();
        }
    }
}
