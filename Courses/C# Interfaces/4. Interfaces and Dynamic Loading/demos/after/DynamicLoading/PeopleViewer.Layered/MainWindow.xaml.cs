using System.Windows;

namespace PeopleViewer
{
    public partial class MainWindow : Window
    {
        PeopleViewModel viewModel;

        public MainWindow()
        {
            InitializeComponent();
            viewModel = new PeopleViewModel();
            this.DataContext = viewModel;
        }

        private void FetchButton_Click(object sender, RoutedEventArgs e)
        {
            viewModel.FetchData();
            ShowRepositoryType();
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            viewModel.ClearData();
            RepositoryTypeTextBlock.Text = string.Empty;
        }

        private void ShowRepositoryType()
        {
            RepositoryTypeTextBlock.Text = viewModel.RepositoryType;
        }
    }
}
