using Common;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PeopleViewer.Presentation.Tests
{
    public class PeopleViewModelTests
    {
        [Test]
        public void RefreshPeople_OnExecute_PeopleIsPopulated()
        {
            // Arrange

            // Act

            // Assert
        }

        //[Test]
        //public void ClearPeople_OnExecute_PeopleIsEmpty()
        //{
        //    // Arrange
        //    var repository = GetTestRepository();
        //    var vm = new PeopleViewModel(repository);
        //    vm.RefreshPeople();
        //    Assert.AreEqual(2, vm.People.Count(), "Invalid Arrangement");

        //    // Act
        //    vm.ClearPeople();

        //    // Assert
        //    Assert.AreEqual(0, vm.People.Count());
        //}
    }
}
