using PersonRepository.Factory;
using PersonRepository.Interface;
using System.Windows;

namespace PeopleViewer
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ServiceFetchButton_Click(object sender, RoutedEventArgs e)
        {
            PopulateListBox("Service");
        }

        private void CSVFetchButton_Click(object sender, RoutedEventArgs e)
        {
            PopulateListBox("CSV");
        }

        private void SQLFetchButton_Click(object sender, RoutedEventArgs e)
        {
            PopulateListBox("SQL");
        }

        private void PopulateListBox(string repositoryType)
        {
            ClearListBox();

            IPersonReader repository = 
                RepositoryFactory.GetRepository(repositoryType);
            var people = repository.GetPeople();
            foreach (var person in people)
                PersonListBox.Items.Add(person);

            ShowRepositoryType(repository);
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            ClearListBox();
        }

        private void ClearListBox()
        {
            PersonListBox.Items.Clear();
            RepositoryTypeTextBlock.Text = string.Empty;
        }

        private void ShowRepositoryType(IPersonReader repository)
        {
            RepositoryTypeTextBlock.Text = repository.GetType().ToString();
        }
    }
}
