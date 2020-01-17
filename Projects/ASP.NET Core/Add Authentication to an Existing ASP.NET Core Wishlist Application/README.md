# ASP.NET Core WishList Application

The ASP.NET Core WishList Application is designed to allow users to create their own wishlists, and other users to mark that they are buying those items in such a way the owner of the wish list isn't able to see, while other users are able to see. This application is designed using the Model View Controller design pattern.

Note: This project is the second in a series of four projects, this project will cover taking an existing ASP.NET Core web application and changing it from only supporting one user to being able to support many users with authentication and basic security.

# Setup the Application

## If you want to use Visual Studio
If you want to use Visual Studio (highly recommended) follow the following steps:
-   If you already have Visual Studio installed make sure you have .Net Core installed by running the "Visual Studio Installer" and making sure ".NET Core cross-platform development" is checked
-   If you need to install visual studio download it at https://www.microsoft.com/net/download/ (If you're using Windows you'll want to check "ASP.NET" and ".NET Core cross-platform development" on the workloads screen during installation.)
-   Open the .sln file in visual studio
-   To run the application simply press the Start Debug button (green arrow) or press F5
-   If you're using Visual Studio on Windows, to run tests open the Test menu, click Run, then click on Run all tests (results will show up in the Test Explorer)
-   If you're using Visual Studio on macOS, to run tests, select the WishListTests Project, then go to the Run menu, then click on Run Unit Tests (results will show up in the Unit Tests panel)

(Note: All tests should fail at this point, this is by design. As you progress through the projects more and more tests will pass. All tests should pass upon completion of the project.)

## If you don't plan to use Visual studio
If you would rather use something other than Visual Studio
-   Install the .Net Core SDK from https://www.microsoft.com/net/download/core once that installation completes you're ready to roll!
-   To run the application go into the WishList project folder and type `dotnet run`
-   To run the tests go into the WishListTests project folder and type `dotnet test`

# Features you will implement

- Add support for user authentication
- Create functionality for creating and logging in
- Expand Wishlist functionality to support multiple users
- Implement several basic security practices (validation tokens, user verification, authentication, etc)

## Tasks necessary to complete implementation:

__Note:__ this isn't the only way to accomplish this, however; this is what the project's tests are expecting. Implementing this in a different way will likely result in being marked as incomplete / incorrect.

