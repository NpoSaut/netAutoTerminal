using System.Collections.Generic;
using System.Linq;
using FirmwareBurner.Validation;

namespace FirmwareBurner.ViewModels.Property
{
    public class TextPrevalidatorDecorator<T> : ITextValueConverter<T>
    {
        private readonly ITextValueConverter<T> _coreConverter;
        private readonly IValidationRule<string>[] _validationRules;

        public TextPrevalidatorDecorator(ITextValueConverter<T> CoreConverter, params IValidationRule<string>[] ValidationRules)
        {
            _coreConverter = CoreConverter;
            _validationRules = ValidationRules;
        }

        public IEnumerable<string> TryConvert(string Text, out T Value)
        {
            return _coreConverter.TryConvert(Text, out Value)
                                 .Concat(_validationRules.SelectMany(rule => rule.GetValidationErrors(Text)));
        }

        public string GetText(T Value) { return _coreConverter.GetText(Value); }
    }

    public static class TextPrevalidatorDecoratorHelper
    {
        public static ITextValueConverter<T> PreValidate<T>(this ITextValueConverter<T> Converter, params IValidationRule<string>[] ValidationRules)
        {
            return new TextPrevalidatorDecorator<T>(Converter, ValidationRules);
        }
    }
}
