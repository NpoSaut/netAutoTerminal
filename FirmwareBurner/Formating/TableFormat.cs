using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.IO;

namespace FirmwareBurner.Formating
{
    class TableFormat : ElementFormat
    {
        //public ElementFormat ItemFormat { get; private set; }

        public TableFormat(XElement XFormat)
            : base(XFormat)
        {
            //ItemFormat = new ElementFormat(XFormat.Elements().First(e => e.HasElements));
            //ItemFormat = new ElementFormat(XFormat);    // TODO: Почему вдруг?
        }

        public override void WriteTo(object Source, Stream output)
        {
            //base.WriteTo(Source, output);
            var seq = (System.Collections.IEnumerable)Source;
            foreach (var element in seq)
                base.WriteTo(element, output);
        }
        public override string ToString()
        {
            return string.Format("Table format");
        }
    }
}