- [ ] Adding Authentication to our existing ASP.NET Core wishlist app
	- [ ] Configure Authentication
		- Note : We created the model `ApplicationUser` that inherits `IdentityUser` for you! (This was done to allow us to more accurately test your code through out this project)
		- [ ] Replace `ApplicationDbContext`'s inheritance of `DbContext` to `IdentityDbContext<ApplicationUser>` (you will need to add `using` directives for `Microsoft.AspNetCore.Identity.EntityFrameworkCore` and `using WishList.Models`)
		- [ ] In `Startup.cs`'s `ConfigureServices` method call `AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();` on `services` (you will need to add `using` directives for `Microsoft.AspNetCore.Identity` and `using WishList.Models`)
		- [ ] In `Startup.cs`'s `Configure` method Before `app.UseMvcWithDefaultRoute();` call `UseAuthentication` on `app`.
	- [ ] Create `AccountController`
		- [ ] Create new controller `AccountController` in the `Controllers` folder
			- The `AccountController` class should have the `Authorize` attribute (you will need a `using` directive for `Microsoft.AspNetCore.Authorization`)
			- The `AccountController` should inherit the `Controller` class (you will need a `using` directive for `Microsoft.AspNetCore.Mvc`)
		- [ ] Create private fields in the `AccountController` class
			- This should have a private readonly field of type `UserManager<ApplicationUser>` named `_userManager` (you will need a `using` directives for `WishList.Models` and `Microsoft.AspNetCore.Identity`)
			- This should have a private readonly field of type `SignInManager<ApplicationUser>` named `_signInManager`
		- [ ] Create a constructor in the `AccountController` class
			- This constructor should accept two parameters, the first of type `UserManager<ApplicationUser>`, the second of type `signInManager<ApplicationUser>`
			- This constructor should set each of the private readonly properties using the parameter of the same type
	- [ ] Create Register Functionality
		- [ ] Create RegisterViewModel class
			- Inside the `Models/AccountViewModels` folder create a new model `RegisterViewModel` (You will need to create the AccountViewModels folder)
			- Create a `String` Property Email
				- Email should have the Required attribute (you will need to add a `using` directive for `System.ComponentModel.DataAnnotations`)
				- Email should have the EmailAddress attribute
			- Create a String Property `Password`
				- `Password` should have the `Required` attribute
				- `Password` should have a `StringLength` attribute of 100 with a `MinimumLength` of 8 characters
				- `Password` should have the `DataType.Password` attribute
			- Create a `String` Property `ConfirmPassword`
				- `ConfirmPassword` should have the `DataType.Password` attribute
				- `ConfirmPassword` should have the `Compare` attribute with "Password"
		- [ ] Create the Register View
			- Inside the `Views/Account` folder add a new view `Register` (you will need to create the `Account` folder)
			- `Register.cshtml` should have a model of `RegisterViewModel` (You will need to include the namespace, `WishList.Models.AccountViewModels.RegisterViewModel`)
			- Add the following HTML to the view (we're providing this to save you from needing to type it all yourself)
				```
				<h3>Register New User</h3>
				<form>
					<div class="text-danger"></div>
					<div>
						<label></label>
						<input />
					</div>
					<div>
						<label></label>
						<input />
					</div>
					<div>
						<label></label>
						<input />
					</div>
					<div>
						<button type="submit">Register User</button>
					</div>
				</form>
				```
			- Add an attribute `asp-action` with a value of `"Register"` to the `form` tag
			- Add an attribute `asp-validation-summary` with a value of `"All"` for the `div` tag with an attribute `class` of value `"text-danger"`
			- Add an attribute `asp-for` with a value of `"Email"` to both the first `label` and `input` tag
			- Add an attribute `asp-for` with a value of `"Password"` to both the second `label` and `input` tag
			- Add an attribute `asp-for` with a value of `"ConfirmPassword"` to both the third `label` and `input` tag
		- [ ] Create an `HttpGet` action `Register` in the `AccountController` class
			- This action should have the `HttpGet` attribute
			- This action should have the `AllowAnonymous` attribute
			- This action should have no parameters
			- This action should return the `Register` view.
		- [ ] Create an `HttpPost` action `Register` in the `AccountController` class
			- This action should have the `HttpPost` attribute
			- This action should have the `AllowAnonymous` attribute
			- This action should accept a parameter of type `RegisterViewModel`  (you will need to add a `using` directive for `WishList.Models.AccountViewModels`) 
			- This action should return a `RedirectToAction` to `HomeController.Index`
		- [ ] Update the `HttpPost` `Register` action to check if the `ModelState` is valid
				- If not return the `Register` view with the model provided in the parameter as it's model
		- [ ] Update the `HttpPost` `Register` action to create the new user using the `_userManager.CreateAsync` method providing it a valid `ApplicationUser` (you will need to set the `User` and `Email` properties to the `Email` from the `RegisterViewModel`) and a `string`  which you'll set the the `Password` property from the `RegisterViewModel`
		- [ ] Check the `Result` property from the `HttpPost` `Register`'s the `CreateAsync` call if `Result.Success`
				- If `Result.Success` is `false` foreach error in `Result.Errors` use `ModelState.AddModelError` to add an error with a the first parameter of`"Password"` and second with the value of the error's `Description` property. Then return the `Register` view with the model provided by `Register`'s the parameter.
	- [ ] Create Login / Logout Functionality
		- [ ] Create `LoginViewModel` class in the `Models\AccountViewModels` folder
			- Create a `String` Property Email
				- Email should have the Required attribute
				- Email should have the EmailAddress attribute
			- Create a String Property `Password`
				- `Password` should have the `Required` attribute
				- `Password` should have the `DataType.Password` attribute
		- [ ] Create a `Login.cshtml` view in the `Views/Account` folder
			- Add the following HTML to the `Login` view
				```
				@model WishList.Models.AccountViewModels.LoginViewModel
				<h2>Log in</h2>
				<form asp-action="Login" method="post">
					<div asp-validation-summary="All" class="text-danger"></div>
					<div>
						<label asp-for="Email"></label>
						<input asp-for="Email" />
						<span asp-validation-for="Email"></span>
					</div>
					<div>
						<label asp-for="Password"></label>
						<input asp-for="Password" />
						<span asp-validation-for="Password"></span>
					</div>
					<div>
						<button type="submit">Log in</button>
					</div>
				</form>
				<a asp-action="Register">Register new account</a>
				```
		- [ ] Create an `HttpGet` action `Login` in the `AccountController`
			- This action should have the `HttpGet` attribute
			- This action should have the `AllowAnonymous` attribute
			- This action should have no parameters
			- This action should return the `Login` view.
		- [ ] Create an `HttpPost` action `Login` in the `AccountController`
			- This action should have the `HttpPost` attribute
			- This action should have the `AllowAnonymous` attribute
			- This action should have the `ValidateAntiForgeryToken` attribute
			- This action should have a return type of `IActionResult`
			- This action should accept a parameter of type `LoginViewModel`
			- This action should return a `RedirectToAction` to the `Home.Index` action.
		- [ ] Update `HttpPost` `Login` to check if the `ModelState` is valid
			- If not return the `Login` view with the model provided in the parameter as it's model
		- [ ] Update `HttpPost` `Login` to use `SignInManager`'s `PasswordSignInAsync` method with the `(string,string,bool,bool)` signature to attempt to login the user (Note: you will need to check the `Result` property to see the results, pass `false` for the 3rd and 4th parameters)
			- if the `SignInResult` returned by `PasswordSignInAsync`'s `Succeeded` property is `false` use `ModelState`'s `AddModelError` with a key of `string.Empty` and an `errorMessage` of `"Invalid login attempt."`
		- [ ] `Create` an `HttpPost` action `Logout` in the `AccountController`
			- This action should have the `HttpPost` attribute
			- This action should have the `ValidateAntiForgeryToken` attribute
			- This action should have a return type of `IActionResult`
			- This action should use `SignInManager`'s `SignOutAsync` method
			- This should return a `RedirectToAction` to the `Home.Index` action
	- [ ] Add Links to Index View
		- [ ] Add `using` directives for `Microsoft.AspNetCore.Identity` and `WishList.Models` to the top of `Index.cshtml`
		- [ ] Add an `inject` directive for `SignInManager<ApplicationUser>` with the name `SignInManager` after the `using` directives
		- [ ] Check if the user is signed in using the injected `SignInManager`'s `IsSignedIn` method (provide `User` as the arguement)
			- If `IsSignedIn` returns `true` provide the following HTML
				```
					<div>
						<form asp-action="Logout" asp-controller="Account" method="post">
							<button type="submit">Logout</button>
						</form>
					</div>
				```
			- If `IsSignedIn` returns `false` provide the following HTML
				```
					<div>
						<a asp-action="Register" asp-controller="Account" >Register</a>
					</div>
					<div>
						<a asp-action="Login" asp-controller="Account" >Log in</a>
					</div>
				```
	- [ ] Create Relationship Between Item and ApplicationUser Models
		- [ ] Add a `virtual` property of type `ApplicationUser` named `User` to the `Item` model
		- [ ] Add a `virtual` property of type `ICollection<Item>` named `Items` to the `ApplicationUser` model (you will need to add a `using` directive for `System.Collections.Generic`)
	- [ ] Update ItemController actions to consider user
		- [ ] Add the `Authorize` attribute to the `ItemController` class (you will need to add a `using` directive for `Microsoft.AspNetCore.Authorization`)
		- [ ] Add a new `private` `readonly` field of type `UserManager<ApplicationUser>` named `_userManager`
		- [ ] Update the `ItemController`'s constructor to accept a second parameter of type `UserManager<ApplicationUser>` and within the costructor set `_userManager` to the provided `UserManager<ApplicationUser>` paramater.
		- [ ] Update the `ItemController.Index` action to only return items with associated with the currently logged in user.
			- Change the `_context.Items.ToList();` to include a `Where` call that only gets items with the matching `User.Id`. (You can get the current logged in user using `UserManager.GetUserAsync(HttpContext.User)`)
		- [ ] Update the `HttpPost` `ItemController.Create` action to add the logged in User to the `Item`
			- Before adding the Item to set the UserId to the logged in user's Id (You can get the current logged in user using `UserManager.GetUserAsync(HttpContext.User)`)
		- [ ] Update the `ItemController.Delete` action to prevent deleting items by anyone but the user who owns that item.
			- Before removing the `Item` check that it is not `null` if it is `null` return `NotFound`
			- After checking if `Item` is null, but before removing the `Item` check te make sure the Item's User is the same as the logged in user, if not return `Unauthorized`
	
## What Now?

You've completed the tasks of this project, if you want to continue working on this project there will be additional projects added to the ASP.NET Core path that continue where this project left off adding more advanced views and models, as well as providing and consuming data as a web service.

Otherwise now is a good time to continue on the ASP.NET Core path to expand your understanding of the ASP.NET Core framework or take a look at the Microsoft Azure for Developers path as Azure is a common choice for hosting, scaling, and expanding the functionality of ASP.NET Core applications.
