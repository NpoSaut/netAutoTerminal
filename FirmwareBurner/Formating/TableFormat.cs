using System.Collections;
using System.IO;
using System.Xml.Linq;

namespace FirmwareBurner.Formating
{
    internal class TableFormat : ElementFormat
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
            var seq = (IEnumerable)Source;
            foreach (object element in seq)
                base.WriteTo(element, output);
        }

        public override string ToString() { return string.Format("Table format"); }
    }
}
