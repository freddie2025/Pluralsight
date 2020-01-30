using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SessionBuilder.Core
{
    public class FakeSpeakerRepository : ISpeakerRepository
    {
        public IEnumerable<Speaker> Speakers { get; } = new List<Speaker> {
            new Speaker
            {
                Id = Guid.Parse("5040BF0C-33E3-4AC0-BEA1-6CD4AD971BAA"),
                Name = "Filip Ekberg",
                Birthday = new DateTime(1987, 01, 29),

                Sessions = new [] {
                    new Session
                    {
                        Id = Guid.Parse("593099a7-021c-47d8-bdfd-26eead842ea9"),
                        Title = "The state of C#",
                        Abstract = "In this talk I go through how C# has changed, as well as focusing on what's coming in C# 7.1, 7.2, 8.0 and beyond!",
                        Length = TimeSpan.FromMinutes(40),
                        SubmittedAt = new DateTimeOffset(2016, 02, 29, 00, 01, 00, TimeSpan.FromHours(1)),  // 2016-02-29 00:01:00.0000000 +01:00
                        ScheduledAt = new DateTimeOffset(2019, 08, 01, 09, 40, 00, TimeSpan.FromHours(2))   // 2019-08-01 09:40:00.0000000 +02:00
                    },
                    new Session
                    {
                        Id = Guid.Parse("156ce5c3-7b0f-49b9-8cf3-5cef7075a843"),
                        Title = "C# 8 and Beyond",
                        Abstract = "One of the most popular programming language on the market is getting even better. With every iteration of C# we get more and more features that are meant to make our lives as developers a lot easier. Join me in this session to explore what's new in C# 8, as well as what we can expect in the near (and far) future of C#!",
                        Length = TimeSpan.FromHours(1),
                        SubmittedAt = new DateTimeOffset(2016, 02, 29, 00, 00, 00, TimeSpan.FromHours(1)),  // 2016-02-29 00:00:00.0000000 +01:00
                        ScheduledAt = new DateTimeOffset(2019, 08, 01, 11, 01, 00, TimeSpan.FromHours(2))   // 2019-08-01 11:01:00.0000000 +02:00
                    },
                    new Session
                    {
                        Id = Guid.Parse("057627f8-e44e-4402-8477-cda3ff770e53"),
                        Title = "Succeeding with Xamarin",
                        Abstract = "TBA",
                        Length = TimeSpan.FromMinutes(55),
                        SubmittedAt = new DateTimeOffset(2019, 01, 01, 00, 01, 00, TimeSpan.FromHours(1)),  // 2019-01-01 00:00:00.0000000 +01:00
                        ScheduledAt = new DateTimeOffset(2019, 08, 01, 12, 00, 00, TimeSpan.FromHours(2))   // 2019-08-01 12:00:00.0000000 +02:00
                    },
                    new Session
                    {
                        Id = Guid.Parse("8f9d4719-6d66-4406-ad26-33ea9455c11e"),
                        Title = "Using Dates and Times in .NET",
                        Abstract = "TBA",
                        Length = new TimeSpan(01, 45, 00),
                        SubmittedAt = new DateTimeOffset(2019, 01, 01, 00, 00, 00, TimeSpan.FromHours(1)),  // 2019-01-01 00:00:00.0000000 +01:00
                        ScheduledAt = new DateTimeOffset(2019, 08, 02, 09, 00, 00, TimeSpan.FromHours(2))   // 2019-08-02 09:00:00.0000000 +02:00
                    }
                }
            }
        };

        public Speaker Get(string name)
        {
            return Speakers.FirstOrDefault(speaker => speaker.Name == name);
        }

        public Speaker Get(Guid id)
        {
            return Speakers.FirstOrDefault(speaker => speaker.Id == id);
        }
    }
}
