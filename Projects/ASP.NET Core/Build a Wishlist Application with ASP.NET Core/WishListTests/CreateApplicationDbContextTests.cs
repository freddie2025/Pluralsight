using System;
using System.IO;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace WishListTests
{
    public class CreateApplicationDbContextTests
    {
        [Fact(DisplayName = "Create Class ApplicationDbContext @create-class-applicationdbcontext")]
        public void CreateApplicationDbContextTest()
        {
            // Get appropriate path to file for the current operating system
            var filePath = ".." + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + "WishList" + Path.DirectorySeparatorChar + "Data" + Path.DirectorySeparatorChar + "ApplicationDbContext.cs";
            // Assert Index.cshtml is in the Views/Home folder
            Assert.True(File.Exists(filePath), "`ApplicationDbContext.cs` was not found in the `Data` folder.");

            var applicationDbContext = TestHelpers.GetUserType("WishList.Data.ApplicationDbContext");

            Assert.True(applicationDbContext != null, "`ApplicationDbContext` class was not found, ensure `ApplicationDbContext.cs` contains a `public` class `AplicationDbContext`.");
            Assert.True(applicationDbContext.BaseType == typeof(DbContext), "`ApplicationDbContext` was found, but did not inherit the `DbContext` class. (this will require a using directive for the `Microsoft.EntityFrameWorkCore` namespace)");
        }

        [Fact(DisplayName = "Add Constructor to ApplictionDbContext @add-constructor-to-applicationdbcontext")]
        public void AddConstructorToApplicationDbContextTest()
        {
            // Get appropriate path to file for the current operating system
            var filePath = ".." + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + "WishList" + Path.DirectorySeparatorChar + "Data" + Path.DirectorySeparatorChar + "ApplicationDbContext.cs";
            // Assert Index.cshtml is in the Views/Home folder
            Assert.True(File.Exists(filePath), "`ApplicationDbContext.cs` was not found in the `Data` folder.");

            var applicationDbContext = TestHelpers.GetUserType("WishList.Data.ApplicationDbContext");

            Assert.True(applicationDbContext != null, "`ApplicationDbContext` class was not found, ensure `ApplicationDbContext.cs` contains a `public` class `AplicationDbContext`.");
            Assert.True(applicationDbContext.BaseType == typeof(DbContext), "`ApplicationDbContext` was found, but did not inherit the `DbContext` class. (this will require a using directive for the `Microsoft.EntityFrameWorkCore` namespace)");

            var constructor = applicationDbContext.GetConstructor(new Type[] { typeof(DbContextOptions) });
            Assert.True(constructor != null, "`ApplicationDbContext` does not appear to contain a constructor accepting a parameter of type `DbContextOptions<ApplicationDbContext>`");
        }
    }
}
