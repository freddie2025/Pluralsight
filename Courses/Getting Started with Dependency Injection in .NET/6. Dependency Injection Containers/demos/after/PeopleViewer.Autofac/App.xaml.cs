using Autofac;
using Autofac.Features.ResolveAnything;
using PeopleViewer.Common;
using PeopleViewer.Presentation;
using PersonDataReader.CSV;
using PersonDataReader.Decorators;
using PersonDataReader.Service;
using PersonDataReader.SQL;
using System.Windows;

namespace PeopleViewer.Autofac
{
    public partial class App : Application
    {
        IContainer Container;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            ConfigureContainer();
            ComposeObjects();
            Application.Current.MainWindow.Title = "With Dependency Injection - Autofac";
            Application.Current.MainWindow.Show();
        }

        private void ConfigureContainer()
        {
            var builder = new ContainerBuilder();

            // DATA READER TYPE OPTION #1 - No Decorator
            //builder.RegisterType<SQLReader>().As<IPersonReader>()
                //.SingleInstance();

            // DATA READER TYPE OPTION #2 - With Decorator
            builder.RegisterType<ServiceReader>().Named<IPersonReader>("reader")
                .SingleInstance();
            builder.RegisterDecorator<IPersonReader>(
                (c, inner) => new CachingReader(inner), fromKey: "reader");

            // OTHER TYPES OPTION #1 - Automatic Registration
            //builder.RegisterSource(new AnyConcreteTypeNotAlreadyRegisteredSource());

            // OTHER TYPES OPTION #2 - Explicit Registration
            builder.RegisterType<PeopleViewerWindow>().InstancePerDependency();
            builder.RegisterType<PeopleViewModel>().InstancePerDependency();

            Container = builder.Build();
        }

        private void ComposeObjects()
        {
            Application.Current.MainWindow = Container.Resolve<PeopleViewerWindow>();
        }
    }
}
