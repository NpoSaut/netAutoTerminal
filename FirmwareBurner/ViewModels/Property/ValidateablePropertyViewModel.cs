using System;
using System.Collections.Generic;
using System.Linq;
using FirmwareBurner.Validation;
using FirmwareBurner.ViewModels.Bases;

namespace FirmwareBurner.ViewModels.Property
{
    public class ValidateablePropertyViewModel<TValue> : ViewModelBase
    {
        private readonly IList<IValidationRule<TValue>> _validationRules;
        private bool _initialized;
        private TValue _value;

        public ValidateablePropertyViewModel(params IValidationRule<TValue>[] ValidationRules)
        {
            _initialized = false;
            _validationRules = ValidationRules;
        }

        public ValidateablePropertyViewModel(TValue Value, params IValidationRule<TValue>[] ValidationRules) : this(ValidationRules) { this.Value = Value; }

        public TValue Value
        {
            get { return _value; }
            set
            {
                _value = value;
                _initialized = true;
                RaisePropertyChanged(string.Empty);
            }
        }

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
            get { return _validationRules.SelectMany(rule => rule.GetValidationErrors(Value)); }
        }

        public string ValidationErrorsText
        {
            get { return string.Join(Environment.NewLine, ValidationErrors); }
        }
    }
}
