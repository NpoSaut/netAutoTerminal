using System;
using System.Runtime.Serialization;

namespace FirmwarePacker.Project.Serializers.Exceptions
{
    /// <Summary>Ошибка при десериализации проекта</Summary>
    [Serializable]
    public abstract class ProjectDeserializationException : ApplicationException
    {
        protected ProjectDeserializationException(string message) : base(message) { }
        protected ProjectDeserializationException(string message, Exception inner) : base(message, inner) { }

        protected ProjectDeserializationException(
            SerializationInfo info,
            StreamingContext context) : base(info, context) { }
    }
}
