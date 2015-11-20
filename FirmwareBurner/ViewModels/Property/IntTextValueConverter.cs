using System;
using System.Collections.Generic;
using System.Linq;

namespace FirmwareBurner.ViewModels.Property
{
    public class IntTextValueConverter : ITextValueConverter<int>
    {
        public IEnumerable<string> TryConvert(string Text, out int Value)
        {
            return Int32.TryParse(Text, out Value)
                       ? Enumerable.Empty<string>()
                       : new[] { "¬ведЄнные данные не €вл€ютс€ числом." };
        }

        public string GetText(int Value) { return Value.ToString(); }
    }
}
