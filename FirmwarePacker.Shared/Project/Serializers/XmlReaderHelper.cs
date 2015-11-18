using System;
using System.Xml.Linq;
using FirmwarePacker.Project.Serializers.Exceptions;
using FirmwarePacking.Annotations;

namespace FirmwarePacker.Project.Serializers
{
    internal static class XmlReaderHelper
    {
        [NotNull]
        public static TValue GetAttribute<TValue>(this XElement Element, string AttributeName)
        {
            XAttribute attribute = Element.Attribute(AttributeName);
            if (attribute == null)
                throw new RequiredAttributeMissed(AttributeName, Element);
            TValue value;
            try
            {
                value = (TValue)Convert.ChangeType(attribute.Value, typeof (TValue));
            }
            catch (Exception exception)
            {
                throw new BadAttributeValueException(attribute, Element, exception);
            }
            return value;
        }

        [NotNull]
        public static XElement GetElement(this XElement Element, string ElementName)
        {
            XElement element = Element.Element(ElementName);
            if (element == null)
                throw new RequiredAttributeMissed(ElementName, Element);
            return element;
        }
    }
}
