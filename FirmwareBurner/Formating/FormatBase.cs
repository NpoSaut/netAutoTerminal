using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Linq;

namespace FirmwareBurner.Formating
{
    abstract class FormatBase
    {
        protected FormatBase(XElement XFormat) { }
        public abstract void WriteTo(object Source, Stream output);
    }
}
