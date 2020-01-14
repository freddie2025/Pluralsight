using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StarChart.Data;
using Xunit;

namespace StarChartTests
{
    public class CreateWriteModifyActionsTests
    {
        [Fact(DisplayName = "Create Create Action @create-create-action")]
        public void CreateCreateActionTest()
        {
            var filePath = ".." + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + "StarChart" + Path.DirectorySeparatorChar + "Controllers" + Path.DirectorySeparatorChar + "CelestialObjectController.cs";
            Assert.True(File.Exists(filePath), "`CelestialObjectController.cs` was not found in the `Controllers` directory.");

            var controller = TestHelpers.GetUserType("StarChart.Controllers.CelestialObjectController");
            Assert.True(controller != null, "A `public` class `CelestialObjectController` was not found in the `StarChart.Controllers` namespace.");

            var model = TestHelpers.GetUserType("StarChart.Models.CelestialObject");

            var item = Activator.CreateInstance(model);
            model.GetProperty("Id").SetValue(item, 1);
            model.GetProperty("Name").SetValue(item, "Sun");

            var optionsBuilder = new DbContextOptionsBuilder();
            optionsBuilder.UseInMemoryDatabase("TestCreate");
            var context = new ApplicationDbContext(optionsBuilder.Options);

            var celestialController = Activator.CreateInstance(controller, new object[] { context });

            var method = controller.GetMethod("Create");
            Assert.True(method != null, "`CelestialObjectController` does not contain a `Create` action that accepts a `CelestialObject` parameter.");
            Assert.True(method.ReturnType == typeof(IActionResult), "`CelestialObjectController`'s `Create` action was found, but does not have a return type of `IActionResult`.");
            var postAttribute = method.GetCustomAttributes(typeof(HttpPostAttribute), false).FirstOrDefault() as HttpPostAttribute;
            Assert.True(postAttribute != null, "`CelestialObjectController`'s `Create` action was found, but does not have an `HttpPost` attribute.");
            var okResults = method.Invoke(celestialController, new object[] { item }) as CreatedAtRouteResult;
            Assert.True(okResults != null, "`CelestialObjectController`'s `Create` action did not return a `CreatedAtRoute` with the new `CelestialObject`'s `Id` and the new `CelestialObject`.");
            Assert.True(okResults.RouteName == "GetById", @"`CelestialObjectController`'s `Create` action's `CreatedAtRoute`'s first argument was not `""GetById""`.");
            Assert.True(okResults.RouteValues.Count == 1 && okResults.RouteValues["id"] != null, "`CelestialObjectController`'s `Create` action's `CreateAtRoute`'s second argument didn't contain a RouteValue of `id`.");
            Assert.True(okResults.Value.GetType() == model, "`CelestialObjectController`'s `Create` action's `CreatedAtRoute`'s third argument didn't contain the newly created `CelestialObject`.");
            var results = context.Find(model, 1);
            Assert.True(model.GetProperty("Name").GetValue(results) == model.GetProperty("Name").GetValue(item), "`CelestialObjectController`'s `Create` action did not add the provided `CelestialObject` to `_context.CelestialObjects` (Don't forget to call `SaveChanges` after adding it!).");
        }

        [Fact(DisplayName = "Create Update Action @create-update-action")]
        public void CreateUpdateActionTest()
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
            var replacement = Activator.CreateInstance(model);
            model.GetProperty("Id").SetValue(replacement, 1);
            model.GetProperty("Name").SetValue(replacement, "Sol");

            var optionsBuilder = new DbContextOptionsBuilder();
            optionsBuilder.UseInMemoryDatabase("TestUpdate");
            var context = new ApplicationDbContext(optionsBuilder.Options);

            var celestialController = Activator.CreateInstance(controller, new object[] { context });

            context.Add(item);
            context.Add(item2);
            context.SaveChanges();

            var method = controller.GetMethod("Update");
            Assert.True(method != null, "`CelestialObjectController` does not contain a `Update` action that accepts an `int` and a `CelestialObject` parameter.");
            Assert.True(method.ReturnType == typeof(IActionResult), "`CelestialObjectController`'s `Update` action was found, but does not have a return type of `IActionResult`.");
            var putAttribute = method.GetCustomAttributes(typeof(HttpPutAttribute), false).FirstOrDefault() as HttpPutAttribute;
            Assert.True(putAttribute != null && putAttribute.Template == "{id}", "`CelestialObjectController`'s `Update` action was found, but does not have an `HttpPut` attribute with a template of `{id}`.");
            var notFoundResults = method.Invoke(celestialController, new object[] { 3, replacement }) as NotFoundResult;
            Assert.True(notFoundResults != null, "`CelestialObjectController`'s `Update` action did not return the `NotFound` when no `CelestialObject` with a matching `Id` was found.");
            var okResults = method.Invoke(celestialController, new object[] { 1, replacement }) as NoContentResult;
            Assert.True(okResults != null, "`CelestialObjectController`'s `Update` action did not return a `NoContent` result.");
            var results = context.Find(model, 1);
            Assert.True(model.GetProperty("Name").GetValue(results) == model.GetProperty("Name").GetValue(replacement), "`CelestialObjectController`'s `Update` action did not update the matching `CelestialObject` in `_context.CelestialObjects` with the `CelestialObject` provided in the parameter (Don't forget to call `SaveChanges` after updating it!).");
        }

