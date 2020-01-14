# ASP.NET Core WishList Application

The ASP.NET Core WishList Application is designed to allow users to create their own wishlists, and other users to mark that they are buying those items in such a way the owner of the wish list isn't able to see, while other users are able to see. This application is designed using the Model View Controller design pattern.

Note: This project is the first in a series of four projects, this project will cover taking an empty ASP.NET Core web application, setting up it's middleware to support MVC and EntityFramework, then creating a simple single user wishlist application.

# Setup the Application

## If you want to use Visual Studio
If you want to use Visual Studio (highly recommended) follow the following steps:
-   If you already have Visual Studio installed make sure you have .Net Core installed by running the "Visual Studio Installer" and making sure ".NET Core cross-platform development" is checked
-   Open the .sln file in visual studio
-   If you need to install visual studio download it at https://www.microsoft.com/net/download/ (If you're using Windows you'll want to check "ASP.NET" and ".NET Core cross-platform development" on the workloads screen during installation.)
-   To run the application simply press the Start Debug button (green arrow) or press F5
-   If you're using Visual Studio on Windows, to run tests open the Test menu, click Run, then click on Run all tests (results will show up in the Test Explorer)
-   If you're using Visual Studio on macOS, to run tests, select the WishListTests Project, then go to the Run menu, then click on Run Unit Tests (results will show up in the Unit Tests panel)

