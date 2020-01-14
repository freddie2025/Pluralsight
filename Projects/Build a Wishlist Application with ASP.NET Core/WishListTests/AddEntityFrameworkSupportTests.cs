using System.IO;
using Xunit;

namespace WishListTests
{
    public class AddEntityFrameworkSupportTests
    {
        [Fact(DisplayName = "Configure EntityFramework @configure-entityframework")]
        public void ConfigureEntityFrameworkTest()
        {
            var filePath = ".." + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + "WishList" + Path.DirectorySeparatorChar + "Startup.cs";
            string file;
            using (var streamReader = new StreamReader(filePath))
            {
                file = streamReader.ReadToEnd();
            }

            Assert.True(file.Contains("services.AddDbContext<ApplicationDbContext>"), "`Startup.cs`'s `Configure` did not contain a call to `ApplicationDbContext` with the `ApplicationDbContext` type.");
            Assert.True(file.Contains(@"options => options.UseInMemoryDatabase"), @"`Startup.cs`'s `Configure` called `AddDbContext` but did not provide it the argument `options => options.UseInMemoryDatabase(""WishList"")`.");
        }
    }
}
