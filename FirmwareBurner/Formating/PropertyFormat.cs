using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;

namespace FirmwareBurner.Formating
{
    internal class PropertyFormat : FormatBase
    {
        public PropertyFormat(XElement XFormatPice)
            : base(XFormatPice)
        {
            PropertyName = XFormatPice.Name.LocalName;
            FieldLength = (int)XFormatPice.Attribute("Length");
        }

        public String PropertyName { get; private set; }
        public int FieldLength { get; private set; }

        public Byte[] Get(Object Source)
        {
            Type st = Source.GetType();
            PropertyInfo p = st.GetProperty(PropertyName);
            if (p == null) return new Byte[FieldLength];

            long v = Convert.ToInt64(p.GetValue(Source, new object[0]));
            return BitConverter.GetBytes(v).Take(FieldLength).ToArray();
        }

        public override void WriteTo(object Source, Stream output)
        {
            byte[] buff = Get(Source);
            output.Write(buff, 0, buff.Length);
        }

        public override string ToString() { return string.Format("Property format: {0}", PropertyName); }
    }
}
