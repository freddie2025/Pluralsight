Getting Started with Entity Framework 6
Julie Lerman
pluralsight.com

This solution needs some TLC before you can use it.

When you open the solution in Visual Studio, you will need to build it for Nuget to bring in the relevant packages.

Creating the database:
Make the console application the startup project.
The Main method is currently set up to only run one method, DataHelpers.NewDbWithSeed.
That will create a database in the developer database (SQL Server LocalDB).
Then comment out that call (it's on line 34).

You'll need to set each app as startup project if you want to run or debug them. The solution contains:
Console App used in other modules
MVCAppNinjaOnly: This is the default MVC app that is generated from the scaffolding tools.
MVCAppRepository: This is teh MVC app that uses the disconnected repository
WPF Application: The WPF app that is in the module.
WebAPI: This is the Web API from the latter part of the module.

The Aurelia Web Site
This is in the download just for fun and so you can see the code within, but I cannot support it if you have technical questions.
This is in a folder but not part of the VIsual Studio solution. I used a text editor to work with the files.

If you want to run the front end Aurelia application, you have a lot of set up to do! And that gets run from the command line using the "gulp watch" command.

I removed some key folders beause of their size so if you are planning to run this you will need to run the following three comnmands at the command line in the Aurelia Front End directory:
   set PYTHON=python2.7
   npm install
   jspm install

I would recommend watching Scott Allen's Aurelia course on Pluralsight.com or jumping over to aurelia.io if you want to run it.

Julie Lerman
thedatafarm.com
twitter @julielerman