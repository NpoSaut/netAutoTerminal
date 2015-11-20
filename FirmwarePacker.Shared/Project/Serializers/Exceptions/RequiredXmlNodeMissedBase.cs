using System;
using System.Linq;
using System.Runtime.Serialization;
using System.Xml.Linq;

namespace FirmwarePacker.Project.Serializers.Exceptions
{
    public abstract class RequiredXmlNodeMissedBase : ProjectDeserializationException
    {
        protected RequiredXmlNodeMissedBase(string message) : base(message) { }
        protected RequiredXmlNodeMissedBase(string message, Exception inner) : base(message, inner) { }
        protected RequiredXmlNodeMissedBase(SerializationInfo info, StreamingContext context) : base(info, context) { }

        protected static string GetElementPresentation(XElement Element)
        {
            return string.Format("<{0}...",
                                 string.Join(" ",
                                             new[] { Element.Name.LocalName }.Concat(
                                                 Element.Attributes()
                                                        .Take(3)
                                                        .Select(a => string.Format("{0}=\"{1}\"", a.Name.LocalName, a.Value)))));
        }
    }
}
