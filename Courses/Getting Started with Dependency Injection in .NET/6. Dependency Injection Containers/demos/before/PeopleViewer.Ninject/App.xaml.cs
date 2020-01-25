using System.Windows;

namespace PeopleViewer.Ninject
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            ConfigureContainer();
            ComposeObjects();
            Application.Current.MainWindow.Title = "With Dependency Injection - Ninject";
            Application.Current.MainWindow.Show();
        }

        private void ConfigureContainer()
        {

        }

        private void ComposeObjects()
        {
            
        }
    }
}
