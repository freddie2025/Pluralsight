using ConferenceTracker.Data;
using ConferenceTracker.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace ConferenceTracker.Repositories
{
    public class PresentationRepository : IPresentationRepository
    {
        private ApplicationDbContext context;

        public PresentationRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public void Create(Presentation presentation)
        {
            context.Presentations.Add(presentation);
            context.SaveChanges();
        }

        public void Delete(Presentation presentation)
        {
            context.Presentations.Remove(presentation);
            context.SaveChanges();
        }

        public Presentation GetPresentation(int id)
        {
            return context.Presentations
                .Include(p => p.Speaker)
                .FirstOrDefault(m => m.Id == id);
        }

        public List<Presentation> GetAllPresentations()
        {
            return context.Presentations.Include(p => p.Speaker).ToList();
        }

        public void Update(Presentation presentation)
        {
            context.Presentations.Update(presentation);
            context.SaveChanges();
        }
    }
}