(Note: All tests should fail! It's ok. This is supposed to happen. As you complete the project, more of the tests will pass. When you complete the project, **all** tests should pass)

## If you don't plan to use Visual studio
If you would rather use something other than Visual Studio
-   Install the .Net Core SDK from https://www.microsoft.com/net/download/core once that installation completes you're ready to roll!
-   To run the application go into the WishList project folder and type `dotnet run`
-   To run the tests go into the WishListTests project folder and type `dotnet test`

# Features you will implement

- Setup and configure middleware to support MVC
- Create the ability to view your wishlist
- Create the ability add items to your wish list
- Create the ability to remove items from your wishlist

## Tasks necessary to complete implementation:

__Note:__ this isn't the only way to accomplish this, however; this is what the project's tests are expecting. Implementing this in a different way will likely result in being marked as incomplete / incorrect.

- [ ] Creating ASP.NET Core Application from scratch
	- [ ] Add Middleware/Configuration to `Startup.cs`
		- [ ] In the `Startup.cs` file add support for the MVC middleware and configure it to have a default route.
			- [ ] In the `ConfigureServices` method call `AddMvc` on `services` to add support for MVC middleware.
			- [ ] In the `Configure` method remove the `app.Run` entirely and replace it with a call to `UseMvcWithDefaultRoute` on `app`.
		- [ ] In the `Startup.cs` file add support for developer exception pages and user friendly error pages.
			- [ ] In the `Configure` method before `UseMvcWithDefaultRoute` add a condition that checks if `env` is set to `Development` using `IsDevelopement`.
				- If Development it should call `UseDeveloperExceptionPage` on `app` to get better detailed error pages.
				- Otherwise, it should call `UseExceptionHandler` on `app` and pass it the argument "/Home/Error". Next, we'll create the generic Error page provided by this method.
	- [ ] Create Home Views and `HomeController`
		- [ ] Create Home Views
			- [ ] Create a Generic Welcome View
				- Create a new view `Index` in the `WishList/Views/Home` folder. (you will need to make some of these folders)
				- The `Index` View should contain an `h1` tag welcoming the user. (Note: you should remove any generated HTML from this new view and any other views you create)
			- [ ] Create a Generic Error View
				- Create a new view `Error` in the `WishList/Views/Shared` folder.
					- This view should contain a `p` tag saying "An error has occurred. Please try again."
		- [ ] Create the `HomeController`
			- [ ] Create a new Controller `HomeController` inside the `Controllers` folder (you might need to create this folder)
				- This should inherit the `Controller` class (you will need to add a `using` statement for the `Microsoft.AspNetCore.Mvc` namespace)
			- [ ] Create a new Action `Index` in the `HomeController`
				- This action should have a return type of `IActionResult`.
				- The return statement should return the `Index` view (specify the "Index" view in your return statement).
			- [ ] Create a new Action `Error` in the `HomeController`
				- This action should have a return type of `IActionResult`.
				- The return statement should return the `Error` view (specify the "Error" view in your return statement).
	- Note: The application is now viewable in your browser!
	- [ ] Create Item Model With EntityFramework Support
		- [ ] Add `EntityFramework` support
			- [ ] Create a class `ApplicationDbContext` that inherits the `DbContext` class in the "WishList/Data" folder. (you will need to make some of these folders) (_Note_ : `DbContext` exists in the `Microsoft.EntityFrameworkCore` namespace)
			- [ ] Add a Constructor that accepts a parameter of type `DbContextOptions options` and invokes the base constructor (you can do this by adding `: base(options)` after the method signature)
		- [ ] In the `Startup` class's `ConfigureServices` method add `EntityFramework` support.
			- [ ] Call `AddDbContext<ApplicationDbContext>` on `services` with the argument `options => options.UseInMemoryDatabase("WishList")` to point `EntityFramework` to the application's `DbContext`. (_Note_ : You will need to add a `using` statement for `WishList.Data`)
		- [ ] Create the `Item` model.
			- [ ] Create a new class `Item` in the "WishList/Models" folder (You might need to create this folder)
				- This class should contain a public property `Id` of type `int`.
				- This class should contain a public property `Description` of type `string`.
				- The `Description` property should have attributes of `Required` and `StringLength(50)`. (_Note_ : You'll need to add a `using` statement for `System.ComponentModel.DataAnnotations`.)
			- [ ] In the `ApplicationDbContext` class add new public property `Items` of type `DbSet<Item>`. (_Note_ : You'll need to add a `using` statement for `WishList.Models`.)
	- [ ] Create "Item" Views
		- [ ] Add support for Tag Helpers and Layout
			- [ ] Create a New View `_ViewImports` in the `WishList/Views` folder.
				- This view should contain `@addTagHelper *, Microsoft.AspNetCore.Mvc.TagHelpers`.
			- [ ] Create a New View `_ViewStart` in the `WishList/Views` folder.
				- This view should contain `@{ Layout = "_Layout"; }`. (_Note_ : We've provided a very basic layout for you, this layout contains some basic CSS and jQuery.)
		- [ ] Create a "Create" View
			- [ ] Create a new view "Create" in the "WishList/Views/Item" folder. (You will need to make some of these folders)
				- This view should use a model of `Item`. (You'll need to use the full `WishList.Models.Item` not just `Item`)
				- This view should contain an `h3` tag saying "Add item to wishlist".
				- This view should have a `form` tag containing the attribute `asp-action` set to "create".
				- Inside the `form` tag create an `input` tag with the attribute `asp-for` set to "Description".
				- Inside the `form` tag create a `span` tag with the attribute `asp-validation-for` set to "Description".
				- Inside the `form` tag create an `button` tag with the attribute `type` set to "submit" and text "Add Item".
		- [ ] Create the Item's "Index" View
			- [ ] Create a new View "Index" in the "WishList/Views/Item" folder
				- This view should use a model of `List<Item>`. (You'll need to use the full `WishList.Models.Item` not just `Item`)
				- This view should have an `h1` tag containing "Wishlist".
				- After the `h1` tag add an `a` tag with an attribute `asp-action` with a value of `create` with the text "Add item".
				- Create a `ul` tag that `ul` tag should contain a razor foreach loop that will iterate through each `item` of type `Item` in `Model`
				- Each iteration should contain an `li` tag that provides the `Item`'s `Description` property followed by an `a` tag.
				- The `a` tag should have the attributes `asp-action` set to "delete" and `asp-route-id` set to the `Item`'s `Id` property with the text of the `a` tag being "delete".
		- [ ] In Home's Index view add an `a` tag with attributes `asp-action` set to "Index" and `asp-controller` set to "Item" with text "View wishlist".
	- [ ] Create `ItemController` and it's Actions
		- [ ] Create a new Controller `ItemController` inside the `Controllers` folder that inherits the `Controller` class from `Microsoft.AspNetCore.Mvc`
			- [ ] Create a `private` `readonly` field `_context` of type `ApplicationDbContext`. (Do not instantiate it at this time) (_Note_ : You will need to add a `using` statement to `WishList.Data`.)
			- [ ] Create a new constructor that accepts a parameter of type `ApplicationDbContext`
				- This constructor should set `_context` to the provided `ApplicationDbContext` parameter.
			- [ ] Create a new Action `Index` in the `ItemController`.
				- This action should have a return type of `IActionResult`.
				- This action should return the item's "Index" view. (Explicitly specify the view in the return statement)
				- This action should provide the "Index" view with a model of type `List<Item>` that contains all items in `_context.Items`.
			- [ ] Create a new Action `Create` in the `ItemController`.
				- This action should have an attribute `HttpGet`.
				- This action should have a return type of `IActionResult`.
				- This action should return the "Create" view. (Explicitly specify the view in the return statement)
			- [ ] Create a new Action `Create` in the `ItemController`.
				- This action should have an attribute `HttpPost`.
				- This action should accept a parameter of type `Item`.
				- This action should have a return type of `IActionResult`.
				- This action should add the provided `Item` to `_context.Items` (_Note_ : Don't forget to `SaveChanges`!)
				- This action should `RedirectToAction` to the `Index` action.
			- [ ] Create a new Action `Delete` in the `ItemController`.
				- This action should accept an `int` parameter named "Id";
				- This action should return a type of `IActionResult`.
				- This action should get the `Item` to be deleted from `_context.Items` then use `_context.Items`
				- This action should remove the `Item` with the matching `Id` property from `_context.Items`. (_Note_ : Don't forget to `SaveChanges`!)
				- This action should `RedirectToAction` to the `Index` action.

## What Now?

Now is a good time to continue on with other [ASP.NET Core courses](https://app.pluralsight.com/library/search?q=asp+net+core) to expand your understanding of the ASP.NET Core framework. You could also take a look at the [Microsoft Azure for Developers](https://app.pluralsight.com/paths/skills/microsoft-azure-for-developers) path as Azure is often used with ASP.NET Core applications.
