using System;
using System.ComponentModel;

namespace FirmwareBurner.ViewModels.Bases
{
    public class ViewModelBase : INotifyPropertyChanged
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
