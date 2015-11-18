using System;
using System.Runtime.Serialization;
using System.Xml.Linq;

namespace FirmwarePacker.Project.Serializers.Exceptions
{
    /// <Summary>Неверный формат значения атрибута</Summary>
    [Serializable]
    public class BadAttributeValueException : RequiredXmlNodeMissedBase
    {
        public BadAttributeValueException(XAttribute Attribute, XElement Element, Exception changeFormatException)
            : base(
                string.Format("Значение \"{0}\" атрибута \"{1}\" в элементе \"{2}\" имеет неверный формат", Attribute.Value, Attribute.Name.LocalName,
                              GetElementPresentation(Element)), changeFormatException) { }

        protected BadAttributeValueException(
            SerializationInfo info,
            StreamingContext context) : base(info, context) { }
    }
}
