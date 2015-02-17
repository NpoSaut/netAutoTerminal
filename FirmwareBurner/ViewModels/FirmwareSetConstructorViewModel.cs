using System;
using System.Collections.Generic;
using FirmwareBurner.ViewModels.Bases;

namespace FirmwareBurner.ViewModels
{
    public class FirmwareSetConstructorViewModel : ViewModelBase
    {
        public FirmwareSetConstructorViewModel(ICollection<FirmwareSetComponentViewModel> Components)
        {
            this.Components = Components;
            foreach (FirmwareSetComponentViewModel component in Components)
                component.SelectedFirmwareChanged += ComponentOnSelectedFirmwareChanged;
        }

        public ICollection<FirmwareSetComponentViewModel> Components { get; private set; }

        public event EventHandler SomethingChanged;

        private void ComponentOnSelectedFirmwareChanged(object Sender, EventArgs Args) { OnSomethingChanged(); }

        private void OnSomethingChanged()
        {
            EventHandler handler = SomethingChanged;
            if (handler != null) handler(this, EventArgs.Empty);
        }
    }
}
