using System;
using FirmwareBurner.Progress;
using FirmwareBurner.ViewModels.Bases;

namespace FirmwareBurner.ViewModels
{
    public class ProgressViewModel : ViewModelBase
    {
        private double _complete;
        private bool _isActive;
        private bool _isIntermediate;

        public ProgressViewModel(IProgressPublisher ProgressPublisher)
        {
            ProgressPublisher.Started += ProgressPublisherOnStarted;
            ProgressPublisher.Changed += ProgressPublisherOnChanged;
            ProgressPublisher.Compleated += ProgressPublisherOnCompleated;
        }

        #region Обработка событий IProgressPublisher

        private void ProgressPublisherOnStarted(object Sender, EventArgs Args)
        {
            IsActive = true;
            IsIntermediate = true;
        }

        private void ProgressPublisherOnChanged(object Sender, EventArgs Args)
        {
            var progress = (IProgressPublisher)Sender;
            IsActive = true;
            IsIntermediate = progress.IsIntermediate;
            Complete = progress.Progress;
        }

        private void ProgressPublisherOnCompleated(object Sender, EventArgs Args)
        {
            IsActive = false;
            IsIntermediate = false;
            Complete = 1.0;
            OnCompleated();
        }

        #endregion

        #region Реализация внешних свойст

        public Boolean IsActive
        {
            get { return _isActive; }
            private set
            {
                if (value.Equals(_isActive)) return;
                _isActive = value;
                RaisePropertyChanged(() => IsActive);
            }
        }

        public Double Complete
        {
            get { return _complete; }
            private set
            {
                if (value.Equals(_complete)) return;
                _complete = value;
                RaisePropertyChanged(() => Complete);
            }
        }

        public Boolean IsIntermediate
        {
            get { return _isIntermediate; }
            private set
            {
                if (value.Equals(_isIntermediate)) return;
                _isIntermediate = value;
                RaisePropertyChanged(() => IsIntermediate);
            }
        }

        public event EventHandler Compleated;

        protected virtual void OnCompleated()
        {
            EventHandler handler = Compleated;
            if (handler != null) handler(this, EventArgs.Empty);
        }

        public void Reset()
        {
            IsIntermediate = false;
            Complete = 0;
            IsActive = false;
        }

        #endregion
    }
}
