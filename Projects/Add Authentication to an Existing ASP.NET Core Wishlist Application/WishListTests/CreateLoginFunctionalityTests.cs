using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Moq;
using WishList.Models;
using Xunit;

namespace WishListTests
{
    public class CreateLoginFunctionalityTests
    {
        [Fact(DisplayName = "Create LoginViewModel @create-loginviewmodel")]
        public void CreateLoginModel()
        {
            var filePath = ".." + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + "WishList" + Path.DirectorySeparatorChar + "Models" + Path.DirectorySeparatorChar + "AccountViewModels" + Path.DirectorySeparatorChar + "LoginViewModel.cs";
            Assert.True(File.Exists(filePath), @"`LoginViewModel.cs` was not found in the `Models\AccountViewModels` folder.");

            var loginViewModel = TestHelpers.GetUserType("WishList.Models.AccountViewModels.LoginViewModel");
            Assert.True(loginViewModel != null, "A `public` class `LoginViewModel` was not found in the `WishList.Models.AccountViewModels` namespace.");

            var emailProperty = loginViewModel.GetProperty("Email");
            Assert.True(emailProperty != null, "`LoginViewModel` does not appear to contain a `public` `string` property `Email`.");
            Assert.True(emailProperty.PropertyType == typeof(string), "`LoginViewModel` has a property `Email` but it is not of type `string`.");
            Assert.True(Attribute.IsDefined(emailProperty, typeof(RequiredAttribute)), "`LoginViewModel` has a property `Email` but it doesn't appear to have a `Required` attribute.");
            Assert.True(emailProperty.CustomAttributes.FirstOrDefault(e => e.AttributeType == typeof(EmailAddressAttribute)) != null, "`LoginViewModel` has a property `Email` but it doesn't appear to have an `EmailAddress` attribute.");

            var passwordProperty = loginViewModel.GetProperty("Password");
            Assert.True(passwordProperty != null, "`LoginViewModel` does not appear to contain a `public` `string` property `Password`.");
            Assert.True(passwordProperty.PropertyType == typeof(string), "`LoginViewModel` has a property `Password` but it is not of type `string`.");
            Assert.True(passwordProperty.CustomAttributes.FirstOrDefault(e => e.AttributeType == typeof(RequiredAttribute)) != null, "`LoginViewModel` has a property `Password` but it doesn't appear to have a `Required` attribute.");
            var dataType = passwordProperty.CustomAttributes.FirstOrDefault(e => e.AttributeType == typeof(DataTypeAttribute));
            Assert.True(dataType != null, "`LoginViewModel` has a property `Password` but it doesn't appear to have a `DataType` attribute set to `Password`.");
            Assert.True(dataType.ConstructorArguments.Count == 1 && (int)dataType.ConstructorArguments[0].Value == 11, "`LoginViewModel` has a property `Password` and it has a `DataType` attribute but it's not set to `DataType.Password`.");
        }

        [Fact(DisplayName = "Create Login View @create-login-view")]
        public void CreateLoginView()
        {
            var filePath = ".." + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + "WishList" + Path.DirectorySeparatorChar + "Views" + Path.DirectorySeparatorChar + "Account" + Path.DirectorySeparatorChar + "Login.cshtml";
            Assert.True(File.Exists(filePath), @"`Login.cshtml` was not found in the `Views\Account` folder.");

            string file;
            using (var streamReader = new StreamReader(filePath))
            {
                file = streamReader.ReadToEnd();
            }
            Assert.True(file.Contains("LoginViewModel"),"`Login.cshtml` was found, but does not appear to contain the provided view (copy and paste the login view from the associated task in the `readme.md` file)");
        }

