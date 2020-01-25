using PeopleViewer.Presentation;
using PersonRepository.Caching;
using PersonRepository.CSV;
using PersonRepository.Service;
using System.Windows;

namespace PeopleViewer
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var wrappedRepo = new ServiceRepository();
            var repository = new CachingRepository(wrappedRepo);
            var viewModel = new PeopleViewModel(repository);
            Application.Current.MainWindow = new MainWindow(viewModel);

            Application.Current.MainWindow.Show();
        }
    }
}
