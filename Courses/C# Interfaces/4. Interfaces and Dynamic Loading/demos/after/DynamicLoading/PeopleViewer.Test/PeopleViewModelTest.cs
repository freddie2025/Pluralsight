using NUnit.Framework;
using System.Linq;

namespace PeopleViewer.Test
{
    public class PeopleViewModelTest
    {
        [Test]
        public void People_OnFetchData_IsPopulated()
        {
            // Arrange
            var viewModel = new PeopleViewModel();

            // Act
            viewModel.FetchData();

            // Assert
            Assert.IsNotNull(viewModel.People);
            Assert.AreEqual(2, viewModel.People.Count());
        }

        [Test]
        public void RepositoryType_OnCreation_ReturnsFakeRepositoryString()
        {
            var viewModel = new PeopleViewModel();
            var expectedString = "PersonRepository.Fake.FakeRepository";

            Assert.AreEqual(expectedString, viewModel.RepositoryType);
        }

        [Test]
        public void People_OnClearData_IsEmpty()
        {
            // Arrange
            var viewModel = new PeopleViewModel();
            viewModel.FetchData();
            Assert.AreEqual(2, viewModel.People.Count(),
                "Invalid Arrangement");

            // Act
            viewModel.ClearData();

            // Assert
            Assert.AreEqual(0, viewModel.People.Count());
        }
    }
}
