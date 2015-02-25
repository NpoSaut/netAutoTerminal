using System;

namespace FirmwareBurner.ViewModels.Property
{
    public class IntTextValueConverter : ITextValueConverter<int>
    {
        public bool TryConvert(string Text, out int Value) { return Int32.TryParse(Text, out Value); }
        public string GetText(int Value) { return Value.ToString(); }
    }
}