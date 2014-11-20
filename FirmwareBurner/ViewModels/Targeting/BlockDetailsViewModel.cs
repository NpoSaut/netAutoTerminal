using System;
using FirmwareBurner.ViewModels.Bases;

namespace FirmwareBurner.ViewModels.Targeting
{
    public class BlockDetailsViewModel : ViewModelBase
    {
        private DateTime _assemblyDate = DateTime.Now;
        private int _serialNumber;

        /// <summary>Серийный номер блока</summary>
        public int SerialNumber
        {
            get { return _serialNumber; }
            set
            {
                if (_serialNumber != value)
                {
                    _serialNumber = value;
                    RaisePropertyChanged("SerialNumber");
                }
            }
        }

        /// <summary>Дата сборки модуля</summary>
        public DateTime AssemblyDate
        {
            get { return _assemblyDate; }
            set
            {
                if (_assemblyDate != value)
                {
                    _assemblyDate = value;
                    RaisePropertyChanged("AssemblyDate");
                }
            }
        }
    }
}
