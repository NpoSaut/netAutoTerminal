using System;
using System.Runtime.Serialization;
using System.Xml.Linq;

namespace FirmwarePacker.Project.Serializers.Exceptions
{
    /// <Summary>Пропущен необходимый атрибут</Summary>
    [Serializable]
    public class RequiredAttributeMissed : RequiredXmlNodeMissedBase
    {
        private readonly string _attributeName;
        private readonly XElement _element;

        public RequiredAttributeMissed(string AttributeName, XElement Element)
            : base(string.Format("Пропущен необходимый атрибут {0} в элементе {1}", AttributeName, GetElementPresentation(Element)))
        {
            _attributeName = AttributeName;
            _element = Element;
        }

        protected RequiredAttributeMissed(
            SerializationInfo info,
            StreamingContext context) : base(info, context) { }
    }
}
