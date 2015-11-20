using System.Collections.Generic;
using System.Linq;
using FirmwareBurner.Validation;

namespace FirmwareBurner.ViewModels.Property
{
    public class ValidateableTextPropertyViewModel<TValue> : ValidateablePropertyViewModel<TValue>
    {
        private readonly ITextValueConverter<TValue> _converter;
        private string[] _valueConversionErrors;
        private string _valueText;

        public ValidateableTextPropertyViewModel(ITextValueConverter<TValue> Converter, params IValidationRule<TValue>[] ValidationRules)
            : base(ValidationRules)
        {
            _converter = Converter;
            _valueText = string.Empty;
            _valueConversionErrors = new string[0];
        }

        public ValidateableTextPropertyViewModel(TValue Value, ITextValueConverter<TValue> Converter, params IValidationRule<TValue>[] ValidationRules)
            : base(Value, ValidationRules)
        {
            _converter = Converter;
            _valueText = _converter.GetText(Value);
            _valueConversionErrors = new string[0];
        }

        public string ValueText
        {
            get { return _valueText; }
            set
            {
                _valueText = value;
                TValue val;
                _valueConversionErrors = _converter.TryConvert(_valueText, out val).ToArray();
                Value = val;
                RaisePropertyChanged(() => ValueText);
            }
        }

        public override IEnumerable<string> ValidationErrors
        {
            get
            {
                return _valueConversionErrors.Any()
                           ? _valueConversionErrors
                           : base.ValidationErrors;
            }
        }
    }
}
