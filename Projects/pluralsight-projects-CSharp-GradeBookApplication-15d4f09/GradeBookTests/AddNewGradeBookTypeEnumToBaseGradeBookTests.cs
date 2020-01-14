using System;
using System.IO;
using GradeBook.GradeBooks;
using Xunit;

namespace GradeBookTests
{
    /// <summary>
    ///     This class contains all tests related to the "Add New GradeBookType Enum to BaseGradeBook" task group.
    ///     Note: Do not use these tests as example of good testing practices, due to the nature of how PluralSight projects work
    ///     we have to create tests against code that doesn't exist and changes implementation, due to this tests are fragile,
    ///     hard to maintain, and don't don't adhere to the "test just one thing" practice commonly used in production tests.
    /// </summary>t
    public class AddNewGradeBookTypeEnumToBaseGradeBookTests
    {
        /// <summary>
        ///     All Tests related to the "Create a new Enum GradeBookType" Task.
        /// </summary>
        [Fact(DisplayName = "Create New Enum GradeBookType Tests @create-a-new-enum-gradebooktype")]
        public void CreateNewEnumGradeBookTypeTests()
        {
            // Get appropriate path to file for the current operating system
            var filePath = ".." + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + "GradeBook" + Path.DirectorySeparatorChar + "Enums" + Path.DirectorySeparatorChar + "GradeBookType.cs";

            // Assert GradeBookType is in the Enums folder
            Assert.True(File.Exists(filePath), "`GradeBookType.cs` was not found in the `Enums` directory.");

            // Get GradeBookType from the GradeBook.Enums namespace
            var gradebookEnum = TestHelpers.GetUserType("GradeBook.Enums.GradeBookType");

            // Assert GradeBookType was found in the GradeBook.Enums namespace
            Assert.True(gradebookEnum != null, "`GradeBookType` wasn't found in the `GradeBooks.Enums` namespace.");

            // Test to make sure the enum `GradeBookType` is public.
            Assert.True(gradebookEnum.IsPublic, "`GradeBook.Enums.GradeBookType` exists, but isn't `public`.");

            // Test to make sure that `GradeBookType` is an enum not a class
            Assert.True(gradebookEnum.IsEnum, "`GradeBook.Enums.GradeBookType` exists, but isn't an enum.");

            // Test that `GradeBookType` contains the value `Standard`
            Assert.True(Enum.IsDefined(gradebookEnum, "Standard"), "`GradeBook.Enums.GradeBookType` doesn't contain the value `Standard`.");

            // Test that `GradeBookType` contains the value `Ranked`
            Assert.True(Enum.IsDefined(gradebookEnum, "Ranked"), "`GradeBook.Enums.GradeBookType` doesn't contain the value `Ranked`.");

            // Test that `GradeBookType` contains the value `ESNU`
            Assert.True(Enum.IsDefined(gradebookEnum, "ESNU"), "`GradeBook.Enums.GradeBookType` doesn't contain the value `ESNU`.");

            // Test that `GradeBookType` contains the value `OneToFour`
            Assert.True(Enum.IsDefined(gradebookEnum, "OneToFour"), "`GradeBook.Enums.GradeBookType` doesn't contain the value `OneToFour`.");

            // Test that `GradeBookType` contains the value `SixPoint`
            Assert.True(Enum.IsDefined(gradebookEnum, "SixPoint"), "`GradeBook.Enums.GradeBookType` doesn't contain the value `SixPoint`.");
        }

        /// <summary>
        ///     All tests related to the "Add property Type to BaseGradeBook" task.
        /// </summary>
        [Fact(DisplayName = "Add property Type to BaseGradeBook Tests @add-a-gradebooktype-property-to-basegradebook")]
        public void AddPropertyTypeToBaseGradeBookTests()
        {
            // Get property Type from BaseGradeBook
            var typeProperty = typeof(BaseGradeBook).GetProperty("Type");

            // Test that the property Type exists in BaseGradeBook
            Assert.True(typeProperty != null, "`GradeBook.GradeBooks.BaseGradeBook` doesn't contain a property `Type` or `Type` is not `public`.");

            // Get GradeBookType Enum from GradeBook.Enums namespace
            var gradebookEnum = TestHelpers.GetUserType("GradeBook.Enums.GradeBookType");

            // Test that the property Type is of type GradeBookType
            Assert.True(typeProperty.PropertyType == gradebookEnum, "`GradeBook.GradeBooks.BaseGradeBook` contains a property `Type` but it is not of type `GradeBookType`.");

            // Test that the property Type's getter is public
            Assert.True(typeProperty.GetGetMethod() != null, "`GradeBook.GradeBooks.BaseGradeBook` contains a property `Type` but it's getter is not `public`.");

            // Test that the property Type's setter is public
            Assert.True(typeProperty.GetSetMethod() != null, "`GradeBook.GradeBooks.BaseGradeBook` contains a property `Type` but it's setter is not `public`.");
        }
    }
}
