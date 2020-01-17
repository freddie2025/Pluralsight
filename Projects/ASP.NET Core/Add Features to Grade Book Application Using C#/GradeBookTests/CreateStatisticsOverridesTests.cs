using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using GradeBook;
using GradeBook.Enums;
using Xunit;

namespace GradeBookTests
{
    public class CreateStatisticsOverridesTests
    {
        /// <summary>
        ///     All tests related to the "Create CalculateStatistics Override" Task.
        /// </summary>
        [Fact(DisplayName = "Create CalculateStatistics Override @create-override-calculatestatistics")]
        public void OverrideCalculateStatisticsTest()
        {
            //Setup Test
            var rankedGradeBook = TestHelpers.GetUserType("GradeBook.GradeBooks.RankedGradeBook");
            Assert.True(rankedGradeBook != null, "`RankedGradeBook` wasn't found in the `GradeBooks.GradeBook` namespace.");

            var ctor = rankedGradeBook.GetConstructors().FirstOrDefault();

            var parameters = ctor.GetParameters();
            object gradeBook = null;
            if (parameters.Count() == 2 && parameters[0].ParameterType == typeof(string) && parameters[1].ParameterType == typeof(bool))
                gradeBook = Activator.CreateInstance(rankedGradeBook, "Test GradeBook", true);
            else if (parameters.Count() == 1 && parameters[0].ParameterType == typeof(string))
                gradeBook = Activator.CreateInstance(rankedGradeBook, "Test GradeBook");

            MethodInfo method = rankedGradeBook.GetMethod("CalculateStatistics");
            var output = string.Empty;

            try
            {
                //Test that message was written to console when there are less than 5 students.
                using (var consolestream = new StringWriter())
                {
                    Console.SetOut(consolestream);
                    method.Invoke(gradeBook, null);
                    output = consolestream.ToString().ToLower();

                    Assert.True(output.Contains("5 students") || output.Contains("five students"), "`GradeBook.GradeBooks.RankedGradeBook.CalculateStatistics` didn't respond with 'Ranked grading requires at least 5 students.' when there were less than 5 students.");

                    //Test that the base calculate statistics didn't still run when there were less than 5 students.
                    Assert.True(!output.Contains("average grade of all students is"), "`GradeBook.GradeBooks.RankedGradeBook.CalculateStatistics` still ran the base `CalculateStatistics` when there was less than 5 students.");
                }
            }
            finally
            {
                StreamWriter standardOutput = new StreamWriter(Console.OpenStandardOutput());
                Console.SetOut(standardOutput);
            }

            var students = new List<Student>
                {
                    new Student("jamie",StudentType.Standard,EnrollmentType.Campus)
                    {
                        Grades = new List<double>{ 100 }
                    },
                    new Student("john",StudentType.Standard,EnrollmentType.Campus)
                    {
                        Grades = new List<double>{ 75 }
                    },
                    new Student("jackie",StudentType.Standard,EnrollmentType.Campus)
                    {
                        Grades = new List<double>{ 50 }
                    },
                    new Student("tom",StudentType.Standard,EnrollmentType.Campus)
                    {
                        Grades = new List<double>{ 25 }
                    },
                    new Student("tony",StudentType.Standard,EnrollmentType.Campus)
                    {
                        Grades = new List<double>{ 0 }
                    }
                };

            gradeBook.GetType().GetProperty("Students").SetValue(gradeBook, students);

            //Test that the base calculate statistics did run when there were 5 or more students.
            output = string.Empty;

            try
            {
                using (var consolestream = new StringWriter())
                {
                    Console.SetOut(consolestream);
                    method.Invoke(gradeBook, null);
                    output = consolestream.ToString().ToLower();

                    Assert.True(output.Contains("average grade of all students is"), "`GradeBook.GradeBooks.RankedGradeBook.CalculateStatistics` did not run the base `CalculateStatistics` when there was 5 or more students.");
                }
            }
            finally
            {
                StreamWriter standardOutput = new StreamWriter(Console.OpenStandardOutput());
                Console.SetOut(standardOutput);
            }
        }