        [Fact(DisplayName = "Create HttpGet Login Action @create-httpget-login-action")]
        public void CreateHttpGetLoginActionTest()
        {
            var filePath = ".." + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + "WishList" + Path.DirectorySeparatorChar + "Controllers" + Path.DirectorySeparatorChar + "AccountController.cs";
            Assert.True(File.Exists(filePath), @"`AccountController.cs` was not found in the `Controllers` folder.");

            var accountController = TestHelpers.GetUserType("WishList.Controllers.AccountController");
            Assert.True(accountController != null, "A `public` class `AccountController` was not found in the `WishList.Controllers` namespace.");
            var method = accountController.GetMethod("Login", new Type[] { });
            Assert.True(method != null, "`AccountController` did not contain a `public` `Login` method with no parameters");
            Assert.True(method.ReturnType == typeof(IActionResult), "`AccountController`'s Get `Login` method did not have a return type of `IActionResult`");
            Assert.True(method.CustomAttributes.FirstOrDefault(e => e.AttributeType == typeof(HttpGetAttribute)) != null, "`AccountController` did not contain a `Login` method with an `HttpGet` attribute");
            Assert.True(method.CustomAttributes.FirstOrDefault(e => e.AttributeType == typeof(AllowAnonymousAttribute)) != null, "`AccountController`'s Get `Login` method did not have the `AllowAnonymous` attribute");

            var userStore = new Mock<IUserStore<ApplicationUser>>();
            var contextAccessor = new Mock<IHttpContextAccessor>();
            var claimsFactory = new Mock<IUserClaimsPrincipalFactory<ApplicationUser>>();
            var userManager = new UserManager<ApplicationUser>(userStore.Object, null, null, null, null, null, null, null, null);
            var signInManager = new SignInManager<ApplicationUser>(userManager, contextAccessor.Object, claimsFactory.Object, null, null, null);
            var controller = Activator.CreateInstance(accountController, new object[] { userManager, signInManager });
            var results = method.Invoke(controller, null) as ViewResult;
            Assert.True(results != null, "`AccountController`'s HttpGet `Login` action did not return a the `Login` view.");
            Assert.True(results.ViewName == "Login" || results.ViewName == null, "`AccountController`'s HttpGet `Login` action did not return a the `Login` view.");
        }

        [Fact(DisplayName = "Create HttpPost Login Action @create-httppost-login-action")]
        public void CreateHttpPostLoginActionTest()
        {
            var filePath = ".." + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + "WishList" + Path.DirectorySeparatorChar + "Controllers" + Path.DirectorySeparatorChar + "AccountController.cs";
            Assert.True(File.Exists(filePath), @"`AccountController.cs` was not found in the `Controllers` folder.");

            var accountController = TestHelpers.GetUserType("WishList.Controllers.AccountController");
            Assert.True(accountController != null, "A `public` class `AccountController` was not found in the `WishList.Controllers` namespace.");

            var loginViewModel = TestHelpers.GetUserType("WishList.Models.AccountViewModels.LoginViewModel");
            Assert.True(loginViewModel != null, "A `public` class `LoginViewModel` was not found in the `WishList.Models.AccountViewModels` namespace.");

            var method = accountController.GetMethod("Login", new Type[] { loginViewModel });
            Assert.True(method != null, "`AccountController` did not contain a `Login` method with a parameter of type `LoginViewModel`.");
            Assert.True(method.ReturnType == typeof(IActionResult), "`AccountController`'s `Login` method did not have a return type of `IActionResult`.");
            Assert.True(method.CustomAttributes.FirstOrDefault(e => e.AttributeType == typeof(HttpPostAttribute)) != null, "`AccountController``s `Login` method did not have the `HttpPost` attribute.");
            Assert.True(method.CustomAttributes.FirstOrDefault(e => e.AttributeType == typeof(AllowAnonymousAttribute)) != null, "`AccountController`'s `Login` method did not have the `AllowAnonymous` attribute.");
            Assert.True(method.CustomAttributes.FirstOrDefault(e => e.AttributeType == typeof(ValidateAntiForgeryTokenAttribute)) != null, "`AccountController`'s `Login` method did not have the `ValidateAntiForgeryToken` attribute.");
            
            var userStore = new Mock<IUserPasswordStore<ApplicationUser>>();
            var contextAccessor = new Mock<IHttpContextAccessor>();
            var claimsFactory = new Mock<IUserClaimsPrincipalFactory<ApplicationUser>>();
            var userManager = new Mock<UserManager<ApplicationUser>>(userStore.Object, null, null, null, null, null, null, null, null);
            var signInManager = new Mock<SignInManager<ApplicationUser>>(userManager.Object, contextAccessor.Object, claimsFactory.Object, null, null, null);
            signInManager.Setup(e => e.PasswordSignInAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>())).ReturnsAsync(Microsoft.AspNetCore.Identity.SignInResult.Failed);
            signInManager.Setup(e => e.PasswordSignInAsync(It.IsAny<string>(), "failure", It.IsAny<bool>(), It.IsAny<bool>())).ReturnsAsync(Microsoft.AspNetCore.Identity.SignInResult.Failed).Verifiable();
            signInManager.Setup(e => e.PasswordSignInAsync(It.IsAny<string>(), "success", It.IsAny<bool>(), It.IsAny<bool>())).ReturnsAsync(Microsoft.AspNetCore.Identity.SignInResult.Success);
            var controller = Activator.CreateInstance(accountController, new object[] { userManager.Object, signInManager.Object });
            var model = Activator.CreateInstance(loginViewModel, null);
            loginViewModel.GetProperty("Email").SetValue(model, "Valid@Email.com");
            loginViewModel.GetProperty("Password").SetValue(model, "success");
            var goodResults = method.Invoke(controller, new object[] { model }) as RedirectToActionResult;
            Assert.True(goodResults != null && goodResults.ControllerName == "Item" && goodResults.ActionName == "Index", "`AccountController`'s `Login` method did not return a `RedirectToAction` to the `Item.Index` action when login was successful.");
        }

