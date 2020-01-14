using System.IO;
using System.Text.RegularExpressions;
using Xunit;

namespace WishListTests
{
    public class CreateItemCreateViewTests
    {
        [Fact(DisplayName = "Create Create View @create-create-view")]
        public void CreateCreateView()
        {
            // Get appropriate path to file for the current operating system
            var filePath = ".." + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + "WishList" + Path.DirectorySeparatorChar + "Views" + Path.DirectorySeparatorChar + "Item" + Path.DirectorySeparatorChar + "Create.cshtml";
            // Assert Index.cshtml is in the Views/Home folder
            Assert.True(File.Exists(filePath), "`Create.cshtml` was not found in the `Views" + Path.DirectorySeparatorChar + "Item` folder.");

            string file;
            using (var streamReader = new StreamReader(filePath))
            {
                file = streamReader.ReadToEnd();
            }
            var pattern = @"@model\s*WishList[.]Models[.]Item";
            var rgx = new Regex(pattern);
            Assert.True(rgx.IsMatch(file), "`Create.cshtml` was found, but does not appear to have a model of `Item`.");
            pattern = @"<\s*?[hH]3\s*?>\s*?Add [iI]tem [tT]o [wW]ishlist\s*?</\s*?[hH]3\s*?>";
            rgx = new Regex(pattern);
            Assert.True(rgx.IsMatch(file), @"`Create.cshtml` was found, but does not appear to have a include an opening and closing `h3` tag with a contents of `""Add item to wishlist""`");
            pattern = @"<\s*?form\s*asp-action\s*?=\s*?""[cC]reate""\s*?>(\s*?.*)*?</\s*?form\s*?>";
            rgx = new Regex(pattern);
            Assert.True(rgx.IsMatch(file), @"`Create.cshtml` was found, but does not appear to contain a `form` with the attribute `asp-action` set to `""create""`.");
            pattern = @"<\s*?form(\s*?.*)>(\s*?.*)<\s*?input\s*asp-for\s*?=\s*?""[dD]escription""\s*?([/]>|>[/]s*?<[/]\s*?input\s*?>)(\s*?.*)*?<[/]\s*?form\s*?>";
            rgx = new Regex(pattern);
            Assert.True(rgx.IsMatch(file), @"`Create.cshtml` was found, but does not appear to contain a `form` containing an `input` tag with an attribute `asp-for` set to `""Description""`.");
            pattern = @"<\s*?form\s*?.*\s*?>\s*?.*\s*?<\s*?span\s*?asp-validation-for\s*?=\s*?""[dD]escription""\s*?>\s*?<[/]\s*?span\s*?>(\s*?.*)*<[/]\s*?form\s*?>";
            rgx = new Regex(pattern);
            Assert.True(rgx.IsMatch(file), @"`Create.cshtml` was found, but does not appear to contain a `form` containing an `span` tag with an attribute `asp-validation-for` set to `""Description""`.");
            pattern = @"<\s*?button\s*type\s*?=\s*?""submit"".*>\s*?Add [iI]tem\s*?<[/]\s*?button\s*?>\s*?</\s*?form\s*?>";
            rgx = new Regex(pattern);
            Assert.True(rgx.IsMatch(file), @"`Create.cshtml` was found, but does not appear to contain a `form` containing an `button` tag with an attribute `type` set to `submit` with the text '""Add item""'.");
        }
    }
}
