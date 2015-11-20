using System;
using System.Collections.Generic;
using System.Globalization;

namespace FirmwareBurner.ViewModels.Property
{
    public class DateTimeTextValueConverter : ITextValueConverter<DateTime>
    {
        private readonly string _formatString;
        public DateTimeTextValueConverter(string FormatString) { _formatString = FormatString; }

        public IEnumerable<string> TryConvert(string Text, out DateTime Value)
        {
            bool conversionSuccessed = DateTime.TryParseExact(Text, _formatString, CultureInfo.CurrentUICulture, DateTimeStyles.AllowWhiteSpaces, out Value);
            return conversionSuccessed ? new String[0] : new[] { string.Format("¬ведите дату в формате {0}", _formatString) };
        }

        public string GetText(DateTime Value) { return Value.ToString(_formatString); }
    }
}
