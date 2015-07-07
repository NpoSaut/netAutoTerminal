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
            this.SerialNumber = new ValidateableTextPropertyViewModel<int>(new IntTextValueConverter()
                                                                               .PreValidate(new SerialNumberLengthValidationRule()),
                                                                           new BoundsValidator<int>(1, 99999));

            this.AssemblyDate = new ValidateableTextPropertyViewModel<DateTime>(AssemblyDate,
                                                                                new DateTimeTextValueConverter("M.yyyy"),
                                                                                new AssemblyDateValidationRule());
        }

        /// <summary>Серийный номер блока</summary>
        public ValidateableTextPropertyViewModel<int> SerialNumber { get; private set; }

        /// <summary>Дата сборки модуля</summary>
        public ValidateableTextPropertyViewModel<DateTime> AssemblyDate { get; private set; }
    }
}