        /// <summary>
        ///     All tests related to the "Create CalculateStudentStatistics Override" Task.
        /// </summary>
        [Fact(DisplayName = "Create CalculateStudentStatistics Override @create-override-calculatestudentstatistics")]
        public void OverrideCalculateStudentStatisticsTest()
        {
            //Setup Test
            var rankedGradeBook = TestHelpers.GetUserType("GradeBook.GradeBooks.RankedGradeBook");
            Assert.True(rankedGradeBook != null, "GradeBook.GradeBooks.RankedGradeBook doesn't exist.");

            var ctor = rankedGradeBook.GetConstructors().FirstOrDefault();
            Assert.True(ctor != null, "No constructor found for GradeBook.GradeBooks.RankedGradeBook.");

            var parameters = ctor.GetParameters();
            object gradeBook = null;
            if (parameters.Count() == 2 && parameters[0].ParameterType == typeof(string) && parameters[1].ParameterType == typeof(bool))
                gradeBook = Activator.CreateInstance(rankedGradeBook, "Test GradeBook", true);
            else if (parameters.Count() == 1 && parameters[0].ParameterType == typeof(string))
                gradeBook = Activator.CreateInstance(rankedGradeBook, "Test GradeBook");
            Assert.True(gradeBook != null, "The constructor for GradeBook.GradeBooks.RankedGradeBook have the expected parameters.");

            MethodInfo method = rankedGradeBook.GetMethod("CalculateStudentStatistics");

            var students = new List<Student>
            {
                new Student("jamie",StudentType.Standard,EnrollmentType.Campus)
                {
                    Grades = new List<double>{ 100 }
                }
            };

            gradeBook.GetType().GetProperty("Students").SetValue(gradeBook, students);

            var output = string.Empty;

            try
            {
                //Test that message was written to console when there are less than 5 students.
                using (var consolestream = new StringWriter())
                {
                    Console.SetOut(consolestream);
                    method.Invoke(gradeBook, new object[] { "jamie" });
                    output = consolestream.ToString().ToLower();

                    Assert.True(output.Contains("5 students") || output.Contains("five students"), "`GradeBook.GradeBooks.RankedGradeBook.CalculateStudentStatistics` didn't respond with 'Ranked grading requires at least 5 students.' when there were less than 5 students.");

                    //Test that the base calculate statistics didn't still run when there were less than 5 students.
                    Assert.True(!output.Contains("grades:"), "`GradeBook.GradeBooks.RankedGradeBook.CalculateStudentStatistics` still ran the base `CalculateStudentStatistics` when there was less than 5 students.");
                }
            }
            finally
            {
                StreamWriter standardOutput = new StreamWriter(Console.OpenStandardOutput());
                Console.SetOut(standardOutput);
            }

            students = new List<Student>
                {
                    new Student("jamie",StudentType.Standard,EnrollmentType.Campus)
                    {
                        Grades = new List<double>{ 100 }
                    },
                    new Student("john",StudentType.Standard,EnrollmentType.Campus)
                    {
                        Grades = new List<double>{ 75 }
                    },
                    new Student("jackie",StudentType.Standard,EnrollmentType.Campus)
                    {
                        Grades = new List<double>{ 50 }
                    },
                    new Student("tom",StudentType.Standard,EnrollmentType.Campus)
                    {
                        Grades = new List<double>{ 25 }
                    },
                    new Student("tony",StudentType.Standard,EnrollmentType.Campus)
                    {
                        Grades = new List<double>{ 0 }
                    }
                };

            gradeBook.GetType().GetProperty("Students").SetValue(gradeBook, students);

            //Test that the base calculate statistics did run when there were 5 or more students.
            output = string.Empty;

            try
            {
                using (var consolestream = new StringWriter())
                {
                    Console.SetOut(consolestream);
                    method.Invoke(gradeBook, new object[] { "jamie" });
                    output = consolestream.ToString().ToLower();

                    Assert.True(output.Contains("grades:"), "`GradeBook.GradeBooks.RankedGradeBook.CalculateStudentStatistics` did not run the base `CalculateStudentStatistics` when there was 5 or more students.");
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
