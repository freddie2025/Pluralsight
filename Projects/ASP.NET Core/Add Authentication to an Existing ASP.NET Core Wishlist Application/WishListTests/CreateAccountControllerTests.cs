using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WishList.Models;
using Xunit;

namespace WishListTests
{
    public class CreateAccountControllerTests
    {
        [Fact(DisplayName = "Create AccountController @create-accountcontroller")]
        public void CreateAccountControllerTest()
        {
            var filePath = ".." + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + "WishList" + Path.DirectorySeparatorChar + "Controllers" + Path.DirectorySeparatorChar + "AccountController.cs";
            Assert.True(File.Exists(filePath), "`AccountController.cs` was not found in the `Controllers` folder.");

            var accountController = TestHelpers.GetUserType("WishList.Controllers.AccountController");

            Assert.True(accountController != null, "A `public` class `AccountController` was not found in the `WishList.Controllers` namespace.");
            Assert.True(accountController.BaseType == typeof(Controller), "`AccountController` didn't inherit the `Controller` class.");
            Assert.True(accountController.CustomAttributes.Any(e => e.AttributeType == typeof(AuthorizeAttribute)), "`AccountController` didn't have an `Authorize` attribute.");
        }

        [Fact(DisplayName = "Create Private Fields For AccountController @create-private-fields-for-accountcontroller")]
        public void CreatePrivateFieldsTest()
        {
            var filePath = ".." + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + "WishList" + Path.DirectorySeparatorChar + "Controllers" + Path.DirectorySeparatorChar + "AccountController.cs";
            Assert.True(File.Exists(filePath), "`AccountController.cs` was not found in the `Controllers` folder.");

            var accountController = TestHelpers.GetUserType("WishList.Controllers.AccountController");
            Assert.True(accountController != null, "A `public` class `AccountController` was not found in the `WishList.Controllers` namespace.");

            var userManager = accountController.GetField("_userManager", BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.True(userManager != null, "`AccountController` does not appear to contain a `private` `readonly` field `_userManager` of type `UserManager` with a type argument of `ApplicationUser`.");
            Assert.True(userManager.FieldType == typeof(UserManager<ApplicationUser>), "`AccountController` has a `_userManager` field but it is not of type `UserManager` with a type argument of `ApplicationUser`.");
            Assert.True(userManager.IsInitOnly, "`AccountController` has a `_userManager` field but it is not `readonly`.");

            var signInManager = accountController.GetField("_signInManager", BindingFlags.NonPublic | BindingFlags.Instance);
            Assert.True(signInManager != null, "`AccountController` does not appear to contain a `private` `readonly` field `_signInManager` of type `SignInManager` with a type argument of `ApplicationUser`.");
            Assert.True(signInManager.FieldType == typeof(SignInManager<ApplicationUser>), "`AccountController` has a `_signInManager` field but it is not of type `SignInManager` with a type argument of `ApplicationUser`.");
            Assert.True(signInManager.IsInitOnly, "`AccountController` has a `_signInManager` field but it is not `readonly`.");
        }

        [Fact(DisplayName = "Create Constructor For AccountController @create-constructor-for-accountcontroller")]
        public void CreateConstructorForAccountControllerTest()
        {
            var filePath = ".." + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + "WishList" + Path.DirectorySeparatorChar + "Controllers" + Path.DirectorySeparatorChar + "AccountController.cs";
            Assert.True(File.Exists(filePath), "`AccountController.cs` was not found in the `Controllers` folder.");

            var accountController = TestHelpers.GetUserType("WishList.Controllers.AccountController");
            Assert.True(accountController != null, "A `public` class `AccountController` was not found in the `WishList.Controllers` namespace.");

            var constructor = accountController.GetConstructors().FirstOrDefault();
            var parameters = constructor.GetParameters();
            Assert.True((parameters.Count() == 2 && parameters[0]?.ParameterType == typeof(UserManager<ApplicationUser>) && parameters[1]?.ParameterType == typeof(SignInManager<ApplicationUser>)), "`AccountController` did not contain a constructor with two parameters, first of type `UserManager<ApplicationUser>`, second of type `SignInManager<ApplicationUser>`.");

            var userStore = new Mock<IUserStore<ApplicationUser>>();
            var contextAccessor = new Mock<IHttpContextAccessor>();
            var claimsFactory = new Mock<IUserClaimsPrincipalFactory<ApplicationUser>>();
            var userManager = new UserManager<ApplicationUser>(userStore.Object, null, null, null, null, null, null, null, null);
            var signInManager = new SignInManager<ApplicationUser>(userManager, contextAccessor.Object, claimsFactory.Object, null, null, null);
            var controller = Activator.CreateInstance(accountController, new object[] { userManager, signInManager });
            Assert.True(accountController.GetField("_userManager", BindingFlags.NonPublic | BindingFlags.Instance)?.GetValue(controller) == userManager, "`AccountController`'s constructor did not set the `_userManager` field based on the provided `UserManager` parameter.");
            Assert.True(accountController.GetField("_signInManager", BindingFlags.NonPublic | BindingFlags.Instance)?.GetValue(controller) == signInManager, "`AccountController``s constructor did not set the `_signInManager` field based on the provided `SignInManager` parameter.");
        }
    }
}
