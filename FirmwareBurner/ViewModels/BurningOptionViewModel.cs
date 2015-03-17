using System;
using System.Windows.Input;
using FirmwareBurner.ViewModels.Bases;
using Microsoft.Practices.Prism.Commands;

namespace FirmwareBurner.ViewModels
{
    public class BurningOptionViewModel : ViewModelBase
    {
        public BurningOptionViewModel(string Name, int ChannelNumber)
        {
            this.ChannelNumber = ChannelNumber;
            this.Name = Name;
            Progress = new ProgressViewModel();
            BurnCommand = new DelegateCommand(OnActivated);
        }

        public String Name { get; private set; }
        public ICommand BurnCommand { get; private set; }
        public ProgressViewModel Progress { get; private set; }
        public int ChannelNumber { get; private set; }

        public event EventHandler Activated;
        public event EventHandler ErrorPulse;

        public void PulseError() { OnErrorPulse(); }

        protected void OnErrorPulse()
        {
            EventHandler handler = ErrorPulse;
            if (handler != null) handler(this, EventArgs.Empty);
        }

        private void OnActivated()
        {
            EventHandler handler = Activated;
            if (handler != null) handler(this, EventArgs.Empty);
        }
    }
}
