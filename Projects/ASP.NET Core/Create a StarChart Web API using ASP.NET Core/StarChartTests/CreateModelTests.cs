using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using Xunit;

namespace StarChartTests
{
    public class CreateModelTests
    {
        [Fact(DisplayName = "Create CelestialObject @create-celestialobject")]
        public void CreateCelestialObjectModelTest()
        {
            var filePath = ".." + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + "StarChart" + Path.DirectorySeparatorChar + "Models" + Path.DirectorySeparatorChar + "CelestialObject.cs";
            Assert.True(File.Exists(filePath), "`CelestialObject.cs` was not found in the `Models` directory.");

            var model = TestHelpers.GetUserType("StarChart.Models.CelestialObject");
            Assert.True(model != null, "A `public` class `CelestialObject` was not found in the `StarChart.Models` namespace.");
        }

        [Fact(DisplayName = "Add Id Property @add-id-property")]
        public void AddIdPropertyTest()
        {
            var filePath = ".." + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + "StarChart" + Path.DirectorySeparatorChar + "Models" + Path.DirectorySeparatorChar + "CelestialObject.cs";
            Assert.True(File.Exists(filePath), "`CelestialObject.cs` was not found in the `Models` directory.");

            var model = TestHelpers.GetUserType("StarChart.Models.CelestialObject");
            Assert.True(model != null, "A `public` class `CelestialObject` was not found in the `StarChart.Models` namespace.");

            var idProperty = model.GetProperty("Id");
            Assert.True(idProperty != null, "A `public` property `Id` was not found in the `CelestialObject` class.");
            Assert.True(idProperty.PropertyType == typeof(int), "A `public` property `Id` was found in `CelestialObject`, but was not of type `int`.");
        }

        [Fact(DisplayName = "Add Name Property @add-name-property")]
        public void AddNamePropertyTest()
        {
            var filePath = ".." + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + "StarChart" + Path.DirectorySeparatorChar + "Models" + Path.DirectorySeparatorChar + "CelestialObject.cs";
            Assert.True(File.Exists(filePath), "`CelestialObject.cs` was not found in the `Models` directory.");

            var model = TestHelpers.GetUserType("StarChart.Models.CelestialObject");
            Assert.True(model != null, "A `public` class `CelestialObject` was not found in the `StarChart.Models` namespace.");

            var nameProperty = model.GetProperty("Name");
            Assert.True(nameProperty != null, "A `public` property `Name` was not found in the `CelestialObject` class.");
            Assert.True(nameProperty.PropertyType == typeof(string), "A `public` property `Name` was found in `CelestialObject`, but was not of type `string`.");
            Assert.True(nameProperty.GetCustomAttributes(typeof(RequiredAttribute),false).Any(), "A `public` property `Name` was found in `CelestialObject`, but does not have the `Required` attribute.");
        }

        [Fact(DisplayName = "Add OrbitedObjectId Property @add-orbitedobject-property")]
        public void AddOrbitedObjectPropertyTest()
        {
            var filePath = ".." + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + "StarChart" + Path.DirectorySeparatorChar + "Models" + Path.DirectorySeparatorChar + "CelestialObject.cs";
            Assert.True(File.Exists(filePath), "`CelestialObject.cs` was not found in the `Models` directory.");

            var model = TestHelpers.GetUserType("StarChart.Models.CelestialObject");
            Assert.True(model != null, "A `public` class `CelestialObject` was not found in the `StarChart.Models` namespace.");

            var property = model.GetProperty("OrbitedObjectId");
            Assert.True(property != null, "A `public` property `OrbitedObjectId` was not found in the `CelestialObject` class.");
            Assert.True(property.PropertyType == typeof(int?), "A `public` property `OrbitedObjectId` was found in `CelestialObject`, but was not of type `int?`.");
        }

        [Fact(DisplayName = "Add Satellites Property @add-satellites-property")]
        public void AddSatellitesPropertyTest()
        {
            var filePath = ".." + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + "StarChart" + Path.DirectorySeparatorChar + "Models" + Path.DirectorySeparatorChar + "CelestialObject.cs";
            Assert.True(File.Exists(filePath), "`CelestialObject.cs` was not found in the `Models` directory.");

            var model = TestHelpers.GetUserType("StarChart.Models.CelestialObject");
            Assert.True(model != null, "A `public` class `CelestialObject` was not found in the `StarChart.Models` namespace.");

            var property = model.GetProperty("Satellites");
            Assert.True(property != null, "A `public` property `Satellites` was not found in the `CelestialObject` class.");
            Assert.True(property.PropertyType.AssemblyQualifiedName.Contains("List") && property.PropertyType.AssemblyQualifiedName.Contains("CelestialObject"), "A `public` property `Satellites` was found in `CelestialObject`, but was not of type `List<CelestialObject>`.");
            Assert.True(property.GetCustomAttributes(typeof(NotMappedAttribute), false).Any(), "A `public` property `Satellites` was found in `CelestialObject`, but does not have the `NotMapped` attribute.");
        }

        [Fact(DisplayName = "Add OrbitalPeriod Property @add-orbitalperiod-property")]
        public void AddOrbitalPeriodPropertyTest()
        {
            var filePath = ".." + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + "StarChart" + Path.DirectorySeparatorChar + "Models" + Path.DirectorySeparatorChar + "CelestialObject.cs";
            Assert.True(File.Exists(filePath), "`CelestialObject.cs` was not found in the `Models` directory.");

            var model = TestHelpers.GetUserType("StarChart.Models.CelestialObject");
            Assert.True(model != null, "A `public` class `CelestialObject` was not found in the `StarChart.Models` namespace.");

            var property = model.GetProperty("OrbitalPeriod");
            Assert.True(property != null, "A `public` property `OrbitalPeriod` was not found in the `CelestialObject` class.");
            Assert.True(property.PropertyType == typeof(TimeSpan), "A `public` property `OrbitalPeriod` was found in `CelestialObject`, but was not of type `TimeSpan`.");
        }

        [Fact(DisplayName = "Add CelestialObject to ApplicationDbContext @add-celestialobject-to-applicationdbcontext")]
        public void AddCelestialObjectToApplicationDbContextTest()
        {
            var filePath = ".." + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + "StarChart" + Path.DirectorySeparatorChar + "Data" + Path.DirectorySeparatorChar + "ApplicationDbContext.cs";
            Assert.True(File.Exists(filePath), "`ApplicationDbContext.cs` was not found in the `Data` directory, did you remove or rename it?");

            filePath = ".." + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + "StarChart" + Path.DirectorySeparatorChar + "Models" + Path.DirectorySeparatorChar + "CelestialObject.cs";
            Assert.True(File.Exists(filePath), "`CelestialObject.cs` was not found in the `Models` directory.");

            var model = TestHelpers.GetUserType("StarChart.Models.CelestialObject");
            Assert.True(model != null, "A `public` class `CelestialObject` was not found in the `StarChart.Models` namespace.");

            var context = TestHelpers.GetUserType("StarChart.Data.ApplicationDbContext");
            Assert.True(model != null, "A `public` class `ApplicationDbContext` was not found in the `StarChart.Data` namespace.");

            var property = context.GetProperty("CelestialObjects");
            Assert.True(property != null, "A `public` property `CelestialObjects` was not found in the `ApplicationDbContext` class.");
            Assert.True(property.PropertyType.AssemblyQualifiedName.Contains("DbSet") && property.PropertyType.AssemblyQualifiedName.Contains("CelestialObject"), "A `public` property `CelestialObjects` was found in `ApplicationDbContext`, but was not of type `DbSet<CelestialObject>`.");
        }
    }
}
