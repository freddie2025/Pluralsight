using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using WishList.Controllers;
using WishList.Data;
using WishList.Models;
using Xunit;

namespace WishListTests
{
    public class UpdateItemControllerTests
    {
        [Fact(DisplayName = "Update ItemController @update-itemcontroller")]
        public void UpdateItemControllerTest()
        {
            Assert.True(typeof(ItemController).CustomAttributes.Any(e => e.AttributeType == typeof(AuthorizeAttribute)), "`ItemController` didn't have an `Authorize` attribute.");
        }

        [Fact(DisplayName = "Add UserManager To ItemController @add-usermanager-to-itemcontroller")]
        public void AddUserManagerToItemControllerTest()
        {
            var userManager = typeof(ItemController).GetField("_userManager", BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.True(userManager != null, "`ItemController` does not appear to contain a `private` `readonly` field `_userManager` of type `UserManager` with a type argument of `ApplicationUser`.");
            Assert.True(userManager.FieldType == typeof(UserManager<ApplicationUser>), "`ItemController` has a `_userManager` field but it is not of type `UserManager` with a type argument of `ApplicationUser`.");
            Assert.True(userManager.IsInitOnly, "`ItemController` has a `_userManager` field but it is not `readonly`.");
        }

        [Fact(DisplayName = "Add Parameter to ItemController @add-parameter-to-itemcontroller")]
        public void AddParameterToItemControllerTest()
        {
            var constructor = typeof(ItemController).GetConstructors().FirstOrDefault();
            var parameters = constructor.GetParameters();
            Assert.True((parameters.Count() == 2 && parameters[0]?.ParameterType == typeof(ApplicationDbContext) && parameters[1]?.ParameterType == typeof(UserManager<ApplicationUser>)), "`ItemController` did not contain a constructor with two parameters, first of type `ApplicationDbContext`, second of type `UserManager` with a type argument of `ApplicationUser`.");

            var userStore = new Mock<IUserStore<ApplicationUser>>();
            var userManager = new UserManager<ApplicationUser>(userStore.Object, null, null, null, null, null, null, null, null);
            var optionsBuilder = new DbContextOptionsBuilder();
            var applicationDbContext = new ApplicationDbContext(optionsBuilder.Options);
            var controller = Activator.CreateInstance(typeof(ItemController), new object[] { applicationDbContext, userManager }) as ItemController;
            Assert.True(typeof(ItemController).GetField("_userManager", BindingFlags.NonPublic | BindingFlags.Instance)?.GetValue(controller) == userManager, "`ItemController`'s constructor did not set the `_userManager` field based on the provided `UserManager` parameter.");
        }

        [Fact(DisplayName = "Update Index Action @update-index-action")]
        public void UpdateIndexActionTest()
        {
            var method = typeof(ItemController).GetMethod("Index", new Type[] { });
            Assert.True(method != null, "`ItemController` did not contain an `Index` method did you remove or rename it?");
            var constructor = typeof(ItemController).GetConstructors().FirstOrDefault();
            var parameters = constructor.GetParameters();
            Assert.True((parameters.Count() == 2 && parameters[0]?.ParameterType == typeof(ApplicationDbContext) && parameters[1]?.ParameterType == typeof(UserManager<ApplicationUser>)), "`ItemController` did not contain a constructor with two parameters, first of type `ApplicationDbContext`, second of type `UserManager` with a type argument of `ApplicationUser`.");

            var userStore = new Mock<IUserStore<ApplicationUser>>();
            var appuser = new ApplicationUser() { Email = "test@test.com" };
            var userManager = new Mock<UserManager<ApplicationUser>>(userStore.Object, null, null, null, null, null, null, null, null);
            userManager.Setup(e => e.GetUserAsync(It.IsAny<ClaimsPrincipal>())).ReturnsAsync(appuser).Verifiable();
            var optionsBuilder = new DbContextOptionsBuilder();
            optionsBuilder.UseInMemoryDatabase("Test");
            var applicationDbContext = new ApplicationDbContext(optionsBuilder.Options);
            var item = new Item() { Id = 20, Description = "Show" };
            typeof(Item).GetProperty("User").SetValue(item, appuser);
            applicationDbContext.Items.Add(new Item() { Id = 10, Description = "Do Not Show" });
            applicationDbContext.Items.Add(item);
            applicationDbContext.SaveChanges();
            var controller = Activator.CreateInstance(typeof(ItemController), new object[] { applicationDbContext, userManager.Object }) as ItemController;
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[] {
                new Claim(ClaimTypes.NameIdentifier, "1")
            }));
            controller.ControllerContext = new ControllerContext();
            controller.ControllerContext.HttpContext = new DefaultHttpContext() { User = user };

            var results = method.Invoke(controller, new object[] { }) as ViewResult;
            Assert.True(results != null && results.ViewName == "Index", "`ItemController`'s `Index` method did not return the `Index` view with a model of only items with the logged in User's Id.");
            Assert.True(results.Model != null, "`ItemController`'s `Index` method did return the `Index` view but without a model of only items with the logged in User's Id.");
            Assert.True(results.Model.GetType() == typeof(List<Item>), "`ItemController`'s `Index` method did return the `Index` view a model but the model was not of type `List` with a type argument of `Item`.");
            Assert.True(((List<Item>)results.Model).Count == 1, "`ItemController`'s `Index` method did return the `Index` view but without a model of only items with the logged in User's Id.");
            Assert.True(typeof(Item).GetProperty("User").GetValue(((List<Item>)results.Model)[0]) == appuser, "`ItemController`'s `Index` method did return the `Index` view but without a model of only items with the logged in User's Id.");
        }

