using System;
using FirmwareBurner.Burning;
using FirmwareBurner.ViewModels.Bases;

namespace FirmwareBurner.ViewModels
{
    /// <summary>Модель представления варианта прошивки устройства</summary>
    public class BurningMethodViewModel : ViewModelBase
    {
        public BurningMethodViewModel(string Name, IBurningReceipt Receipt)
        {
            this.Receipt = Receipt;
            this.Name = Name;
        }

        /// <summary>Имя варианта прошивки</summary>
        public String Name { get; private set; }

        /// <summary>Рецепт прошивки</summary>
        public IBurningReceipt Receipt { get; private set; }
    }
}
