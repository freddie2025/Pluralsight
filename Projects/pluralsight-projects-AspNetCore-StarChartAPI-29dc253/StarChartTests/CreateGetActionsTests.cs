using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StarChart.Data;
using Xunit;

namespace StarChartTests
{
    public class CreateGetActionsTests
    {
        [Fact(DisplayName = "Create GetById Action @create-getbyid-action")]
        public void CreateGetByIdActionTest()
        {
            var filePath = ".." + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + "StarChart" + Path.DirectorySeparatorChar + "Controllers" + Path.DirectorySeparatorChar + "CelestialObjectController.cs";
            Assert.True(File.Exists(filePath), "`CelestialObjectController.cs` was not found in the `Controllers` directory.");

            var controller = TestHelpers.GetUserType("StarChart.Controllers.CelestialObjectController");
            Assert.True(controller != null, "A `public` class `CelestialObjectController` was not found in the `StarChart.Controllers` namespace.");

            var model = TestHelpers.GetUserType("StarChart.Models.CelestialObject");

            var item = Activator.CreateInstance(model);
            model.GetProperty("Id").SetValue(item, 1);
            model.GetProperty("Name").SetValue(item, "Sun");
            var item2 = Activator.CreateInstance(model);
            model.GetProperty("Id").SetValue(item2, 2);
            model.GetProperty("Name").SetValue(item2, "Earth");
            model.GetProperty("OrbitedObjectId").SetValue(item2, 1);

            var optionsBuilder = new DbContextOptionsBuilder();
            optionsBuilder.UseInMemoryDatabase("Test");
            var context = new ApplicationDbContext(optionsBuilder.Options);

            var celestialController = Activator.CreateInstance(controller, new object[] { context });

            context.Add(item);
            context.Add(item2);
            context.SaveChanges();

            var method = controller.GetMethod("GetById", new Type[] { typeof(int) });
            Assert.True(method != null, "`CelestialObjectController` does not contain a `GetById` action that accepts an `int` parameter.");
            Assert.True(method.ReturnType == typeof(IActionResult), "`CelestialObjectController`'s `GetById` action was found, but does not have a return type of `IActionResult`.");
            var getAttribute = method.GetCustomAttributes(typeof(HttpGetAttribute), false).FirstOrDefault() as HttpGetAttribute;
            Assert.True(getAttribute != null && getAttribute.Template == "{id:int}", "`CelestialObjectController`'s `GetById` action was found, but does not have an `HttpGet` attribute with a template of `{id:int}`.");
            var notFoundResults = method.Invoke(celestialController, new object[] { 3 }) as NotFoundResult;
            Assert.True(notFoundResults != null, "`CelestialObjectController`'s `GetById` action did not return the `NotFound` when no `CelestialObject` with a matching `Id` was found.");
            var okResults = method.Invoke(celestialController, new object[] { 1 }) as OkObjectResult;
            Assert.True(okResults != null && okResults.Value != null, "`CelestialObjectController`'s `GetById` action did not return an `Ok` with the `CelestialObject` that has a matching `Id` when one was found.");
            Assert.True((int)model.GetProperty("Id")?.GetValue(okResults.Value) == 1, "`CelestialObjectController`'s `GetById` action returned an `Ok` with a `CelestialObject`, however; the `Id` does not appear to match the one provided by the parameter.");
            Assert.True(model.GetProperty("Satellites")?.GetValue(okResults.Value) != null, "`CelestialObjectController`'s `GetById` action returned an `Ok` with a `CelestialObject`, however; the `Satellites` property was not set.");
        }

