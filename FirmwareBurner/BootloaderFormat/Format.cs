using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Linq;

namespace FirmwareBurner.BootloaderFormat
{
    abstract class Format
    {
        protected Format(XElement XFormat) { }
        public abstract void WriteTo(object Source, Stream output);
    }
}
