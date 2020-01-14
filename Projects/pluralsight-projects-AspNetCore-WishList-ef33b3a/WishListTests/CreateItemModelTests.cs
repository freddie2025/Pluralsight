using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using Xunit;

namespace WishListTests
{
    public class CreateItemModelTests
    {
        [Fact(DisplayName = "Create Item Model @create-item-model")]
        public void CreateItemModelTest()
        {
            var filePath = ".." + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + "WishList" + Path.DirectorySeparatorChar + "Models" + Path.DirectorySeparatorChar + "Item.cs";
            Assert.True(File.Exists(filePath), "`Item.cs` was not found in the `Models` folder.");

            var itemModel = TestHelpers.GetUserType("WishList.Models.Item");

            Assert.True(itemModel != null, "`Item` class was not found, ensure `Item.cs` contains a `public` class `Item`.");
            var idProperty = itemModel.GetProperty("Id");
            Assert.True(idProperty != null && idProperty.PropertyType == typeof(int), "`Item` class did not contain a `public` `int` property `Id`.");
            var descriptionProperty = itemModel.GetProperty("Description");
            Assert.True(descriptionProperty != null && descriptionProperty.PropertyType == typeof(string), "`Item` class did not contain a `public` `string` property `Description`.");
            Assert.True(descriptionProperty.GetCustomAttributes(typeof(RequiredAttribute), false).FirstOrDefault() != null, "`Item` class's `Description` property didn't have a `Required` attribute. (the `RequiredAttribute` can be found in the `System.ComponentModel.DataAnnotations` namespace)");
            Assert.True(((MaxLengthAttribute)descriptionProperty.GetCustomAttributes(typeof(MaxLengthAttribute), false)?.FirstOrDefault())?.Length == 50, "`Item` class's `Description` property didn't have a `MaxLength` attribute of `50`.");
        }

        [Fact(DisplayName = "Add Item to ApplicationDbContext @add-item-to-applicationdbcontext")]
        public void AddItemToApplicationDbContextTest()
        {
            // Get appropriate path to file for the current operating system
            var filePath = ".." + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + "WishList" + Path.DirectorySeparatorChar + "Data" + Path.DirectorySeparatorChar + "ApplicationDbContext.cs";
            // Assert Index.cshtml is in the Views/Home folder
            Assert.True(File.Exists(filePath), "`ApplicationDbContext.cs` was not found in the `Data` folder.");

            var applicationDbContext = TestHelpers.GetUserType("WishList.Data.ApplicationDbContext");

            Assert.True(applicationDbContext != null, "`ApplicationDbContext` class was not found, ensure `ApplicationDbContext.cs` contains a `public` class `AplicationDbContext`.");

            var itemsProperty = applicationDbContext.GetProperty("Items");
            Assert.True(itemsProperty != null, "`ApplicationDbContext` class did not contain a `public` `Items` property.");
            Assert.True(itemsProperty.PropertyType.GenericTypeArguments[0].ToString() == "WishList.Models.Item", "`ApplicationDbContext` class's `Items` property was not of type `DbSet<Item>`.");
        }
    }
}
