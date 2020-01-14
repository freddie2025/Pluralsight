using System;
using System.IO;
using System.Linq;
using GradeBook.GradeBooks;
using GradeBook.UserInterfaces;
using Xunit;

namespace GradeBookTests
{
    public class AddWeightedSupportToBaseGradeBook
    {
        /// <summary>
        ///     All Tests related to the "Create IsWeighted Property" task.
        /// </summary>
        [Fact(DisplayName = "Create IsWeighted Property Test @add-isweighted-to-basegradebook")]
        public void CreateIsWeightedTest()
        {
            var isWeightedProperty = typeof(BaseGradeBook).GetProperty("IsWeighted");

            Assert.True(isWeightedProperty != null, "`GradeBook.GradeBooks.BaseGradeBook` does not contain an `IsWeighted` property.");
            Assert.True(isWeightedProperty.GetGetMethod().IsPublic == true, "`GradeBook.GradeBooks.BaseGradeBook`'s `IsWeighted` property is not public.");
            Assert.True(isWeightedProperty.PropertyType == typeof(bool), "`GradeBook.GradeBooks.BaseGradeBook`'s `IsWeighted` is not of type `bool`.");
        }

        [Fact(DisplayName = "Refactor GradeBooks and StartingUserInterface Test @refactor-to-support-isweighted")]
        public void RefactorGradeBooksAndStartingUserInterface()
        {
            // Get the StandardGradeBook type
            var standardGradeBook = TestHelpers.GetUserType("GradeBook.GradeBooks.StandardGradeBook");
            Assert.True(standardGradeBook != null, "`StandardGradeBook` wasn't found in the `GradeBooks.GradeBook` namespace.");

            // Get the StandardGradeBook's constructor
            var constructor = standardGradeBook.GetConstructors().FirstOrDefault();

            // Get the constructor's parameters
            var parameters = constructor.GetParameters();

            // Test that the parameters match what is expected at this point
            Assert.True(parameters.Count() == 2 && parameters[0].ParameterType == typeof(string) && parameters[1].ParameterType == typeof(bool), "`GradeBook.GradeBooks.BaseGradeBook`'s constructor doesn't have the correct parameters. It should be a `string` and then a `bool`.");

            // Note: this doesn't actually test that the refactor was done in all places, code won't compile otherwise once the bool is added to the constructor.
        }


        /// <summary>
        ///     All tests related to the "Set IsWeighted In BaseGradeBook Constructor" task.
        /// </summary>
        [Fact(DisplayName = "Set IsWeighted In BaseGradeBook Constructor Test @set-isweighted-property")]
        public void SetIsWeightedInBaseGradeBookConstructorTest()
        {
            // get standardgradebook type
            var standardGradeBook = TestHelpers.GetUserType("GradeBook.GradeBooks.StandardGradeBook");
            Assert.True(standardGradeBook != null, "`StandardGradeBook` wasn't found in the `GradeBooks.GradeBook` namespace.");

            // Instantiate StandardGradeBook with weighted grading
            object gradeBook = Activator.CreateInstance(standardGradeBook, "WeightedTest", true);

            // Assert that is weighted is true
            Assert.True(gradeBook.GetType().GetProperty("IsWeighted").GetValue(gradeBook).ToString().ToLower() == "true", "`GradeBook.GradeBooks.BaseGradeBook`'s constructor didn't properly set the `IsWeighted` property based on the provided bool parameter");
        }

        /// <summary>
        ///     All Tests related to the "Update StartingUserInterface CreateCommand Method's Condition"
        /// </summary>
        [Fact(DisplayName = "Update StartingUserInterface CreateCommand Methods Condition @add-weighted-to-createcommand")]
        public void UpdateStartingUserInterfacesCreateCommandMethodsCondition()
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
                        StartingUserInterface.CreateCommand("create test standard");
                        output = consolestream.ToString().ToLower();

                        //Test that message written to console when parts.length != 4.
                        Assert.True(output.Contains("command not valid"), "`GradeBook.UserInterfaces.StartingUserInterface` didn't write a message to the console when the create command didn't contain a name, type, and if it was weighted (true / false).");

                        //Test that message written to console is correct.
                        Assert.True(output.Contains("command not valid, create requires a name, type of gradebook, if it's weighted (true / false)."), "`GradeBook.UserInterfaces.StartingUserInterface` didn't write 'Command not valid, Create requires a name, type of gradebook, if it's weighted (true / false)..' to the console when the create command didn't contain both a name and type.");

                        //Test that `CreateCommand` escapes returns without setting the gradebook when parts.Length != 4.
                        Assert.True(!output.Contains("created gradebook"), "`GradeBook.UserInterfaces.StartingUserInterface` still created a gradebook when the create command didn't contain a name, type, if it's weighted (true / false).");
                    }
                }
            }
            finally
            {
                StreamWriter standardOutput = new StreamWriter(Console.OpenStandardOutput());
                Console.SetOut(standardOutput);
                StreamReader standardInput = new StreamReader(Console.OpenStandardInput());
                Console.SetIn(standardInput);
            }

            output = string.Empty;

            try
            {
                using (var consoleInputStream = new StringReader("close"))
                {
                    Console.SetIn(consoleInputStream);
                    using (var consolestream = new StringWriter())
                    {
                        Console.SetOut(consolestream);
                        StartingUserInterface.CreateCommand("create test standard true");
                        output = consolestream.ToString().ToLower();

                        Assert.True(output.Contains("standard"), "`GradeBook.UserInterfaces.StartingUserInterface` didn't create a gradebook when the `CreateCommand` included a name, type, and if it was weighted (true / false).");
                    }
                }
            }
            finally
            {
                StreamWriter standardOutput = new StreamWriter(Console.OpenStandardOutput());
                Console.SetOut(standardOutput);
                StreamReader standardInput = new StreamReader(Console.OpenStandardInput());
                Console.SetIn(standardInput);
            }
        }
    }
}
