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
        private IPersonRepository GetTestRepository()
        {
            List<Person> testPeople = TestData.testPeople;
            var mockRepo = new Mock<IPersonRepository>();
            mockRepo.Setup(r => r.GetPeople()).Returns(testPeople);
            return mockRepo.Object;
        }

        [Test]
        public void RefreshPeople_OnExecute_PeopleIsPopulated()
        {
            // Arrange
            var repository = GetTestRepository();
            var viewModel = new PeopleViewModel(repository);

            // Act
            viewModel.RefreshPeople();

            // Assert
            Assert.IsNotNull(viewModel.People);
            Assert.AreEqual(2, viewModel.People.Count());
        }

        [Test]
        public void ClearPeople_OnExecute_PeopleIsEmpty()
        {
            // Arrange
            var repository = GetTestRepository();
            var vm = new PeopleViewModel(repository);
            vm.RefreshPeople();
            Assert.AreEqual(2, vm.People.Count(), "Invalid Arrangement");

            // Act
            vm.ClearPeople();

            // Assert
            Assert.AreEqual(0, vm.People.Count());
        }
    }
}
