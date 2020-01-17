using System;
using System.IO;
using GradeBook.UserInterfaces;
using Xunit;

namespace GradeBookTests
{
    public class AddWeightedSupportToStartingUserInterfaceTests
    {
        /// <summary>
        ///     Tests Help Command update to ensure all changes were made correctly.
        /// </summary>
        [Fact(DisplayName = "Update HelpCommand Method @update-helpcommand-method")]
        public void UpdateHelpCommandTest()
        {
            //Setup Test
            var output = string.Empty;

            try
            {
                using (var consoleInputStream = new StringReader("close"))
                {
                    Console.SetIn(consoleInputStream);
                    using (var consolestream = new StringWriter())
                    {
                        Console.SetOut(consolestream);
                        StartingUserInterface.HelpCommand();
                        output = consolestream.ToString().ToLower();

                        // Test if help command message is correct
                        Assert.True(output.Contains("create 'name' 'type' 'weighted' - creates a new gradebook where 'name' is the name of the gradebook, 'type' is what type of grading it should use, and 'weighted' is whether or not grades should be weighted (true or false)."), "`GradeBook.UserInterfaces.StartingUserInterface.HelpCommand` didn't write \"Create 'Name' 'Type' 'Weighted' - Creates a new gradebook where 'Name' is the name of the gradebook, 'Type' is what type of grading it should use, and 'Weighted' is whether or not grades should be weighted (true or false).\"");
                    }
                }
            }
            finally
            {
                StreamWriter standardOutput = new StreamWriter(Console.OpenStandardOutput());
                Console.SetOut(standardOutput);
            }
        }
    }
}
