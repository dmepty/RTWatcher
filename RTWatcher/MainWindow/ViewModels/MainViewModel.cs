using System.Windows;
using Core.Commands;
using Core.Controls.TitleBar.ViewModels;
using Microsoft.Win32;
using RTWatcher.Common;
using RTWatcher.Properties;

namespace RTWatcher.MainWindow.ViewModels
{
    /// <summary>
    /// ViewModel of main window
    /// </summary>
    public class MainViewModel : BaseViewModelWithView
    {
        private OpenFileDialog _openFileDialog;
        private string _fileName;

        /// <summary>
        /// Class initialization <see cref="MainViewModel"/>
        /// </summary>
        public MainViewModel(TitleBarViewModel titleBarViewModel)
        {
            InitOpenFileDialog();
            InitTitleBar(titleBarViewModel);

            OpenFileCommand = new DelegateCommand(o =>
            {
                
                if (_openFileDialog.ShowDialog() == true)
                {
                    _fileName = _openFileDialog.FileName;
                }
            });
        }

        /// <summary>
        /// ViewMоdel of titlebar.
        /// </summary>
        public TitleBarViewModel TitleBarViewModel { get; set; }
        
        /// <summary>
        /// Sstate of window.
        /// </summary>
        public WindowState WindowState { get; set; }

        public DelegateCommand OpenFileCommand { get; set; }

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

        private void InitOpenFileDialog()
        {
            _openFileDialog = new OpenFileDialog
            {
                Filter = "Text files (*.txt)|*.txt|Log files (*.log)|*.log",
                Multiselect = false,
                CheckFileExists = true
            };
        }
    }
}
