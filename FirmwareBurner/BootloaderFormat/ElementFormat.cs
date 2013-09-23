using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.IO;

namespace FirmwareBurner.BootloaderFormat
{
    class ElementFormat : Format
    {
        private IList<Format> Getters { get; set; }

        public ElementFormat(XElement XFormat)
            : base(XFormat)
        {
            Getters =
                XFormat
                    .Elements()
                    .Select(e =>
                        e.HasElements ?
                        (Format)new TableFormat(e) :
                        (Format)new PropertyFormat(e))
                    .ToList();
        }

        public override void WriteTo(object Source, Stream output)
        {
            foreach (var getter in Getters)
                getter.WriteTo(Source, output);
        }
    }
}
