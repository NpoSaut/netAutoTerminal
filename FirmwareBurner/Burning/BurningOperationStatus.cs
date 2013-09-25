using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FirmwareBurner.Burning
{
    public class BurningOperationStatus : IBurningOperationStatusReceiver
    {
        private double _Progress;
        /// <summary>Возникает каждый раз при изменении доли выполнения операции</summary>
        public event EventHandler ProgressChanged;
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

        /// <summary>Говорит о завершении процесса</summary>
        public bool IsFinished { get; private set; }
        /// <summary>Событие возникает при завершении операции</summary>
        public event EventHandler Finished;

        public void OnFinish()
        {
            if (Finished != null) Finished(this, new EventArgs());
        }
    }
}
