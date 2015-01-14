using System;
using FirmwareBurner.Validation.Rules;
using FirmwareBurner.ViewModels.Bases;

namespace FirmwareBurner.ViewModels.Targeting
{
    public class BlockDetailsViewModel : ViewModelBase
    {
        public BlockDetailsViewModel() : this(0, DateTime.Now) { }

        public BlockDetailsViewModel(int SerialNumber, DateTime AssemblyDate)
        {
            this.SerialNumber = new ValidateablePropertyViewModel<int>(SerialNumber,
                new SerialNumberValidationRule());
            this.AssemblyDate = new ValidateablePropertyViewModel<DateTime>(AssemblyDate);
        }

        /// <summary>Серийный номер блока</summary>
        public ValidateablePropertyViewModel<int> SerialNumber { get; private set; }

        /// <summary>Дата сборки модуля</summary>
        public ValidateablePropertyViewModel<DateTime> AssemblyDate { get; private set; }
    }
}