        [Fact(DisplayName = "Create GetByName Action @create-getbyname-action")]
        public void CreateGetByNameActionTest()
        {
            var filePath = ".." + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + "StarChart" + Path.DirectorySeparatorChar + "Controllers" + Path.DirectorySeparatorChar + "CelestialObjectController.cs";
            Assert.True(File.Exists(filePath), "`CelestialObjectController.cs` was not found in the `Controllers` directory.");

            var controller = TestHelpers.GetUserType("StarChart.Controllers.CelestialObjectController");
            Assert.True(controller != null, "A `public` class `CelestialObjectController` was not found in the `StarChart.Controllers` namespace.");

            var model = TestHelpers.GetUserType("StarChart.Models.CelestialObject");

            var item = Activator.CreateInstance(model);
            model.GetProperty("Id").SetValue(item, 1);
            model.GetProperty("Name").SetValue(item, "Sun");
            var item2 = Activator.CreateInstance(model);
            model.GetProperty("Id").SetValue(item2, 2);
            model.GetProperty("Name").SetValue(item2, "Earth");
            model.GetProperty("OrbitedObjectId").SetValue(item2, 1);

            var optionsBuilder = new DbContextOptionsBuilder();
            optionsBuilder.UseInMemoryDatabase("Test2");
            var context = new ApplicationDbContext(optionsBuilder.Options);

            var celestialController = Activator.CreateInstance(controller, new object[] { context });

            context.Add(item);
            context.Add(item2);
            context.SaveChanges();

            var method = controller.GetMethod("GetByName", new Type[] { typeof(string) });
            Assert.True(method != null, "`CelestialObjectController` does not contain a `GetByName` action that accepts a `string` parameter.");
            Assert.True(method.ReturnType == typeof(IActionResult), "`CelestialObjectController`'s `GetByName` action was found, but does not have a return type of `IActionResult`.");
            var getAttribute = method.GetCustomAttributes(typeof(HttpGetAttribute), false).FirstOrDefault() as HttpGetAttribute;
            Assert.True(getAttribute != null && getAttribute.Template == "{name}", "`CelestialObjectController`'s `GetByName` action was found, but does not have an `HttpGet` attribute with a template of `{name}`.");
            var notFoundResults = method.Invoke(celestialController, new object[] { "Bob" }) as NotFoundResult;
            Assert.True(notFoundResults != null, "`CelestialObjectController`'s `GetByName` action did not return the `NotFound` when no `CelestialObject` with a matching `Name` was found.");
            var okResults = method.Invoke(celestialController, new object[] { "Sun" }) as OkObjectResult;
            Assert.True(okResults != null && okResults.Value != null, "`CelestialObjectController`'s `GetByName` action did not return an `Ok` with the `CelestialObject`s that had a matching `Name` when one was found.");
            Assert.True((okResults.Value as IEnumerable<object>) != null && okResults.Value.GetType().ToString().Contains("StarChart.Models.CelestialObject"), "`CelestialObjectController`'s `GetByName` action returned an `Ok` with the list of `CelestialObject`s.");
            Assert.True((model.GetProperty("Name").GetValue((okResults.Value as IEnumerable<object>).First()) as string) == "Sun", "`CelestialObjectController`'s `GetByName` returned an `Ok` with a list of `CelestialObject`s, but those object's `Name` didn't match the `name` parameter..");
            Assert.True((okResults.Value as IEnumerable<object>) != null, "`CelestialObjectController`'s `GetByName` action did not set each `CelestialObject`'s `Satellites` property.");
            Assert.True((model.GetProperty("Satellites")?.GetValue((okResults.Value as IEnumerable<object>)?.First()) as IEnumerable<object>)?.Count() == 1, "`CelestialObjectController`'s `GetByName` action did not set each `CelestialObject`'s `Satellites` property.");
        }

        [Fact(DisplayName = "Create GetAll Action @create-getall-action")]
        public void CreateGetAllActionTest()
        {
            var filePath = ".." + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + "StarChart" + Path.DirectorySeparatorChar + "Controllers" + Path.DirectorySeparatorChar + "CelestialObjectController.cs";
            Assert.True(File.Exists(filePath), "`CelestialObjectController.cs` was not found in the `Controllers` directory.");

            var controller = TestHelpers.GetUserType("StarChart.Controllers.CelestialObjectController");
            Assert.True(controller != null, "A `public` class `CelestialObjectController` was not found in the `StarChart.Controllers` namespace.");

            var model = TestHelpers.GetUserType("StarChart.Models.CelestialObject");

            var item = Activator.CreateInstance(model);
            model.GetProperty("Id").SetValue(item, 1);
            model.GetProperty("Name").SetValue(item, "Sun");
            var item2 = Activator.CreateInstance(model);
            model.GetProperty("Id").SetValue(item2, 2);
            model.GetProperty("Name").SetValue(item2, "Earth");
            model.GetProperty("OrbitedObjectId").SetValue(item2, 1);

            var optionsBuilder = new DbContextOptionsBuilder();
            optionsBuilder.UseInMemoryDatabase("Test3");
            var context = new ApplicationDbContext(optionsBuilder.Options);

            var celestialController = Activator.CreateInstance(controller, new object[] { context });

            context.Add(item);
            context.Add(item2);
            context.SaveChanges();

            var method = controller.GetMethod("GetAll", new Type[] { });
            Assert.True(method != null, "`CelestialObjectController` does not contain a `GetAll` action with no parameters.");
            Assert.True(method.ReturnType == typeof(IActionResult), "`CelestialObjectController`'s `GetAll` action was found, but does not have a return type of `IActionResult`.");
            var getAttribute = method.GetCustomAttributes(typeof(HttpGetAttribute), false).FirstOrDefault() as HttpGetAttribute;
            Assert.True(getAttribute != null, "`CelestialObjectController`'s `GetAll` action was found, but does not have an `HttpGet` attribute.");
            var okResults = method.Invoke(celestialController, new object[] { }) as OkObjectResult;
            Assert.True(okResults != null && okResults.Value != null, "`CelestialObjectController`'s `GetAll` action did not return an `Ok` with all `CelestialObject`s.");
            Assert.True((okResults.Value as IEnumerable<object>) != null && okResults.Value.GetType().ToString().Contains("StarChart.Models.CelestialObject"), "`CelestialObjectController`'s `GetAll` action did not return an `Ok` with all `CelestialObject`s.");
            Assert.True((model.GetProperty("Satellites")?.GetValue((okResults.Value as IEnumerable<object>)?.First()) as IEnumerable<object>)?.Count() == 1, "`CelestialObjectController`'s `GetAll` action did not set each `CelestialObject`'s `Satellites` property.");
        }
    }
}
