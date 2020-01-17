using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using WishList.Models;
using Xunit;

namespace WishListTests
{
    public class UpdateModelRelationshipTests
    {
        [Fact(DisplayName = "Update Item Model @update-item-model")]
        public void UpdateItemModel()
        {
            var filePath = ".." + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + "WishList" + Path.DirectorySeparatorChar + "Models" + Path.DirectorySeparatorChar + "Item.cs";
            Assert.True(File.Exists(filePath), @"`Item.cs` was not found in the `Models` folder, did you accidentally rename or remove it?");

            var item = TestHelpers.GetUserType("WishList.Models.Item");
            Assert.True(item != null, "A `public` class `Item` was not found in the `WishList.Models` namespace, did you accidentally rename or remove it?");

            var userProperty = item.GetProperty("User");
            Assert.True(userProperty != null, "`Item` does not appear to contain a `public` `virtual` `ApplicationUser` property `User`");
            Assert.True(userProperty.PropertyType == typeof(ApplicationUser),"`Item` contained a property `User` but it was not of type `ApplicationUser`");
            Assert.True(userProperty.GetMethod.IsVirtual, "`Item` contained a property `User` but it didn't use the `virtual` keyword.");
        }

        [Fact(DisplayName = "Update ApplicationUser Model @update-applicationuser-model")]
        public void UpdateApplicationUserModel()
        {
            var filePath = ".." + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + "WishList" + Path.DirectorySeparatorChar + "Models" + Path.DirectorySeparatorChar + "ApplicationUser.cs";
            Assert.True(File.Exists(filePath), @"`ApplicationUser.cs` was not found in the `Models` folder, did you accidentally rename or remove it?");

            var item = TestHelpers.GetUserType("WishList.Models.ApplicationUser");
            Assert.True(item != null, "A `public` class `ApplicationUser` was not found in the `WishList.Models` namespace, did you accidentally rename or remove it?");

            var itemsProperty = item.GetProperty("Items");
            Assert.True(itemsProperty != null, "`ApplicationUser` does not appear to contain a `public` `virtual` `ICollection` with a type argument of `Item` property `Items`");
            Assert.True(itemsProperty.PropertyType == typeof(ICollection<Item>), "`ApplicationUser` contained a property `Items` but it was not of type `ICollection` with a type argument of `Item`");
            Assert.True(itemsProperty.GetMethod.IsVirtual, "`ApplicationUser` contained a property `Items` but it didn't use the `virtual` keyword.");
        }
    }
}
