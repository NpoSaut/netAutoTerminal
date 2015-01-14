﻿using System.Collections.Generic;
using FirmwareBurner.Validation;

namespace FirmwareBurner.ViewModels.Property
{
    public class ValidateableTextPropertyViewModel<TValue> : ValidateablePropertyViewModel<TValue>
    {
        private readonly ITextValueConverter<TValue> _converter;
        private bool _valueConverted;
        private string _valueText;

        public ValidateableTextPropertyViewModel(TValue Value, ITextValueConverter<TValue> Converter, params IValidationRule<TValue>[] ValidationRules)
            : base(Value, ValidationRules)
        {
            _converter = Converter;
            _valueText = _converter.GetText(Value);
        }

        public string ValueText
        {
            get { return _valueText; }
            set
            {
                _valueText = value;
                TValue val;
                _valueConverted = _converter.TryConvert(_valueText, out val);
                Value = val;
                RaisePropertyChanged(string.Empty);
            }
        }

        public override IEnumerable<string> ValidationErrors
        {
            get
            {
                return _valueConverted
                           ? base.ValidationErrors
                           : new[] { "Введённые данные не соответствуют формату" };
            }
        }
    }
}
