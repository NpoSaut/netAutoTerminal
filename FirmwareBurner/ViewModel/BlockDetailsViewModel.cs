using System;

namespace FirmwareBurner.ViewModel
{
    public class BlockDetailsViewModel : ViewModelBase
    {
        private int _SerialNumber = 0;
        /// <summary>
        /// Серийный номер блока
        /// </summary>
        public int SerialNumber
        {
            get { return _SerialNumber; }
            set
            {
                if (_SerialNumber != value)
                {
                    _SerialNumber = value;
                    OnPropertyChanged("SerialNumber");
                }
            }
        }

        private DateTime _AssemblyDate = DateTime.Now;
        /// <summary>
        /// Дата сборки модуля
        /// </summary>
        public DateTime AssemblyDate
        {
            get { return _AssemblyDate; }
            set
            {
                if (_AssemblyDate != value)
                {
                    _AssemblyDate = value;
                    OnPropertyChanged("AssemblyDate");
                }
            }
        }
    }
}
