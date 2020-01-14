using System.IO;
using System.Text.RegularExpressions;
using Xunit;

namespace WishListTests
{
    public class AddTagHelpersAndLayoutTests
    {
        [Fact(DisplayName = "Add Tag Helper Support @add-tag-helper-support")]
        public void AddTagHelperSupportTest()
        {
            // Get appropriate path to file for the current operating system
            var filePath = ".." + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + "WishList" + Path.DirectorySeparatorChar + "Views" + Path.DirectorySeparatorChar + "_ViewImports.cshtml";
            // Assert Index.cshtml is in the Views/Home folder
            Assert.True(File.Exists(filePath), "`_ViewImports.cshtml` was not found in the `Views` folder.");

            string file;
            using (var streamReader = new StreamReader(filePath))
            {
                file = streamReader.ReadToEnd();
            }
            Assert.True(file.Contains(@"@addTagHelper *, Microsoft.AspNetCore.Mvc.TagHelpers"), "`_ViewImports.cshtml` was found, but does not appear to contain `@addTagHelper *, Microsoft.AspNetCore.Mvc.TagHelpers`.");
        }

        [Fact(DisplayName = "Add Base Layout @add-base-layout")]
        public void AddBaseLayoutTest()
        {
            // Get appropriate path to file for the current operating system
            var filePath = ".." + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + "WishList" + Path.DirectorySeparatorChar + "Views" + Path.DirectorySeparatorChar + "_ViewStart.cshtml";
            // Assert Index.cshtml is in the Views/Home folder
            Assert.True(File.Exists(filePath), "`_ViewStart.cshtml` was not found in the `Views` folder.");

            string file;
            using (var streamReader = new StreamReader(filePath))
            {
                file = streamReader.ReadToEnd();
            }
            var pattern = @"@{\s*?Layout\s*?=\s*?""_Layout""\s*?;\s*?}";
            var rgx = new Regex(pattern);
            Assert.True(rgx.IsMatch(file), @"`_ViewStart.cshtml` was found, but does not appear to contain `@{ Layout = ""_Layout""; }`.");
        }
    }
}
