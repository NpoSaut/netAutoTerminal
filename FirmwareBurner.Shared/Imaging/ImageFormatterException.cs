using System;
using System.Runtime.Serialization;

namespace FirmwareBurner.Imaging
{
    /// <Summary>Ошибка при формировании образа прошивки</Summary>
    [Serializable]
    public class ImageFormatterException : ApplicationException
    {
        public ImageFormatterException() : base("Ошибка при формировании образа прошивки") { }
        public ImageFormatterException(Exception inner) : base("Ошибка при формировании образа прошивки", inner) { }
        public ImageFormatterException(string message) : base(message) { }
        public ImageFormatterException(string message, Exception inner) : base(message, inner) { }

        protected ImageFormatterException(
            SerializationInfo info,
            StreamingContext context) : base(info, context) { }
    }
}
