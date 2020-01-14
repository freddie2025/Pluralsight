using System.IO;
using System.Text.RegularExpressions;
using Xunit;

namespace WishListTests
{
    public class CreateHomeViewsTests
    {
        [Fact(DisplayName = "Create the Home/Index View @create-index-view")]
        public void CreateIndexViewTest()
        {
            // Get appropriate path to file for the current operating system
            var filePath = ".." + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + "WishList" + Path.DirectorySeparatorChar + "Views" + Path.DirectorySeparatorChar + "Home" + Path.DirectorySeparatorChar + "Index.cshtml";
            // Assert Index.cshtml is in the Views/Home folder
            Assert.True(File.Exists(filePath), "`Index.cshtml` was not found in the `Views" + Path.DirectorySeparatorChar + "Home` folder.");

            string file;
            using (var streamReader = new StreamReader(filePath))
            {
                file = streamReader.ReadToEnd();
            }
            var pattern = @"(?i)<\s?h1\s?>\s?.*<\/\s?h1\s?>";
            var rgx = new Regex(pattern);
            Assert.True(rgx.IsMatch(file), "`Index.cshtml` was found, but does not appear to contain both an opening and closing `h1` tag.");
        }

        [Fact(DisplayName = "Create the Shared/Error View @create-error-view")]
        public void CreateErrorViewTest()
        {
            // Get appropriate path to file for the current operating system
            var filePath = ".." + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + "WishList" + Path.DirectorySeparatorChar + "Views" + Path.DirectorySeparatorChar + "Shared" + Path.DirectorySeparatorChar + "Error.cshtml";
            // Assert Index.cshtml is in the Views/Home folder
            Assert.True(File.Exists(filePath), "`Error.cshtml` was not found in the `Views" + Path.DirectorySeparatorChar + "Shared` folder.");

            string file;
            using (var streamReader = new StreamReader(filePath))
            {
                file = streamReader.ReadToEnd();
            }
            var pattern = @"(?i)<\s*?p\s*?>\s*?an\s*error\s*has\s*occurred[.]\s*please\s*try\s*again[.]\s*?<\/\s*?p\s*?>";
            var rgx = new Regex(pattern);
            Assert.True(rgx.IsMatch(file), "`Error.cshtml` was found, but does not appear to contain both an opening and closing `p` tag containing the message 'An error has occurred. Please try again.'.");
        }
    }
}
