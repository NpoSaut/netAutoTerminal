using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.IO;

namespace FirmwareBurner.BootloaderFormat
{
    class PropertyFormat : Format
    {
        public String PropertyName { get; private set; }
        public int FieldLength { get; private set; }

        public PropertyFormat(XElement XFormatPice)
            : base(XFormatPice)
        {
            this.PropertyName = XFormatPice.Name.LocalName;
            this.FieldLength = (int)XFormatPice.Attribute("Length");
        }

        public Byte[] Get(Object Source)
        {
            var st = Source.GetType();
            var p = st.GetProperty(PropertyName);
            if (p == null) return new Byte[FieldLength];

            var v = (Int64)p.GetValue(Source, new object[0]);
            return BitConverter.GetBytes(v).Take(FieldLength).ToArray();
        }

        public override void WriteTo(object Source, Stream output)
        {
            var buff = Get(Source);
            output.Write(buff, 0, buff.Length);
        }
    }
}
