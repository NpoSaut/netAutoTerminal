using System;
using FirmwareBurner.Progress;
using FirmwareBurner.ViewModels.Bases;

namespace FirmwareBurner.ViewModels
{
    public class ProgressViewModel : ViewModelBase, IProgressToken
    {
        private double _complete;
        private bool _isActive;
        private bool _isIntermediate;

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

        #region Реализация IProgressToken

        void IProgressToken.Start()
        {
            IsActive = true;
            IsIntermediate = true;
        }

        void IProgressToken.SetProgress(double Progress)
        {
            IsActive = true;
            IsIntermediate = false;
            Complete = Progress;
        }

        void IProgressToken.SetToIntermediate()
        {
            IsActive = true;
            IsIntermediate = true;
        }

        void IProgressToken.OnCompleated()
        {
            IsActive = false;
            IsIntermediate = false;
            Complete = 1.0;
            OnCompleated();
        }

        #endregion
    }
}
