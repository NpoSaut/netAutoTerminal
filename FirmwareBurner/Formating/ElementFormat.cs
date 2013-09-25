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
            XAttribute AdressAttribute;
            if ((AdressAttribute = XFormat.Attribute("StartAdress")) != null)
                StartAdress = Convert.ToInt32(AdressAttribute.Value, 16);
            Getters =
                XFormat
                    .Elements()
                    .Select(e =>
                        e.HasElements ?
                        (FormatBase)new TableFormat(e) :
                        (FormatBase)new PropertyFormat(e))
                    //.Where(e => !e.HasElements)
                    //.Select(e => (FormatBase)new ElementFormat(e))
                    .ToList();
        }

        public override void WriteTo(object Source, Stream output)
        {
            if (StartAdress != null) output.Seek(StartAdress.Value, SeekOrigin.Begin);
            foreach (var getter in Getters)
                getter.WriteTo(Source, output);
        }

        public override string ToString()
        {
            return string.Format("Item format");
        }
    }
}