        [Fact(DisplayName = "Check ModelState In HttpPost Login Action @check-modelstate-in-httppost-login-action")]
        public void CheckModelInHttpPostLoginActionTest()
        {
            var filePath = ".." + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + "WishList" + Path.DirectorySeparatorChar + "Controllers" + Path.DirectorySeparatorChar + "AccountController.cs";
            Assert.True(File.Exists(filePath), @"`AccountController.cs` was not found in the `Controllers` folder.");

            var accountController = TestHelpers.GetUserType("WishList.Controllers.AccountController");
            Assert.True(accountController != null, "A `public` class `AccountController` was not found in the `WishList.Controllers` namespace.");

            var loginViewModel = TestHelpers.GetUserType("WishList.Models.AccountViewModels.LoginViewModel");
            Assert.True(loginViewModel != null, "A `public` class `LoginViewModel` was not found in the `WishList.Models.AccountViewModels` namespace.");

            var method = accountController.GetMethod("Login", new Type[] { loginViewModel });
            Assert.True(method != null, "`AccountController` did not contain a `Login` method with a parameter of type `LoginViewModel`.");
            Assert.True(method.ReturnType == typeof(IActionResult), "`AccountController`'s `Login` method did not have a return type of `IActionResult`.");
            Assert.True(method.CustomAttributes.FirstOrDefault(e => e.AttributeType == typeof(HttpPostAttribute)) != null, "`AccountController``s `Login` method did not have the `HttpPost` attribute.");
            Assert.True(method.CustomAttributes.FirstOrDefault(e => e.AttributeType == typeof(AllowAnonymousAttribute)) != null, "`AccountController`'s `Login` method did not have the `AllowAnonymous` attribute.");
            Assert.True(method.CustomAttributes.FirstOrDefault(e => e.AttributeType == typeof(ValidateAntiForgeryTokenAttribute)) != null, "`AccountController`'s `Login` method did not have the `ValidateAntiForgeryToken` attribute.");

            var userStore = new Mock<IUserPasswordStore<ApplicationUser>>();
            var contextAccessor = new Mock<IHttpContextAccessor>();
            var claimsFactory = new Mock<IUserClaimsPrincipalFactory<ApplicationUser>>();
            var userManager = new Mock<UserManager<ApplicationUser>>(userStore.Object, null, null, null, null, null, null, null, null);
            var signInManager = new Mock<SignInManager<ApplicationUser>>(userManager.Object, contextAccessor.Object, claimsFactory.Object, null, null, null);
            signInManager.Setup(e => e.PasswordSignInAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>())).ReturnsAsync(Microsoft.AspNetCore.Identity.SignInResult.Failed);
            signInManager.Setup(e => e.PasswordSignInAsync(It.IsAny<string>(), "failure", It.IsAny<bool>(), It.IsAny<bool>())).ReturnsAsync(Microsoft.AspNetCore.Identity.SignInResult.Failed).Verifiable();
            signInManager.Setup(e => e.PasswordSignInAsync(It.IsAny<string>(), "success", It.IsAny<bool>(), It.IsAny<bool>())).ReturnsAsync(Microsoft.AspNetCore.Identity.SignInResult.Success);
            var controller = Activator.CreateInstance(accountController, new object[] { userManager.Object, signInManager.Object });
            var model = Activator.CreateInstance(loginViewModel, null);
            loginViewModel.GetProperty("Email").SetValue(model, "Valid@Email.com");
            loginViewModel.GetProperty("Password").SetValue(model, "success");

            var modelState = accountController.GetProperty("ModelState").GetValue(controller);
            var addModelError = typeof(ModelStateDictionary).GetMethod("AddModelError", new Type[] { typeof(string), typeof(string) });
            addModelError.Invoke(modelState, new object[] { "Email", "The entered email is not a valid email address." });

