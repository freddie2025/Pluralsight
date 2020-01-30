using Microsoft.EntityFrameworkCore;
using SessionBuilder.Core.Data;
using System;
using System.Linq;

namespace SessionBuilder.Core
{
    public class SpeakerRepository : ISpeakerRepository
    {
        private readonly SessionBuilderContext context;

        public SpeakerRepository(SessionBuilderContext context)
        {
            this.context = context;
        }

        public Speaker Get(string name)
        {
            return context.Speakers
                   .Include(speaker => speaker.Sessions)
                   .FirstOrDefault(speaker => speaker.Name == name);
        }

        public Speaker Get(Guid id)
        {
            return context.Speakers
                   .Include(speaker => speaker.Sessions)
                   .FirstOrDefault(speaker => speaker.Id == id);
        }

    }

    public interface ISpeakerRepository
    {
        Speaker Get(string name);
        Speaker Get(Guid id);
    }
}
