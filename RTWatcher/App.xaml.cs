using System.Windows;
using Core.Controls.TitleBar.ViewModels;
using Ninject;
using RTWatcher.MainWindow.ViewModels;
using RTWatcher.MainWindow.Views;

namespace RTWatcher
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private IKernel _container;
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            ConfigureContainer();
            ComposeObjects();
            Current.MainWindow.Show();
        }

        private void ConfigureContainer()
        {
            _container = new StandardKernel();

            _container.Bind<TitleBarViewModel>().ToSelf();
            _container.Bind<MainViewModel>().ToSelf();

            _container.Bind<MainView>().ToMethod(context => new MainView { DataContext = _container.Get<MainViewModel>()});
        }

        private void ComposeObjects()
        {
            Current.MainWindow = _container.Get<MainView>();
        }
    }
}
