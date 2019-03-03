using System.Windows;
using Core.Client.Controls.TitleBar.ViewModels;
using Core.Commands;
using RTWatcher.Common;
using RTWatcher.Properties;

namespace RTWatcher.ViewModels
{
    /// <summary>
    /// ViewModel of main window
    /// </summary>
    public class MainViewModel : BaseViewModelWithView
    {
        /// <summary>
        /// Class initialization <see cref="MainViewModel"/>
        /// </summary>
        public MainViewModel(TitleBarViewModel titleBarViewModel)
        {
            InitTitleBar(titleBarViewModel);
        }

        /// <summary>
        /// ViewMоdel of titlebar.
        /// </summary>
        public TitleBarViewModel TitleBarViewModel { get; set; }
        
        /// <summary>
        /// Sstate of window.
        /// </summary>
        public WindowState WindowState { get; set; }

        private void InitTitleBar(TitleBarViewModel titleBarViewModel)
        {
            TitleBarViewModel = titleBarViewModel;
            TitleBarViewModel.Title = Resources.Title;

            TitleBarViewModel.CloseCommand = new DelegateCommand(o => CurrentView.Close());

            TitleBarViewModel.ChangeWindowStateCommand = new DelegateCommand(o =>
            {
                if (WindowState == WindowState.Maximized)
                {
                    WindowState = WindowState.Normal;
                    OnPropertyChanged(nameof(WindowState));
                }
                else if (WindowState == WindowState.Normal)
                {
                    WindowState = WindowState.Maximized;
                    OnPropertyChanged(nameof(WindowState));
                }
            });

            TitleBarViewModel.MinimizeCommand = new DelegateCommand(o =>
            {
                WindowState = WindowState.Minimized;
                OnPropertyChanged(nameof(WindowState));
            });
        }
    }
}
