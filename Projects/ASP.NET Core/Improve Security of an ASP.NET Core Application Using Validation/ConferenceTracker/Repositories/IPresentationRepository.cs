using ConferenceTracker.Entities;
using System.Collections.Generic;

namespace ConferenceTracker.Repositories
{
    public interface IPresentationRepository
    {
        public void Create(Presentation presentation);
        public void Delete(Presentation presentation);
        public Presentation GetPresentation(int id);
        public List<Presentation> GetAllPresentations();
        public void Update(Presentation presentation);
    }
}
