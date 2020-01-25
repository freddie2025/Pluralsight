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

        }

        private void CSVFetchButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SQLFetchButton_Click(object sender, RoutedEventArgs e)
        {

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

        private void ShowRepositoryType(IPersonRepository repository)
        {
            RepositoryTypeTextBlock.Text = repository.GetType().ToString();
        }
    }
}
