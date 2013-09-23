using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Globalization;
using System.Reflection;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Markup;

namespace Converters
{
    [ValueConversion(typeof(Version), typeof(String))]
    public class VersionToStringConverter : ConverterBase<VersionToStringConverter>
    {
        public override object Convert(object Value, Type TargetType, object Parameter, CultureInfo Culture)
        {
            var v = Value as Version;
            if (v == null) throw new ArgumentException();
            return v.ToString();
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var v = new Version();
            if (Version.TryParse(value as String, out v)) return v;
            else return new Version();
        }
    }

    public abstract class ConverterBase<T> : MarkupExtension, IValueConverter
        where T : class, new()
    {
        /// <summary>
        /// Must be implemented in inheritor.
        /// </summary>
        public abstract object Convert(object value, Type targetType, object parameter, CultureInfo culture);

        /// <summary>
        /// Override if needed.
        /// </summary>
        public virtual object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #region MarkupExtension members

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            //if (_converter == null)
            //    _converter = new T();
            //return _converter;
            return this;
        }

        protected static T _converter = new T();

        #endregion
    }
}