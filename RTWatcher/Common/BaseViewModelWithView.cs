using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Core.Common;

namespace RTWatcher.Common
{
    public class BaseViewModelWithView : BaseViewModel
    {
        private Window _currentView;
        protected Window CurrentView => _currentView ?? (_currentView = FindView(this));

        private Window FindView<T>(T model)
        {
            var dispatcher = Application.Current.Dispatcher;
            if (!dispatcher.CheckAccess())
            {
                return dispatcher.Invoke(() => FindView(this));
            }

            var enumerator = Application.Current.Windows.GetEnumerator();
            while (enumerator.MoveNext())
            {
                if (enumerator.Current is Window window && model.Equals(window.DataContext))
                {
                    return window;
                }
            }

            throw new Exception("View не найдена.");
        }
    }
}
