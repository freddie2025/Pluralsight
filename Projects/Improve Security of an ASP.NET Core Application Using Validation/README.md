# Improve Security of an ASP.NET Core Application Using Validation

In this project we’ll take an existing conference application written in ASP.NET Core, and implement validation in order to better secure our application from bad data entry, as well as injection attacks.

# Setup the Application

## Using Visual Studio
If you want to use Visual Studio _(highly recommended)_ follow the following steps:
-   If you already have Visual Studio installed make sure you have .NET Core installed by running the "Visual Studio Installer" and making sure ".NET Core cross-platform development" is checked.
-   If you need to install Visual Studio download it at https://visualstudio.microsoft.com/ by selecting "Community 2019" from the "Dowload Visual Studio" drop down list. _(If you're using Windows you'll want to check "ASP.NET" and ".NET Core cross-platform development" on the workloads screen during installation.)_
-   Open the .sln file in Visual Studio.
-   To run the application simply press the Start Debug button _(green arrow)_ or press F5.
-   If you're using Visual Studio on Windows, to run tests open the Test menu, click Run, then click on Run all tests. _(results will show up in the Test Explorer)_
-   If you're using Visual Studio on macOS, to run tests select the ConferenceTrackerTests Project, then go to the Run menu, then click on Run Unit Tests. _(results will show up in the Unit Tests panel)_

_(Note: All tests should fail at this point. This is by design. As you progress through the project, more and more tests will pass. All tests should pass upon completion of the project.)_

## Using a Tool Other Than Visual Studio
If you would rather use something other than Visual Studio:
-   Install the .NET Core SDK from https://dotnet.microsoft.com/download once that installation completes, you're ready to get started!
-   To run the application go into the ConferenceTracker project folder and type `dotnet run`.
-   To run the tests go into the ConferenceTrackerTests project folder and type `dotnet test`.

# Features you will implement

- Frontend validation
- Model validation
- Database validation
- Antiforgery tokens (prevent cross-site request forgery attacks)
- Prevent overposting attacks

## Tasks necessary to complete implementation

__Note:__ This isn't the only way to accomplish implementation. However, this is what the project's tests are expecting. Implementing the features in a different way will likely result in being marked as incomplete / incorrect.

_Validation has two important security concerns: the first is ensuring data integrity which prevents the submission of incomplete or bad data, and the second is preventing malicious efforts such as forgery and injection attacks._

_The basis for validation in ASP.NET Core is DataAnnotations. They can be used to facilitate front end validation, ModelState validation, and database validation. As such, we'll make DataAnnotations available to our model, and setup properties that we want to be not-`null`._
- [ ] In our `Speaker` class at `ConferenceTracker/Entities/`, add `Required` attributes where appropriate. _Note: `Required` is part of `System.ComponentModel.DataAnnotations` which we already have a `using` directive for. Usually you'll need to add this yourself._
    - [ ] Add the `Required` attribute to the following properties:
      - `Id`
      - `FirstName`
      - `LastName`
      - `Description`
      
_We also want to make sure the data submitted is the correct data type to help avoid problems when we're handling that data._
- [ ] Add `DataType` attributes to all appropriate properties in our `Speaker` class.
  - [ ] Add the `DataType` attribute with an argument of `DataType.Text` to the following properties:
    - `FirstName`
    - `LastName`
  - [ ] Add the `DataType` attribute with an argument of `DataType.MultilineText` to the `Description` property.
  - [ ] Add the `DataType` attribute with an argument of `DataType.EmailAddress` to the `EmailAddress` property.
  - [ ] Add the `DataType` attribute with an argument of `DataType.PhoneNumber` to the `PhoneNumber` property.

_We should also ensure what is submitted adheres to the size expectations of our database by limiting the length of our strings._
- [ ] Add `StringLength` attributes to all appropriate properties in our `Speaker` class.
  - [ ] Add the `StringLength` attribute with a `MaximumLength` of `100`, and a `MinimumLength` of `2` to the following properties:
    - `FirstName`
    - `LastName`
  - [ ] Add the `StringLength` attribute with a `MaximumLength` of `500`, and a `MinimumLength` of `10` to the `Description` property.