        [Fact(DisplayName = "Create RenameObject Action @create-renameobject-action")]
        public void CreateUpdateNameActionTest()
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

            var optionsBuilder = new DbContextOptionsBuilder();
            optionsBuilder.UseInMemoryDatabase("TestUpdateName");
            var context = new ApplicationDbContext(optionsBuilder.Options);

            var celestialController = Activator.CreateInstance(controller, new object[] { context });

            context.Add(item);
            context.Add(item2);
            context.SaveChanges();

            var method = controller.GetMethod("RenameObject", new Type[] { typeof(int), typeof(string) });
            Assert.True(method != null, "`CelestialObjectController` does not contain a `RenameObject` action that accepts an `int` and a `string` parameter.");
            Assert.True(method.ReturnType == typeof(IActionResult), "`CelestialObjectController`'s `RenameObject` action was found, but does not have a return type of `IActionResult`.");
            var patchAttribute = method.GetCustomAttributes(typeof(HttpPatchAttribute), false).FirstOrDefault() as HttpPatchAttribute;
            Assert.True(patchAttribute != null && patchAttribute.Template == "{id}/{name}", "`CelestialObjectController`'s `RenameObject` action was found, but does not have an `HttpPatch` attribute with a template of `{id}/{name}`.");
            var notFoundResults = method.Invoke(celestialController, new object[] { 3, "Bob" }) as NotFoundResult;
            Assert.True(notFoundResults != null, "`CelestialObjectController`'s `RenameObject` action did not return the `NotFound` when no `CelestialObject` with a matching `Id` was found.");
            var okResults = method.Invoke(celestialController, new object[] { 1, "Sol" }) as NoContentResult;
            Assert.True(okResults != null, "`CelestialObjectController`'s `RenameObject` action did not return a `NoContent` result.");
            var results = context.Find(model, 1);
            Assert.True(model.GetProperty("Name").GetValue(results).ToString() == "Sol", "`CelestialObjectController`'s `RenameObject` action did not update the `Name` property of the matching `CelestialObject` in `_context.CelestialObjects` with the `string` provided in the parameter (Don't forget to call `SaveChanges` after updating it!).");
        }

        [Fact(DisplayName = "Create Delete Action @create-delete-action")]
        public void CreateDeleteActionTest()
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

            var optionsBuilder = new DbContextOptionsBuilder();
            optionsBuilder.UseInMemoryDatabase("TestDelete");
            var context = new ApplicationDbContext(optionsBuilder.Options);

            var celestialController = Activator.CreateInstance(controller, new object[] { context });

            context.Add(item);
            context.Add(item2);
            context.SaveChanges();

            var method = controller.GetMethod("Delete", new Type[] { typeof(int) });
            Assert.True(method != null, "`CelestialObjectController` does not contain a `Delete` action that accepts an `int` parameter.");
            Assert.True(method.ReturnType == typeof(IActionResult), "`CelestialObjectController`'s `Delete` action was found, but does not have a return type of `IActionResult`.");
            var deleteAttribute = method.GetCustomAttributes(typeof(HttpDeleteAttribute), false).FirstOrDefault() as HttpDeleteAttribute;
            Assert.True(deleteAttribute != null && deleteAttribute.Template == "{id}", "`CelestialObjectController`'s `Delete` action was found, but does not have an `HttpDelete` attribute with a template of `{id}`.");
            var notFoundResults = method.Invoke(celestialController, new object[] { 3 }) as NotFoundResult;
            Assert.True(notFoundResults != null, "`CelestialObjectController`'s `Delete` action did not return the `NotFound` when no `CelestialObject` with a matching `Id` was found.");
            var okResults = method.Invoke(celestialController, new object[] { 1 }) as NoContentResult;
            Assert.True(okResults != null, "`CelestialObjectController`'s `Delete` action did not return a `NoContent` result.");
            var results = context.Find(model, 1);
            Assert.True(results == null, "`CelestialObjectController`'s `Delete` action did not remove the matching `CelestialObject` in `_context.CelestialObjects` (Don't forget to call `SaveChanges` after updating it!).");
        }
    }
}
