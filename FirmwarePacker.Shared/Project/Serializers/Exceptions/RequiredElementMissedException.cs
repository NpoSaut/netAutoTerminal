using System;
using System.Runtime.Serialization;
using System.Xml.Linq;

namespace FirmwarePacker.Project.Serializers.Exceptions
{
    /// <Summary>Пропущен необходимый XML-элемент</Summary>
    [Serializable]
    public class RequiredElementMissedException : RequiredXmlNodeMissedBase
    {
        public RequiredElementMissedException(string ElementName, XElement Element)
            : base(string.Format("В элементе {0} отсутствует необходимый элемент {1}", GetElementPresentation(Element), ElementName)) { }

        protected RequiredElementMissedException(
            SerializationInfo info,
            StreamingContext context) : base(info, context) { }
    }
}
