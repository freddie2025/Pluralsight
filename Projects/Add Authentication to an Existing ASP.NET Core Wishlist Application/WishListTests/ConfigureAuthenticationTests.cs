using System.IO;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using WishList.Data;
using WishList.Models;
using Xunit;

namespace WishListTests
{
    public class ConfigureAuthenticationTests
    {
        [Fact(DisplayName = "Update ApplicationDbContexts Inheritance @update-applicationdbcontexts-inheritance")]
        public void UpdateApplicationDbContextInherritenceTest()
        {
            Assert.True(typeof(ApplicationDbContext) != null, "A `public` class `ApplicationDbContext` was not found in the `WishList.Data` namespace, was it accidentally renamed or deleted?");
            Assert.True(typeof(ApplicationDbContext).BaseType == typeof(IdentityDbContext<ApplicationUser>), "`ApplicationDbContext` is not inheriting an `IdentityDbContext` with a type argument of `ApplicationUser`.");
        }

        [Fact(DisplayName = "Call AddIdentity In Configure @call-addidentity-in-configureservices")]
        public void CallAddIdentityInConfigureServicesTest()
        {
            var filePath = ".." + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + "WishList" + Path.DirectorySeparatorChar + "Startup.cs";
            Assert.True(File.Exists(filePath), "`Startup.cs` was not found. Did you rename or delete it?");

            string file;
            using (var streamReader = new StreamReader(filePath))
            {
                file = streamReader.ReadToEnd();
            }

            var pattern = @"services\s*?[.]AddIdentity\s*?[<]\s*?ApplicationUser\s*?,\s*?IdentityRole\s*?[>]\s*?[(]\s*?[)]\s*?[.]AddEntityFrameworkStores\s*?[<]\s*?ApplicationDbContext\s*?[>]\s*?[(]\s*?[)]\s*?[.]AddDefaultTokenProviders\s*?[(]\s*?[)]\s*?;";
            var rgx = new Regex(pattern);

            Assert.True(rgx.IsMatch(file), "`Startup.ConfigureServices` didn't contain calls for `AddIdentity` on `services`. (you can copy this call from the task instructions)");
        }

        [Fact(DisplayName = "Call UseAuthentication In ConfigureServices @call-useauthentication-in-configure")]
        public void CallUseAuthenticationInConfigureTest()
        {
            var filePath = ".." + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + "WishList" + Path.DirectorySeparatorChar + "Startup.cs";
            Assert.True(File.Exists(filePath), "`Startup.cs` was not found. Did you rename or delete it?");

            string file;
            using (var streamReader = new StreamReader(filePath))
            {
                file = streamReader.ReadToEnd();
            }

            var pattern = @"app.UseAuthentication[(]\s*?[)]\s*?;\s*?app.UseMvc";
            var rgx = new Regex(pattern);

            Assert.True(rgx.IsMatch(file), "`Startup.Configure` should call `UseAuthentication` before `UseMvcWithDefaultRoute`.");
        }
    }
}
