using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using StarChart.Data;
using Moq;
using Xunit;
using Microsoft.EntityFrameworkCore;

namespace StarChartTests
{
    public class CreateControllerTests
    {
        [Fact(DisplayName = "Create Controller @create-controller")]
        public void CreateControllerTest()
        {
            var filePath = ".." + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + "StarChart" + Path.DirectorySeparatorChar + "Controllers" + Path.DirectorySeparatorChar + "CelestialObjectController.cs";
            Assert.True(File.Exists(filePath), "`CelestialObjectController.cs` was not found in the `Controllers` directory.");

            var controller = TestHelpers.GetUserType("StarChart.Controllers.CelestialObjectController");
            Assert.True(controller != null, "A `public` class `CelestialObjectController` was not found in the `StarChart.Controllers` namespace.");
            Assert.True(controller.BaseType == typeof(ControllerBase), "A `public` class `CelestialObjectController` was found, but is not inheriting the `ControllerBase` class.");
        }

        [Fact(DisplayName = "Add Attributes to Controller @add-attributes-to-controller")]
        public void AddAttributesToControllerTest()
        {
            var filePath = ".." + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + "StarChart" + Path.DirectorySeparatorChar + "Controllers" + Path.DirectorySeparatorChar + "CelestialObjectController.cs";
            Assert.True(File.Exists(filePath), "`CelestialObjectController.cs` was not found in the `Controllers` directory.");

            var controller = TestHelpers.GetUserType("StarChart.Controllers.CelestialObjectController");
            Assert.True(controller != null, "A `public` class `CelestialObjectController` was not found in the `StarChart.Controllers` namespace.");
            var routeAttribute = controller.GetCustomAttributes(typeof(RouteAttribute), false).FirstOrDefault() as RouteAttribute;
            Assert.True(routeAttribute != null && routeAttribute.Template == "", @"A `public` class `CelestialObjectController` was found, but didn't have a `Route` attribute with an argument of `""""`.");
            Assert.True(controller.GetCustomAttributes(typeof(ApiControllerAttribute), false).Any(), "A `public` class `CelestialObjectController` was found, but didn't have the `ApiController` attribute.");
        }

        [Fact(DisplayName = "Create Private Property @create-private-property")]
        public void CreatePrivatePropertyTest()
        {
            var filePath = ".." + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + "StarChart" + Path.DirectorySeparatorChar + "Controllers" + Path.DirectorySeparatorChar + "CelestialObjectController.cs";
            Assert.True(File.Exists(filePath), "`CelestialObjectController.cs` was not found in the `Controllers` directory.");

            var controller = TestHelpers.GetUserType("StarChart.Controllers.CelestialObjectController");
            Assert.True(controller != null, "A `public` class `CelestialObjectController` was not found in the `StarChart.Controllers` namespace.");

            var property = controller.GetField("_context", BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.True(property != null, "`CelestialObjectController` does not appear to contain a `private` `readonly` field `_context` of type `ApplicationDbContext`.");
            Assert.True(property.FieldType == typeof(ApplicationDbContext), "`CelestialObjectController` has a `_context` field, but it is not of type `ApplicationDbContext`.");
            Assert.True(property.IsInitOnly, "`CelestialObjectController` has a `_context` field but it is not `readonly`.");
        }

        [Fact(DisplayName = "Create Controller @create-constructor")]
        public void CreateConstructorTest()
        {
            var filePath = ".." + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + "StarChart" + Path.DirectorySeparatorChar + "Controllers" + Path.DirectorySeparatorChar + "CelestialObjectController.cs";
            Assert.True(File.Exists(filePath), "`CelestialObjectController.cs` was not found in the `Controllers` directory.");

            var controller = TestHelpers.GetUserType("StarChart.Controllers.CelestialObjectController");
            Assert.True(controller != null, "A `public` class `CelestialObjectController` was not found in the `StarChart.Controllers` namespace.");

            var constructor = controller.GetConstructors().FirstOrDefault();
            var parameters = constructor.GetParameters();

            Assert.True((parameters.Count() == 1 && parameters[0]?.ParameterType == typeof(ApplicationDbContext)), "`CelestialObjectController` did not contain a constructor with a parameter of type `ApplicationDbContext`.");

            var optionsBuilder = new DbContextOptionsBuilder();
            var context = new Mock<ApplicationDbContext>(optionsBuilder.Options);
            var celestialController = Activator.CreateInstance(controller, new object[] { context.Object });
            Assert.True(controller.GetField("_context", BindingFlags.NonPublic | BindingFlags.Instance)?.GetValue(celestialController) == context.Object, "`CelestialObjectController`'s constructor did not set the `_context` field based on the provided `ApplicationDbContext` parameter.");
        }
    }
}
