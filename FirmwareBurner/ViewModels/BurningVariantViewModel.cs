using System;
using System.Windows.Input;
using FirmwareBurner.Burning;
using FirmwareBurner.ViewModels.Bases;
using Microsoft.Practices.Prism.Commands;

namespace FirmwareBurner.ViewModels
{
    /// <summary>Модель представления варианта прошивки устройства</summary>
    public class BurningVariantViewModel : ViewModelBase
    {
        private readonly IBurningReceipt _burningReceipt;

        public BurningVariantViewModel(IBurningReceipt BurningReceipt, bool IsDefault = false)
        {
            _burningReceipt = BurningReceipt;
            this.IsDefault = IsDefault;

            Name = BurningReceipt.Name;
            BurnCommand = new DelegateCommand(Burn);
        }

        /// <summary>Имя варианта прошивки</summary>
        public String Name { get; private set; }

        /// <summary>Признак того, что этот вариант является вариантом по-умолчанию</summary>
        public Boolean IsDefault { get; private set; }

        /// <summary>Команда на прошивку</summary>
        public ICommand BurnCommand { get; private set; }

        private void Burn() { OnActivated(new BurningVariantActivatedEventArgs(_burningReceipt)); }

        public event EventHandler<BurningVariantActivatedEventArgs> Activated;

        private void OnActivated(BurningVariantActivatedEventArgs E)
        {
            EventHandler<BurningVariantActivatedEventArgs> handler = Activated;
            if (handler != null) handler(this, E);
        }
    }

    public class BurningVariantActivatedEventArgs : EventArgs
    {
        public BurningVariantActivatedEventArgs(IBurningReceipt BurningReceipt) { this.BurningReceipt = BurningReceipt; }
        public IBurningReceipt BurningReceipt { get; private set; }
    }
}