_Sometimes we need to validate things that are not built into `DataAnnotations`. In these cases we can programmatically do validation on models using the `IValidateObject` interface._ 
- [ ] Using the `IValidatableObject` interface, setup our `Speaker` class to validate that our `EmailAddress` property isn't a `"Technology Live Conference"` email address.
  - [ ] Set up our `Speaker` class to inherit the `IValidatableObject` interface. _Note: your code will not compile at this point, but will soon once the `Validate` method is implemented._
  - [ ] Create a new method, `Validate`, with the following characteristics:
    - Add an access modifier of `public`.
    - Have a return type of `IEnumerable<ValidationResult>`.
    - Add a parameter of type `ValidationContext`.
    - It declares a variable of type `List<ValidationResult>`, and instantiates it to a new empty list of `ValidationResult` objects.
    - It checks if the `EmailAddress` property is not `null` and ends with `"TechnologyLiveConference.com"`. (Use `StringComparison` to make this case insensitive)
      - If this is `true`, add a new `ValidationResult` with an `ErrorMessage` of `"Technology Live Conference staff should not use their conference email addresses."`.
    - Finally, it returns the `List<ValidationResult>` variable.

_While we've set up the `DataAnnotations` needed for our validation, we still have to actually wire it up on our frontend. This client side validation helps reduce server load by preventing invalid submissions._
- [ ] Set up client side validation on our `ConferenceTracker/Views/Speaker/Create.cshtml` view.
  - [ ] At the end of our `Create` view, add a `Scripts` `section`.
    - Add the section using `@section Scripts { }`.
    - Inside our `Scripts` section, use `@{await Html.RenderPartialAsync("_ValidationScriptsPartial");}` to add `_ValidateScriptsPartial` to our `Scripts` section. _The `_ValidateScriptsPartial` is included in the template by default. It contains references to `jquery.validate`. ASP.NET Core uses `jquery.validate` for client side validation.)_
  - [ ] Add a validation summary to our `Create` view's `Create` form.
    - Just inside our `Create` form, before it's first `div`, add a `div` tag with the following attributes:
      - `asp-validation-summary` set to `"ModelOnly"`
      - `class` set to `"text-danger"`
  - [ ] For each of our `Create` form's inputs, add a `span` tag with the following attributes:
    - `asp-validate-for` set to the same value as the `asp-for` of the corresponding `input`
    - `class` set to `"text-danger"`

_Now that we have client side validation setup on our Speaker's Create form, we should also set up our server side validation. Client side validation is helpful, but shouldn't be relied on by itself as it is easy to bypass accidentally or maliciously._
- [ ] Setup `ModelState` validation on our `SpeakerController`'s `HttpPost` `Create` action.
  - [ ] Add a condition to our `SpeakerController`'s `HttpPost` `Create` action that checks `ModelState.IsValid`.
    - If `true`, the action should perform the `Create` and `RedirectToAction` just like it did before.
    - If `false`, the action should `return` `View` with an argument of `speaker`. _ASP.NET Core will automatically carry any validation errors back to the client so long as you've provided the model that failed validation._

_We may have both client side and `ModelState` validation, but we're still potentially vulnerable to cross-site request forgery and overposting attacks._
- [ ] Setup the `HttpPost` `Create` action to validate an `AntiForgeryToken` and use `Bind` on our `speaker` parameter to prevent stuffing.
  - [ ] On our `HttpPost` `Create` action, add the `ValidateAntiForgeryToken` attribute. _That's it! Anytime you make a `Form` in ASP.NET Core it, automatically adds a hidden input with the antiforgery token._
  - [ ] Instead of just accepting `speaker` as is, we should use the `Bind` attribute with an argument of `"Id,FirstName,LastName,Description,EmailAddress,PhoneNumber"` to restrict the action to only accepting those properties. _Otherwise someone could maliciously alter their submission to set the `IsStaff` property._

## What Now?

You've completed the tasks of this project, if you want to continue working on this project some next steps would be to setup rate limiting, caching, and utilize middleware to IP filter to help mitigate attacks and their severity.

Otherwise now is a good time to continue the ASP.Net Core path to expand your understanding of the ASP.Net Core framework or delve into the C# path to expand your knowledge of the C# programming language.