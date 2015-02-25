using System;
using System.Globalization;

namespace FirmwareBurner.ViewModels.Property
{
    public interface ITextValueConverter<TValue>
    {
        bool TryConvert(string Text, out TValue Value);
        string GetText(TValue Value);
    }

    public class DateTimeTextValueConverter : ITextValueConverter<DateTime>
    {
        private readonly string _formatString;
        public DateTimeTextValueConverter(string FormatString) { _formatString = FormatString; }

        public bool TryConvert(string Text, out DateTime Value)
        {
            return DateTime.TryParseExact(Text, _formatString, CultureInfo.CurrentUICulture, DateTimeStyles.AllowWhiteSpaces, out Value);
        }

        public string GetText(DateTime Value) { return Value.ToString(_formatString); }
    }
}
