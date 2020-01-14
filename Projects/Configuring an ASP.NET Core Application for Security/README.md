# ASP.NET Core Security Project: Configuring Application for Security

In this project, we’ll take an existing conference application written in ASP.NET Core and implement several simple security measures to help improve application security.

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

- Require HTTPS (What is HTTPS? https://en.wikipedia.org/wiki/HTTPS)
- Utilize HSTS (What is HSTS? https://en.wikipedia.org/wiki/HTTP_Strict_Transport_Security)
- Restrict cross origin requests
- Setup basic error handling
- Setup basic logging
- Store and consume user secrets
- Set and configure cookie policy options

## Tasks necessary to complete implementation

__Note:__ This isn't the only way to implement this project. However, this is what the project's tests are expecting. Implementing the features in a different way will likely result in being marked as incomplete / incorrect.

### Improve application security through Startup configuration

_In order to properly secure any web application, HTTPS's end to end encryption has become effectively a mandatory to help prevent "man in the middle" attacks._
- [ ] Redirect users who access the web application through `HTTP` to use `HTTPS` instead
    - [ ] In `Startup` class's `Configure` method, call the `UseHttpsRedirection` method on `app`. _(This should be done after our database is created, but before `UseStaticFiles` is called.)_

_To help prevent "protocol downgrade" and "cookie hijacking" attacks we can utilize HTTP Strict-Transport-Security (HSTS). This should be limited to only happen in production._
- [ ] Setup the web application to use the HSTS header
  - [ ] In `Startup` class's `Configure` method, as the very first thing we do in that method, call the `IsDevelopment` method on `env`, and check if it returns `true` or `false`.
    - If `false`, call `UseHsts` on `app`.

_We need the ability to see when something has gone wrong in detail as a developer, while also ensuring we don't accidentally expose that information to our users._
- [ ] Setup separate error pages for developers and users
  - [ ] In `Startup` class's `Configure` method, if `IsDevelopment` returns:
    - `false` call the `UseExceptionHandler` method on `app` with an argument of `"/Home/Error"` before the call to `UseHsts`.
    - `true` call the `UseDeveloperExceptionPage` method on `app` and the `UseDatabaseErrorPage` method on `app`.

_The best way to prevent "Cross Orgin" attacks is to not use Cross-Origin Resourc Sharing (CORS). However this isn't always an option: in such cases we can specify which domains to allow requests._
- [ ] Setup CORS to permit requests from a single domain
  - [ ] In our `Startup` class, create a new `private` `readonly` field of type `string` named `_allowedOrigins`, and set it to the value `"_allowedOrigins"`.
  - [ ] In our `Startup` class's `ConfigureServices` method, add a call to the method `AddCors` on `services` and provide it an argument of `options => { options.AddPolicy(_allowedOrigins, builder => { builder.WithOrigins("http://pluralsight.com"); }); }`. _(This is specifying the name of our CORS policy, and providing what domains will be permitted)_
  - [ ] In our `Startup` class's `Configure` method, add a call to `UseCors` on `app` with `_allowedOrigins` as the arguement. Do this before our call to `UseHttpsRedirection`.

_Logging is an important strategy for diagnosing bugs, spotting attempts to compromise security, as well as investigating what happened after something has gone wrong._

_Note: Logging methods are not asynchronous, nor should they be as logging asynchronously can cause logged messages to be out of order._

_Note: Logging is enabled by default and provides `Console`, `Debug`, `EventSource`, and `EventLog` logging. `EventLog` is a Windows feature, and on other operating systems `EventLog` logging will be ignored._
- [ ] Add logging to our `Startup` class
  - [ ] Update our `Startup` class's `Configure` method, to log if the application is running in development.
    - Add a using directive for `Microsoft.Extensions.Logging`.
    - Update the `Configure` method's signature to take a third parameter of type `ILogger<Startup>` with a name of `logger`.
    - In our existing condition that checks if `IsDevelopment` is `true`, when `true` call `LogInformation` on `logger` with an argument of `"Environment is in development"` before our exception handling.

- [ ] Add logging to our `PresentationsController` class
  - [ ] In our `PresentationsController` class, add a `private` `readonly` field of type `ILogger` named `_logger`.
  - [ ] Update our `PresentationsController` update the constructor to take a third parameter of type `ILogger<PresentationsController>` named `logger` and set `_logger` to `logger`.
  
  _We'll add logging to see when our `Edit` action is called, and what outcome occurred via logging. Note some of this logging will duplicate built in logging. We will not add logging to everything. This is simply to familiarize yourself with logging in a controller action._
  - [ ] Update our `PresentationsController`'s `Edit` method (the `HTTP Get` not the `HTTP Post` `Edit` method), add the following logging.
    - As the very first line call `LogInformation` on `_logger` with a message `"Getting presentation id:" + id +  " for edit."`.
    - Inside the condition where we check if `id` is `null`, before we return `NotFound()`, call `LogError` on `_logger` with a message `"Presentation id was null."`.
    - Inside the condition where we check if `presentation` is `null`, before we return `NotFound()`, call `LogWarning` on `_logger` with a message `"Presentation id," + id + ", was not found."`.
    - Immediately before we set our `ViewData`, call `LogInformation` on `_logger` with a message `"Presentation id," + id + ", was found. Returning 'Edit view'`.

_We need to be very careful handling sensitive information like connection strings. We absolutely DO NOT want these in our code base where they could accidently be committed to our repository potentially compromising our database credentials. Instead, locally we can use `UserSecrets` and in production `EnvirornmentVariables` to handle this information securely._

_First we need to set up `UserSecrets` and `EnvironmentVariables`, as of ASP.NET Core 2.0 that's just done by default when `CreateDefaultBuilder` is called in our `Program` class! So, we can skip to just putting them to use!_
- [ ] Add a call to retrieve `SecretMessage` from `Configuration`
  - [ ] Inside our `Startup` class's `ConfigureServices` method, set our `SecretMessage` property using to the returned value from `Configuration["SecretMessage"]`. _Normally, you'd use this to contain things like connection strings. However, since we're using an `InMemory` database, this is simply being used as an example, and serves no functional purpose._

_This is how you set user secrets. However, because they're a secret only stored on your local computer, we can't actually check to see if you did it right. Which is a good thing... but... means we can't tell you if it worked :)_

- [ ] Create a "Password" secret
  - We're going to use the .NET Core CLI (Command Line Interface).
  - In the CLI navigate to the `ConferenceTracker` directory, not the solution's directory. (You can use the `cd` command to navigate between directories. Example: `cd ConferenceTracker`)
  - Enter the command `dotnet user-secrets init` 
    - _this sets the `secretsId` for your project_
  - Enter the command `dotnet user-secrets set "SecretMessage" "Keep it secret, Keep it safe."` 
    - _this sets a secret with the key `"SecretMessage"` with a value "Keep it secret, Keep it safe."_

_While not directly security related, we do need to ensure we're complying with General Data Protection Regulation (GDPR) legislation if our website is accessible to Europe. From the web application perspective this means we need to set our cookie policy._
- [ ] Add `CookiePolicy` to our `Startup` class to get user consent for any cookies we might use
  - [ ] Inside `Startup` class's `ConfigureServices` method, anywhere before our call to `AddControllersWithViews`, call `Configure<CookiePolicyOptions>` on `services` with the argument `options => { options.CheckConsentNeeded = context => true; options.MinimumSameSitePolicy = SameSiteMode.None; }`. _(Don't worry about specifically what these arguments mean. You'll need to change them based on what cookies you use and what they store)_
  - [ ] Inside `Startup` class's `Configure` method, anywhere before our call to `UseRouting`, call `UseCookiePolicy` on `app`.

## What Now?

You've completed the tasks of this project. Congratulations! If you want to continue working on this project, some next steps would be to expand the existing functionality of the application to include an administration section to manage your existing users, implement caching, and make the information stored in the application available via a secure API.

Otherwise now is a good time to continue the ASP.NET Core path to expand your understanding of the ASP.NET Core framework, or delve into the C# path to expand your knowledge of the C# programming language.