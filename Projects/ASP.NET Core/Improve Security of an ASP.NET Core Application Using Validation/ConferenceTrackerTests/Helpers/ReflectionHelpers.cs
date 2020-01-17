using System;
using System.Linq;

namespace ConferenceTrackerTests.Helpers
{
    public class ReflectionHelpers
    {
        private static readonly string _projectName = "ConferenceTracker";

        public static Type GetUserType(string fullName)
        {
            return (from assembly in AppDomain.CurrentDomain.GetAssemblies()
                    where assembly.FullName.StartsWith(_projectName)
                    from type in assembly.GetTypes()
                    where type.FullName == fullName
                    select type).FirstOrDefault();
        }
    }
}
