using System;
using System.IO;

namespace FirmwareBurner.Models.Images.Binary
{
    public interface IBinaryPropertiesTableFormatter
    {
        void AddPropertyValue(Stream DestinationStream, Byte PropertyKey, Int32 PropertyValue);
    }
}
