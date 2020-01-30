using Microsoft.EntityFrameworkCore;

namespace SessionBuilder.Core.Data
{
    public class SessionBuilderContext : DbContext
    {
        public SessionBuilderContext(DbContextOptions<SessionBuilderContext> options)
            : base(options)
        { }

        public DbSet<Session> Sessions { get; set; }
        public DbSet<Speaker> Speakers { get; set; }
    }    
}
