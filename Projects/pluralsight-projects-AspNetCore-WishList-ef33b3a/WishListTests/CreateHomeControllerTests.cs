using System;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace WishListTests
{
    public class CreateHomeControllerTests
    {
        [Fact(DisplayName = "Create the HomeController @create-the-homecontroller")]
        public void CreateHomeControllerTest()
        {
            // Get appropriate path to file for the current operating system
            var filePath = ".." + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + "WishList" + Path.DirectorySeparatorChar + "Controllers" + Path.DirectorySeparatorChar + "HomeController.cs";
            // Assert Index.cshtml is in the Views/Home folder
            Assert.True(File.Exists(filePath), "`HomeController.cs` was not found in the `Controllers` folder.");

            var controllerType = TestHelpers.GetUserType("WishList.Controllers.HomeController");

            Assert.True(controllerType != null, "`HomeController.cs` was found, but does not appear to contain a `public` `HomeController` class.");
            Assert.True(controllerType.BaseType == typeof(Controller), "`HomeController` was found, but is not inheriting the `Controller` class. (you will need a using directive for `Microsoft.AspNetCore.Mvc`)");
        }

        [Fact(DisplayName = "Create the HomeController's Index Action @create-homecontrollers-index-action")]
        public void CreateHomeControllersIndexActionTest()
        {
            // Get appropriate path to file for the current operating system
            var filePath = ".." + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + "WishList" + Path.DirectorySeparatorChar + "Controllers" + Path.DirectorySeparatorChar + "HomeController.cs";
            // Assert Index.cshtml is in the Views/Home folder
            Assert.True(File.Exists(filePath), "`HomeController.cs` was not found in the `Controllers` folder.");

            var controllerType = TestHelpers.GetUserType("WishList.Controllers.HomeController");

            Assert.True(controllerType != null, "`HomeController.cs` was found, but does not appear to contain a `public` `HomeController` class.");
            Assert.True(controllerType.BaseType == typeof(Controller), "`HomeController` was found, but is not inheriting the `Controller` class. (you will need a using directive for `Microsoft.AspNetCore.Mvc`)");

            var controller = Activator.CreateInstance(controllerType);
            var method = controllerType.GetMethod("Index");
            Assert.True(method != null, "`HomeController` was found, but does not appear to contain a `public` `Index` action.");
            Assert.True(method.ReturnType == typeof(IActionResult), "`HomeController.Index` was found, but did not have a return type of `IActionResult`.");

            var result = (ViewResult)method.Invoke(controller, null);
            Assert.True(result.ViewName == "Index", "`HomeController.Index` did not explicitly return the `Index` view.");
        }

        [Fact(DisplayName = "Create the HomeController's Error Action @create-homecontrollers-error-action")]
        public void CreateHomeControllersErrorActionTest()
        {
            // Get appropriate path to file for the current operating system
            var filePath = ".." + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + "WishList" + Path.DirectorySeparatorChar + "Controllers" + Path.DirectorySeparatorChar + "HomeController.cs";
            // Assert Index.cshtml is in the Views/Home folder
            Assert.True(File.Exists(filePath), "`HomeController.cs` was not found in the `Controllers` folder.");

            var controllerType = TestHelpers.GetUserType("WishList.Controllers.HomeController");

            Assert.True(controllerType != null, "`HomeController.cs` was found, but does not appear to contain a `public` `HomeController` class.");
            Assert.True(controllerType.BaseType == typeof(Controller), "`HomeController` was found, but is not inheriting the `Controller` class. (you will need a using directive for `Microsoft.AspNetCore.Mvc`)");

            var controller = Activator.CreateInstance(controllerType);
            var method = controllerType.GetMethod("Error");
            Assert.True(method != null, "`HomeController` was found, but does not appear to contain a `public` `Error` action.");
            Assert.True(method.ReturnType == typeof(IActionResult), "`HomeController.Error` was found, but did not have a return type of `IActionResult`.");

            var result = (ViewResult)method.Invoke(controller, null);
            Assert.True(result.ViewName == "Error", "`HomeController.Error` did not explicitly return the `Error` view.");
        }
    }
}
