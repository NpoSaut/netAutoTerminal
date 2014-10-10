using System.IO;
using System.Xml.Linq;

namespace FirmwareBurner.Formating
{
    internal abstract class FormatBase
    {
        protected FormatBase(XElement XFormat) { }
        public abstract void WriteTo(object Source, Stream output);
    }
}
