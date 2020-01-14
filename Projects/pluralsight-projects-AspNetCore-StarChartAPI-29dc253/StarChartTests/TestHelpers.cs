using System;
using System.Linq;

namespace StarChartTests
{
    public static class TestHelpers
    {
        private static readonly string _projectName = "StarChart";

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
