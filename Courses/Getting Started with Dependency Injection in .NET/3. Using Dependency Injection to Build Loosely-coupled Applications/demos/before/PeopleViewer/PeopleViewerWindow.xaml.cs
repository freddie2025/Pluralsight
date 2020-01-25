using PeopleViewer.Presentation;
using System.Windows;

namespace PeopleViewer
{
    public partial class PeopleViewerWindow : Window
    {
        PeopleViewModel viewModel;

        public PeopleViewerWindow()
        {
            InitializeComponent();
            viewModel = new PeopleViewModel();
            this.DataContext = viewModel;
        }

        private void FetchButton_Click(object sender, RoutedEventArgs e)
        {
            viewModel.RefreshPeople();
            ShowRepositoryType();
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            viewModel.ClearPeople();
            ClearRepositoryType();
        }

        private void ShowRepositoryType()
        {
            RepositoryTypeTextBlock.Text = viewModel.DataReaderType;
        }

        private void ClearRepositoryType()
        {
            RepositoryTypeTextBlock.Text = string.Empty;
        }
    }
}
