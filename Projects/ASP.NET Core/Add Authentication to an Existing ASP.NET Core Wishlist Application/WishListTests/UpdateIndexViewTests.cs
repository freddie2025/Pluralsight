using System.IO;
using System.Text.RegularExpressions;
using Xunit;

namespace WishListTests
{
    public class UpdateIndexViewTests
    {
        [Fact(DisplayName = "Add Using Directives To Index View @add-using-directives-to-index-view")]
        public void AddUsingDirectivesToIndexViewTest()
        {
            var filePath = ".." + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + "WishList" + Path.DirectorySeparatorChar + "Views" + Path.DirectorySeparatorChar + "Home" + Path.DirectorySeparatorChar + "Index.cshtml";
            Assert.True(File.Exists(filePath), @"`Index.cshtml` was not found in the `Views/Home` folder, did you accidentally delete or rename it?");

            string file;
            using (var streamReader = new StreamReader(filePath))
            {
                file = streamReader.ReadToEnd();
            }

            var pattern = @"@using\s*Microsoft.AspNetCore.Identity";
            var rgx = new Regex(pattern);
            Assert.True(rgx.IsMatch(file), @"`Home\Index.cshtml` did not contain a `using` directive for `Microsoft.AspNetCore.Identity`.");

            pattern = @"@using\s*WishList.Models";
            rgx = new Regex(pattern);
            Assert.True(rgx.IsMatch(file), @"`Home\Index.cshtml` did not contain a `using` directive for `WishList.Models`.");
        }

        [Fact(DisplayName = "Add Inject Directive To Index View @add-inject-directive-to-index-view")]
        public void AddInjectDirectiveToIndexViewTest()
        {
            var filePath = ".." + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + "WishList" + Path.DirectorySeparatorChar + "Views" + Path.DirectorySeparatorChar + "Home" + Path.DirectorySeparatorChar + "Index.cshtml";
            Assert.True(File.Exists(filePath), @"`Index.cshtml` was not found in the `Views/Home` folder, did you accidentally delete or rename it?");

            string file;
            using (var streamReader = new StreamReader(filePath))
            {
                file = streamReader.ReadToEnd();
            }

            var pattern = @"@inject\s*SignInManager<ApplicationUser>\s*SignInManager";
            var rgx = new Regex(pattern);
            Assert.True(rgx.IsMatch(file), @"`Home\Index.cshtml` did not contain an `inject` directive for `SignInManager` with a type argument of `ApplicationUser` with the name `SignInManager`.");
        }

        [Fact(DisplayName = "Check If User SignedIn Index View @check-if-user-signedin-index-view")]
        public void CheckIfUserSignedInIndexViewTest()
        {
            var filePath = ".." + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + "WishList" + Path.DirectorySeparatorChar + "Views" + Path.DirectorySeparatorChar + "Home" + Path.DirectorySeparatorChar + "Index.cshtml";
            Assert.True(File.Exists(filePath), @"`Index.cshtml` was not found in the `Views/Home` folder, did you accidentally delete or rename it?");

            string file;
            using (var streamReader = new StreamReader(filePath))
            {
                file = streamReader.ReadToEnd();
            }

            var pattern = @"SignInManager[.]IsSignedIn\s*?[(]\s*?User\s*?[)]";
            var rgx = new Regex(pattern);
            Assert.True(rgx.IsMatch(file), @"`Home\Index.cshtml` did not check if the user is signed in.");

            pattern = @"<\s*?form\s*asp-action\s*?=\s*?""Logout""\s*asp-controller\s*?=\s*?""Account""\s*?method\s*?=\s*?""post""\s*?>";
            rgx = new Regex(pattern);
            Assert.True(rgx.IsMatch(file), @"`Home\Index.cshtml` did not contain a link to the `Account.Logout` action when the user was logged in.");

            pattern = @"<a\s*asp-action\s*?=\s*?""Login""\s*asp-controller\s*?=\s*?""Account""\s*?>\s*?Log in\s*?</\s*?a\s*?>";
            rgx = new Regex(pattern);
            Assert.True(rgx.IsMatch(file), @"`Home\Index.cshtml` did not contain a link to the `Account.Login` action when the user was not logged in.");

            pattern = @"<a\s*asp-action\s*?=\s*?""Register""\s*asp-controller\s*?=\s*?""Account""\s*?>\s*?Register\s*?</\s*?a\s*?>";
            rgx = new Regex(pattern);
            Assert.True(rgx.IsMatch(file), @"`Home\Index.cshtml` did not contain a link to the `Account.Register` action when the user was not logged in.");
        }
    }
}
