using System.IO;
using Xunit;

namespace WishListTests
{
    public class AddMVCMiddlewareTests
    {
        [Fact(DisplayName = "Add MVC Middleware to ConfigureServices @add-mvc-middleware-to-configureservices")]
        public void AddMVCCallAdded()
        {
            var filePath = ".." + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + "WishList" + Path.DirectorySeparatorChar + "Startup.cs";
            string file;
            using (var streamReader = new StreamReader(filePath))
            {
                file = streamReader.ReadToEnd();
            }

            Assert.True(file.Contains("services.AddMvc();"), "`Startup.cs`'s `ConfigureServices` did not contain a call to `AddMvc`.");
        }

        [Fact(DisplayName = "Configure MVC Middleware In Configure @configure-mvc-middleware-in-configure")]
        public void UseMVCAdded()
        {
            var filePath = ".." + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + "WishList" + Path.DirectorySeparatorChar + "Startup.cs";
            string file;
            using (var streamReader = new StreamReader(filePath))
            {
                file = streamReader.ReadToEnd();
            }

            Assert.True(file.Contains("app.UseMvcWithDefaultRoute();"), "`Startup.cs`'s `Configure` did not contain a call to `UseMvcWithDefaultRoute`.");
        }
    }
}
