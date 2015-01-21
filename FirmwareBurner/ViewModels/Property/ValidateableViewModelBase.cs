using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using FirmwareBurner.Validation;
using FirmwareBurner.ViewModels.Bases;

namespace FirmwareBurner.ViewModels.Property
{
    public abstract class ValidateableViewModelBase<TValue> : ViewModelBase, IValidateable
    {
        private readonly IList<IValidationRule<TValue>> _validationRules;
        private bool _initialized;

        public ValidateableViewModelBase(params IValidationRule<TValue>[] ValidationRules)
        {
            _initialized = false;
            _validationRules = ValidationRules;
        }

        protected abstract TValue ValidateableValue { get; }

        public bool IsInitialized
        {
            get { return _initialized; }
        }

        public bool IsValid
        {
            get { return IsInitialized && !ValidationErrors.Any(); }
        }

        public virtual IEnumerable<string> ValidationErrors
        {
            get { return _validationRules.SelectMany(rule => rule.GetValidationErrors(ValidateableValue)); }
        }

        public string ValidationErrorsText
        {
            get { return string.Join(Environment.NewLine, ValidationErrors); }
        }

        protected void OnValidateableValueChanged()
        {
            _initialized = true;
            RaisePropertyChanged(() => IsValid);
            RaisePropertyChanged(() => IsInitialized);
            RaisePropertyChanged(() => ValidationErrors);
            RaisePropertyChanged(() => ValidationErrorsText);
        }
    }

    public class ValidateableViewModelShell<TValue> : ValidateableViewModelBase<TValue> where TValue : INotifyPropertyChanged
    {
        public ValidateableViewModelShell(TValue Value, params IValidationRule<TValue>[] ValidationRules) : base(ValidationRules)
        {
            this.Value = Value;
            Value.PropertyChanged += ValueOnPropertyChanged;
        }

        public TValue Value { get; private set; }

        protected override TValue ValidateableValue
        {
            get { return Value; }
        }

        private void ValueOnPropertyChanged(object Sender, PropertyChangedEventArgs ChangedEventArgs) { OnValidateableValueChanged(); }
    }
}
