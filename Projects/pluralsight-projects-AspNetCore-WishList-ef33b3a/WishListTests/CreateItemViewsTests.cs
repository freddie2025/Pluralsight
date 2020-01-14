using System.IO;
using System.Text.RegularExpressions;
using Xunit;

namespace WishListTests
{
    public class CreateItemIndexViewTests
    {
        [Fact(DisplayName = "Create Item's Index View @create-items-index-view")]
        public void CreateItemsIndexView()
        {
            // Get appropriate path to file for the current operating system
            var filePath = ".." + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + "WishList" + Path.DirectorySeparatorChar + "Views" + Path.DirectorySeparatorChar + "Item" + Path.DirectorySeparatorChar + "Index.cshtml";
            // Assert Index.cshtml is in the Views/Home folder
            Assert.True(File.Exists(filePath), "`Index.cshtml` was not found in the `Views" + Path.DirectorySeparatorChar + "Item` folder.");

            string file;
            using (var streamReader = new StreamReader(filePath))
            {
                file = streamReader.ReadToEnd();
            }
            var pattern = @"@model\s*?List\s*?<\s*?WishList[.]Models[.]Item\s*?>";
            var rgx = new Regex(pattern);
            Assert.True(rgx.IsMatch(file), "`Index.cshtml` was found, but does not appear to have a model of `List<Item>`.");
            pattern = @"(?i)<\s*?h1\s*?>\s*?wishlist\s*?</\s*?h1\s*?>";
            rgx = new Regex(pattern);
            Assert.True(rgx.IsMatch(file), "`Index.cshtml` was found, but does not appear to have a include an opening and closing `h1` tag with a contents of 'Wishlist'");
            pattern = @"<\s*?[uU][lL]\s*?>\s*?@foreach\s*?[(]\s*?(var|Item)\s*item\s*in\s*Model\s*?[)]\s*?{\s*?<\s*?[lL][iI]\s*?>\s*?@item.Description\s*?<\s*?[aA](.*)\s*?>\s*?delete\s*?</\s*?[aA]\s*?>\s*?</\s*?[lL][iI]\s*?>\s*?}\s*?</\s*?[uU][lL]\s*?>";
            rgx = new Regex(pattern);
            Assert.True(rgx.IsMatch(file), "`Index.cshtml` was found, but does not appear to contain a `ul` with a `foreach` loop that provides the `item.Description` and a link to the `delete` action foreach item.");
            pattern = @"(?i)<\s*?a(\s*?.*)\s*?>\s*?delete";
            rgx = new Regex(pattern);
            var aTag = rgx.Match(file).Value;
            Assert.True(aTag.Contains(@"asp-action=""delete""") && aTag.Contains(@"asp-route-id=""@item.Id"""), "`Index.cshtml` contains an `a` tag, but that `a` tag does not appear to have both tag helpers `asp-action` set to 'delete' and `asp-route-id` set to `@item.Id`");
        }

        [Fact(DisplayName = "Add Item Link To Home @add-item-link-to-home")]
        public void AddItemLinkToHomeTest()
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
            var pattern = @"(?i)<\s*?a\s*asp-action\s*?=\s*?""index""\s*asp-controller\s*?=\s*?""item""\s*?>\s*?view wishlist\s*?<[/]\s*?a\s*?>";
            var rgx = new Regex(pattern);
            Assert.True(rgx.IsMatch(file), "`Index.cshtml` was found, but does not appear to contain link to the `ItemController.Index` action. (use the `asp-action` and `asp-controller` tag helpers)");
        }
    }
}
