using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.IO;

namespace FirmwareBurner.Formating
{
    class ElementFormat : FormatBase
    {
        public int? StartAdress { get; set; }
        private IList<FormatBase> Getters { get; set; }

        public ElementFormat(XElement XFormat)
            : base(XFormat)
        {
            StartAdress = (int?)XFormat.Attribute("StartAdress");
            Getters =
                XFormat
                    .Elements()
                    .Select(e =>
                        e.HasElements ?
                        (FormatBase)new TableFormat(e) :
                        (FormatBase)new PropertyFormat(e))
                    .ToList();
        }

        public override void WriteTo(object Source, Stream output)
        {
            if (StartAdress != null) output.Seek(StartAdress.Value, SeekOrigin.Begin);
            foreach (var getter in Getters)
                getter.WriteTo(Source, output);
        }
    }
}