            var invalidModelResults = method.Invoke(controller, new object[] { model }) as ViewResult;
            Assert.True(invalidModelResults != null && (invalidModelResults.ViewName == "Login" || invalidModelResults.ViewName == null), "`AccountController`'s Post `Login` method did not return the `Login` view when the `ModelState` was not valid.");
            Assert.True(invalidModelResults.Model == model, "`AccountController`'s Post `Login` method did not provide the invalid model when returning the `Register` view when the `ModelState` was not valid.");

            var clearModelState = typeof(ModelStateDictionary).GetMethod("Clear");
            clearModelState.Invoke(modelState, new object[] { });

            var goodResults = method.Invoke(controller, new object[] { model }) as RedirectToActionResult;
            Assert.True(goodResults != null && goodResults.ControllerName == "Item" && goodResults.ActionName == "Index", "`AccountController`'s `Login` method did not return a `RedirectToAction` to the `Item.Index` action when login was successful.");
        }

        [Fact(DisplayName = "Check PasswordSignInAsync Results In HttpPost Login Action @check-passwordsignin-results-in-httppost-login-action")]
        public void CheckPasswordSignInAsyncResultsInHttpPostLoginActionTest()
        {
            var filePath = ".." + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + "WishList" + Path.DirectorySeparatorChar + "Controllers" + Path.DirectorySeparatorChar + "AccountController.cs";
            Assert.True(File.Exists(filePath), @"`AccountController.cs` was not found in the `Controllers` folder.");

            var accountController = TestHelpers.GetUserType("WishList.Controllers.AccountController");
            Assert.True(accountController != null, "A `public` class `AccountController` was not found in the `WishList.Controllers` namespace.");

            var loginViewModel = TestHelpers.GetUserType("WishList.Models.AccountViewModels.LoginViewModel");
            Assert.True(loginViewModel != null, "A `public` class `LoginViewModel` was not found in the `WishList.Models.AccountViewModels` namespace.");

            var method = accountController.GetMethod("Login", new Type[] { loginViewModel });
            Assert.True(method != null, "`AccountController` did not contain a `Login` method with a parameter of type `LoginViewModel`.");
            Assert.True(method.ReturnType == typeof(IActionResult), "`AccountController`'s `Login` method did not have a return type of `IActionResult`.");
            Assert.True(method.CustomAttributes.FirstOrDefault(e => e.AttributeType == typeof(HttpPostAttribute)) != null, "`AccountController``s `Login` method did not have the `HttpPost` attribute.");
            Assert.True(method.CustomAttributes.FirstOrDefault(e => e.AttributeType == typeof(AllowAnonymousAttribute)) != null, "`AccountController`'s `Login` method did not have the `AllowAnonymous` attribute.");
            Assert.True(method.CustomAttributes.FirstOrDefault(e => e.AttributeType == typeof(ValidateAntiForgeryTokenAttribute)) != null, "`AccountController`'s `Login` method did not have the `ValidateAntiForgeryToken` attribute.");

            var userStore = new Mock<IUserPasswordStore<ApplicationUser>>();
            var contextAccessor = new Mock<IHttpContextAccessor>();
            var claimsFactory = new Mock<IUserClaimsPrincipalFactory<ApplicationUser>>();
            var userManager = new Mock<UserManager<ApplicationUser>>(userStore.Object, null, null, null, null, null, null, null, null);
            var signInManager = new Mock<SignInManager<ApplicationUser>>(userManager.Object, contextAccessor.Object, claimsFactory.Object, null, null, null);
            signInManager.Setup(e => e.PasswordSignInAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>())).ReturnsAsync(Microsoft.AspNetCore.Identity.SignInResult.Failed);
            signInManager.Setup(e => e.PasswordSignInAsync(It.IsAny<string>(), "failure", It.IsAny<bool>(), It.IsAny<bool>())).ReturnsAsync(Microsoft.AspNetCore.Identity.SignInResult.Failed).Verifiable();
            signInManager.Setup(e => e.PasswordSignInAsync(It.IsAny<string>(), "success", It.IsAny<bool>(), It.IsAny<bool>())).ReturnsAsync(Microsoft.AspNetCore.Identity.SignInResult.Success);
            var controller = Activator.CreateInstance(accountController, new object[] { userManager.Object, signInManager.Object });
            var model = Activator.CreateInstance(loginViewModel, null);
            loginViewModel.GetProperty("Email").SetValue(model, "Valid@Email.com");
            loginViewModel.GetProperty("Password").SetValue(model, "failure");

            var modelState = accountController.GetProperty("ModelState").GetValue(controller);
            var addModelError = typeof(ModelStateDictionary).GetMethod("AddModelError", new Type[] { typeof(string), typeof(string) });
            addModelError.Invoke(modelState, new object[] { "Email", "The entered email is not a valid email address." });

            var invalidModelResults = method.Invoke(controller, new object[] { model }) as ViewResult;
            Assert.True(invalidModelResults != null && (invalidModelResults.ViewName == "Login" || invalidModelResults.ViewName == null), "`AccountController`'s Post `Login` method did not return the `Login` view when the `ModelState` was not valid.");
            Assert.True(invalidModelResults.Model == model, "`AccountController`'s Post `Login` method did not provide the invalid model when returning the `Register` view when the `ModelState` was not valid.");

            var clearModelState = typeof(ModelStateDictionary).GetMethod("Clear");
            clearModelState.Invoke(modelState, new object[] { });

            var badResults = method.Invoke(controller, new object[] { model }) as ViewResult;
            try
            {
                signInManager.Verify();
            }
            catch (MockException)
            {
                Assert.True(false, "`AccountController`'s Post `Login` action did not attempt to login in the user using `PasswordSignInAsync(string,string,bool,bool)` (Note: for this project do not use the `PasswordSignInAsync(ApplicationUser,string,bool,bool)` signature).");
            }
            Assert.True(badResults != null && (badResults.ViewName == "Login" || badResults.ViewName == null), "`AccountController`'s `Login` method did not return the `Login` view when the login failed.");
            Assert.True(badResults.Model == model, "`AccountController`'s `Login` method did not provide the invalid model when returning the `Login` view when login failed.");

            clearModelState.Invoke(modelState, new object[] { });

            loginViewModel.GetProperty("Password").SetValue(model, "success");
            var goodResults = method.Invoke(controller, new object[] { model }) as RedirectToActionResult;
            Assert.True(goodResults != null && goodResults.ControllerName == "Item" && goodResults.ActionName == "Index", "`AccountController`'s `Login` method did not return a `RedirectToAction` to the `Item.Index` action when login was successful.");
        }

        [Fact(DisplayName = "Create Logout Action @create-logout-action")]
        public void CreateHttpPostLogoutActionTest()
        {
            var filePath = ".." + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + "WishList" + Path.DirectorySeparatorChar + "Controllers" + Path.DirectorySeparatorChar + "AccountController.cs";
            Assert.True(File.Exists(filePath), @"`AccountController.cs` was not found in the `Controllers` folder.");

            var accountController = TestHelpers.GetUserType("WishList.Controllers.AccountController");
            Assert.True(accountController != null, "A `public` class `AccountController` was not found in the `WishList.Controllers` namespace.");

            var method = accountController.GetMethod("Logout", new Type[] { });
            Assert.True(method != null, "`AccountController` did not contain a `Logout` method.");
            Assert.True(method.ReturnType == typeof(IActionResult), "`AccountController`'s Post `Logout` method did not have a return type of `IActionResult`");
            Assert.True(method.CustomAttributes.FirstOrDefault(e => e.AttributeType == typeof(HttpPostAttribute)) != null, "`AccountController``s `Logout` method did not have the `HttpPost` attribute.");
            Assert.True(method.CustomAttributes.FirstOrDefault(e => e.AttributeType == typeof(ValidateAntiForgeryTokenAttribute)) != null, "`AccountController`'s `Logout` method did not have the `ValidateAntiForgeryToken` attribute.");

            var userStore = new Mock<IUserPasswordStore<ApplicationUser>>();
            var contextAccessor = new Mock<IHttpContextAccessor>();
            var claimsFactory = new Mock<IUserClaimsPrincipalFactory<ApplicationUser>>();
            var userManager = new Mock<UserManager<ApplicationUser>>(userStore.Object, null, null, null, null, null, null, null, null);
            var signInManager = new Mock<SignInManager<ApplicationUser>>(userManager.Object, contextAccessor.Object, claimsFactory.Object, null, null, null);
            signInManager.Setup(e => e.SignOutAsync()).Verifiable();
            var controller = Activator.CreateInstance(accountController, new object[] { userManager.Object, signInManager.Object });
            var results = method.Invoke(controller, new object[] { }) as RedirectToActionResult;
            try
            {
                signInManager.Verify();
            }
            catch (MockException)
            {
                Assert.True(false, "`AccountController`'s Post `Logout` action did not attempt to login out the user using `SignOutAsync`.");
            }
            Assert.True(results != null && results.ControllerName == "Home" && results.ActionName == "Index", "`AccountController`'s `Logout` method did not return a `RedirectToAction` to the `Home.Index` action.");
        }
    }
}
