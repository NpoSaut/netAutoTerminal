﻿using System;
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
            this.Components = Components.Select(c => new ValidateableFirmwareSetComponentViewModel(c, rule)).ToList();

            foreach (FirmwareSetComponentViewModel component in Components)
                component.SelectedFirmwareChanged += ComponentOnSelectedFirmwareChanged;
        }

        public List<ValidateableFirmwareSetComponentViewModel> Components { get; private set; }

        public event EventHandler SomethingChanged;

        private void ComponentOnSelectedFirmwareChanged(object Sender, EventArgs Args) { OnSomethingChanged(); }

        private void OnSomethingChanged()
        {
            EventHandler handler = SomethingChanged;
            if (handler != null) handler(this, EventArgs.Empty);
        }
    }
}
