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
        private TValue _value;

        public ValidateablePropertyViewModel(TValue Value, params IValidationRule<TValue>[] ValidationRules)
        {
            _value = Value;
            _validationRules = ValidationRules;
        }

        public TValue Value
        {
            get { return _value; }
            set
            {
                _value = value;
                RaisePropertyChanged(string.Empty);
            }
        }

        public bool IsValid
        {
            get { return !ValidationErrors.Any(); }
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
