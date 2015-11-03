using System;
using System.Runtime.Serialization;

namespace FirmwareBurner.Imaging
{
    /// <Summary>Burner не умеет создавать образы, удовлетворяющие требованиям этой версии прошивки</Summary>
    [Serializable]
    public class ImageFormatterNotFoundException : ImageFormatterException
    {
        public ImageFormatterNotFoundException() : base("Burner не умеет создавать образы, удовлетворяющие требованиям этой версии прошивки") { }

        protected ImageFormatterNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
