using Core.Commands;
using Core.Common;

namespace Core.Controls.TitleBar.ViewModels
{
    /// <summary>
    /// ViewModel of title bar.
    /// </summary>
    public class TitleBarViewModel : BaseViewModel
    {
        private string _title;

        /// <summary>
        /// Class initialization <see cref="TitleBarViewModel"/>.
        /// </summary>
        public TitleBarViewModel()
        {
            
        }

        /// <summary>
        /// Window title.
        /// </summary>
        public string Title
        {
            get => _title;

            set
            {
                _title = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Command to close window.
        /// </summary>
        public DelegateCommand CloseCommand { get; set; }

        /// <summary>
        /// Command to change window state.
        /// </summary>
        public DelegateCommand ChangeWindowStateCommand { get; set; }

        /// <summary>
        /// Command to minimize window.
        /// </summary>
        public DelegateCommand MinimizeCommand { get; set; }
    }
}