        [Fact(DisplayName = "Update Create Action @update-create-action")]
        public void UpdateCreateActionTest()
        {
            var method = typeof(ItemController).GetMethod("Create", new Type[] { typeof(Item) });
            Assert.True(method != null, "`ItemController` did not contain a `Create` method did you remove or rename it?");
            var constructor = typeof(ItemController).GetConstructors().FirstOrDefault();
            var parameters = constructor.GetParameters();
            Assert.True((parameters.Count() == 2 && parameters[0]?.ParameterType == typeof(ApplicationDbContext) && parameters[1]?.ParameterType == typeof(UserManager<ApplicationUser>)), "`ItemController` did not contain a constructor with two parameters, first of type `ApplicationDbContext`, second of type `UserManager` with a type argument of `ApplicationUser`.");

            var userStore = new Mock<IUserStore<ApplicationUser>>();
            var appuser = new ApplicationUser() { Email = "test@test.com" };
            var userManager = new Mock<UserManager<ApplicationUser>>(userStore.Object, null, null, null, null, null, null, null, null);
            userManager.Setup(e => e.GetUserAsync(It.IsAny<ClaimsPrincipal>())).ReturnsAsync(appuser).Verifiable();
            var optionsBuilder = new DbContextOptionsBuilder();
            optionsBuilder.UseInMemoryDatabase("Test");
            var applicationDbContext = new ApplicationDbContext(optionsBuilder.Options);
            var controller = Activator.CreateInstance(typeof(ItemController), new object[] { applicationDbContext, userManager.Object }) as ItemController;
            controller.ControllerContext = new ControllerContext();
            controller.ControllerContext.HttpContext = new DefaultHttpContext();
            controller.ControllerContext.HttpContext.User = new ClaimsPrincipal();
            controller.ControllerContext.HttpContext.User.AddIdentity(new ClaimsIdentity());

            var results = method.Invoke(controller, new object[] { new Item() { Id = 1, Description = "Test" } }) as RedirectToActionResult;
            Assert.True(results != null, "`ItemController`'s `Create` method did not run successfully, please run locally and verify results.");
            // Verify this sets the item / user relationship
        }

        [Fact(DisplayName = "Update Delete Action @update-delete-action")]
        public void UpdateDeleteActionTest()
        {
            var filePath = ".." + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + "WishList" + Path.DirectorySeparatorChar + "Controllers" + Path.DirectorySeparatorChar + "ItemController.cs";
            Assert.True(File.Exists(filePath), @"`ItemController.cs` was not found in the `Controllers` folder.");

            var itemController = TestHelpers.GetUserType("WishList.Controllers.ItemController");
            Assert.True(itemController != null, "A `public` class `ItemController` was not found in the `WishList.Controllers` namespace.");

            var method = itemController.GetMethod("Delete", new Type[] { typeof(int) });
            Assert.True(method != null, "`ItemController` did not contain a `Delete` method did you remove or rename it?");
            var constructor = itemController.GetConstructors().FirstOrDefault();
            var parameters = constructor.GetParameters();
            Assert.True((parameters.Count() == 2 && parameters[0]?.ParameterType == typeof(ApplicationDbContext) && parameters[1]?.ParameterType == typeof(UserManager<ApplicationUser>)), "`ItemController` did not contain a constructor with two parameters, first of type `ApplicationDbContext`, second of type `UserManager` with a type argument of `ApplicationUser`.");

            var userStore = new Mock<IUserStore<ApplicationUser>>();
            var appuser = new ApplicationUser() { Email = "test@test.com" };
            var userManager = new Mock<UserManager<ApplicationUser>>(userStore.Object, null, null, null, null, null, null, null, null);
            userManager.Setup(e => e.GetUserAsync(It.IsAny<ClaimsPrincipal>())).ReturnsAsync(appuser).Verifiable();
            var optionsBuilder = new DbContextOptionsBuilder();
            optionsBuilder.UseInMemoryDatabase("Test");
            var applicationDbContext = new ApplicationDbContext(optionsBuilder.Options);
            var item = new Item() { Id = 99, Description = "Delete" };
            typeof(Item).GetProperty("User").SetValue(item, appuser);
            applicationDbContext.Items.Add(item);
            var item2 = new Item() { Id = 107, Description = "Don't Delete" };
            typeof(Item).GetProperty("User").SetValue(item2, new ApplicationUser() { Email = "bad@user.com" });
            applicationDbContext.Items.Add(item2);
            applicationDbContext.SaveChanges();

            var controller = Activator.CreateInstance(itemController, new object[] { applicationDbContext, userManager.Object }) as ItemController;
            controller.ControllerContext = new ControllerContext();
            controller.ControllerContext.HttpContext = new DefaultHttpContext();

            var results = method.Invoke(controller, new object[] { 99 }) as RedirectToActionResult;
            Assert.True(results != null, "`ItemController`'s `Delete` method did not run successfully, please run locally and verify results.");
            Assert.True(!applicationDbContext.Items.Any(e => e.Id == 99), "`ItemController`'s `Delete` method did not delete the `Item` with the matching `Id` when the correct user was logged in.");

            var unauthorizedResults = method.Invoke(controller, new object[] { 107 }) as UnauthorizedResult;
            Assert.True(unauthorizedResults != null, "`ItemController`'s `Delete` method did not return `Unauthorized` when the `Item`'s `User` did not match the logged in `User`.");
            Assert.True(applicationDbContext.Items.Any(e => e.Id == 107), "`ItemController`'s `Delete` method deleted the `Item` even though the logged in user wasn't the same as the `Item`'s `User`.");
        }
    }
}
