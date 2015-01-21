using System;
using System.Collections.Generic;
using System.Linq;
using FirmwareBurner.Validation.Rules;
using FirmwareBurner.ViewModels.Bases;
using FirmwareBurner.ViewModels.Property;

namespace FirmwareBurner.ViewModels
{
    public class FirmwareSetConstructorViewModel : ViewModelBase
    {
        public FirmwareSetConstructorViewModel(ICollection<FirmwareSetComponentViewModel> Components)
        {
            var rule = new HaveFirmwareValidationRule();
            this.Components = Components.Select(c => new ValidateableViewModelShell<FirmwareSetComponentViewModel>(c, rule)).ToList();

            foreach (FirmwareSetComponentViewModel component in Components)
                component.SetChanged += ComponentOnSetChanged;
        }

        public ICollection<ValidateableViewModelShell<FirmwareSetComponentViewModel>> Components { get; private set; }

        public event EventHandler SomethingChanged;

        private void ComponentOnSetChanged(object Sender, EventArgs Args) { OnSomethingChanged(); }

        private void OnSomethingChanged()
        {
            EventHandler handler = SomethingChanged;
            if (handler != null) handler(this, EventArgs.Empty);
        }
    }
}
