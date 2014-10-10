using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace FirmwareBurner.Formating
{
    internal class ElementFormat : FormatBase
    {
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
                            e.HasElements
                                ? (FormatBase)new TableFormat(e)
                                : (FormatBase)new PropertyFormat(e))
                    //.Where(e => !e.HasElements)
                    //.Select(e => (FormatBase)new ElementFormat(e))
                    .ToList();
        }

        public int? StartAdress { get; set; }
        private IList<FormatBase> Getters { get; set; }

        public override void WriteTo(object Source, Stream output)
        {
            if (StartAdress != null) output.Seek(StartAdress.Value, SeekOrigin.Begin);
            foreach (FormatBase getter in Getters)
                getter.WriteTo(Source, output);
        }

        public override string ToString() { return string.Format("Item format"); }
    }
}
