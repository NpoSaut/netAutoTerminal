using System;

namespace FirmwareBurner.Burning
{
    public class BurningOperationStatus : IBurningOperationStatusReceiver
    {
        private double _Progress;

        /// <summary>Говорит о завершении процесса</summary>
        public bool IsFinished { get; private set; }

        /// <summary>Доля выполнения</summary>
        public double Progress
        {
            get { return _Progress; }
            set
            {
                if (_Progress != value)
                {
                    _Progress = value;
                    if (ProgressChanged != null) ProgressChanged(this, new EventArgs());
                }
            }
        }

        public void OnFinish() { if (Finished != null) Finished(this, new EventArgs()); }

        /// <summary>Возникает каждый раз при изменении доли выполнения операции</summary>
        public event EventHandler ProgressChanged;

        /// <summary>Событие возникает при завершении операции</summary>
        public event EventHandler Finished;
    }
}
