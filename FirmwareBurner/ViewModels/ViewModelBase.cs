using System;
using System.ComponentModel;
using System.Windows.Threading;

namespace FirmwareBurner.ViewModels
{
    public class ViewModelBase : DispatcherObject, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(String PropertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(PropertyName));
        }
    }

    public interface IDataCheck
    {
        bool Check();
    }
}
