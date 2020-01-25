using Common;
using Ninject;
using PersonRepository.Caching;
using PersonRepository.CSV;
using PersonRepository.Service;
using System.Windows;

namespace PeopleViewer.Ninject
{
    public partial class App : Application
    {
        IKernel Container = new StandardKernel();

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            Container.Bind<IPersonRepository>().To<CSVRepository>();
            Application.Current.MainWindow = Container.Get<MainWindow>();

            Application.Current.MainWindow.Show();
        }
    }
}
