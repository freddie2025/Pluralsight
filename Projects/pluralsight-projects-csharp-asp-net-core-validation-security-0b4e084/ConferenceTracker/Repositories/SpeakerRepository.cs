using ConferenceTracker.Data;
using ConferenceTracker.Entities;
using System.Collections.Generic;
using System.Linq;

namespace ConferenceTracker.Repositories
{
    public class SpeakerRepository : ISpeakerRepository
    {
        private ApplicationDbContext context;

        public SpeakerRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public void Create(Speaker speaker)
        {
            context.Speakers.Add(speaker);
            context.SaveChanges();
        }

        public void Delete(Speaker speaker)
        {
            context.Speakers.Remove(speaker);
            context.SaveChanges();
        }

        public Speaker GetSpeaker(int id)
        {
            return context.Speakers.FirstOrDefault(e => e.Id == id);
        }

        public List<Speaker> GetAllSpeakers()
        {
            return context.Speakers.ToList();
        }

        public void Update(Speaker speaker)
        {
            context.Speakers.Update(speaker);
            context.SaveChanges();
        }
    }
}
