using System;
using System.Collections.Generic;

namespace SessionBuilder.Core
{
    public class Speaker
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime Birthday { get; set; }

        public ICollection<Session> Sessions { get; set; }
    }
}
