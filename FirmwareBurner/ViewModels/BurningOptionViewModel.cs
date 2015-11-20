using System;
using System.Windows.Input;
using AsyncOperations.OperationTokens;
using FirmwareBurner.ViewModels.Bases;
using Microsoft.Practices.Prism.Commands;

namespace FirmwareBurner.ViewModels
{
    public enum BurningOperationStatus
    {
        Ready,
        InProgress,
        Compleated
    }

    public class BurningOptionViewModel : ViewModelBase
    {
        private IAsyncOperationToken _operation;
        private ProgressViewModel _progress;
        private BurningOperationStatus _status;

        public BurningOptionViewModel(string Name, int ChannelNumber)
        {
            this.ChannelNumber = ChannelNumber;
            this.Name = Name;
            BurnCommand = new DelegateCommand(OnActivated);
            Status = BurningOperationStatus.Ready;
        }

        public BurningOperationStatus Status
        {
            get { return _status; }
            private set
            {
                if (value == _status) return;
                _status = value;
                RaisePropertyChanged("Status");
            }
        }

        public String Name { get; private set; }
        public ICommand BurnCommand { get; private set; }

        public ProgressViewModel Progress
        {
            get { return _progress; }
            private set
            {
                if (Equals(value, _progress)) return;
                _progress = value;
                RaisePropertyChanged("Progress");
            }
        }

        public int ChannelNumber { get; private set; }

        public event EventHandler Activated;

        private void OnActivated()
        {
            EventHandler handler = Activated;
            if (handler != null) handler(this, EventArgs.Empty);
        }

        public void ProcessAsyncOperation(IAsyncOperationToken OperationToken)
        {
            if (_operation != null)
                UnsubscribeFromOperation(_operation);
            _operation = OperationToken;
            Status = BurningOperationStatus.InProgress;
            SubscribeToOperation(_operation);
        }

        public void ResetOperation()
        {
            if (_operation != null)
            {
                if (_operation.CanAbort)
                    _operation.Abort();
                UnsubscribeFromOperation(_operation);
                _operation = null;
            }
            Status = BurningOperationStatus.Ready;
        }

        private void UnsubscribeFromOperation(IAsyncOperationToken Operation) { Operation.Compleated -= OperationOnCompleated; }

        private void SubscribeToOperation(IAsyncOperationToken Operation)
        {
            Progress = new ProgressViewModel(Operation.Progress);
            Operation.Compleated += OperationOnCompleated;
        }

        private void OperationOnCompleated(object Sender, AsyncOperationCompleatedEventArgs OperationCompleatedEventArgs)
        {
            Status = OperationCompleatedEventArgs.Status == AsyncOperationCompleatingStatus.Success
                         ? BurningOperationStatus.Compleated
                         : BurningOperationStatus.Ready;
        }
    }
}
