using System;
using FirmwareBurner.Validation.Rules;
using FirmwareBurner.ViewModels.Bases;
using FirmwareBurner.ViewModels.Property;

namespace FirmwareBurner.ViewModels.Targeting
{
    public class BlockDetailsViewModel : ViewModelBase
    {
        public BlockDetailsViewModel() : this(0, DateTime.Now) { }

        public BlockDetailsViewModel(int SerialNumber, DateTime AssemblyDate)
        {
            this.SerialNumber = new ValidateableTextPropertyViewModel<int>(SerialNumber,
                new IntTextValueConverter(),
                new SerialNumberValidationRule());
            
            this.AssemblyDate = new ValidateablePropertyViewModel<DateTime>(AssemblyDate);
        }

        /// <summary>Серийный номер блока</summary>
        public ValidateableTextPropertyViewModel<int> SerialNumber { get; private set; }

        /// <summary>Дата сборки модуля</summary>
        public ValidateablePropertyViewModel<DateTime> AssemblyDate { get; private set; }
    }
}
