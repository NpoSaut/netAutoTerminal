using System;
using System.IO;

namespace FirmwareBurner.Imaging.Binary
{
    public interface IBinaryPropertiesTableFormatter
    {
        void AddPropertyValue(Stream DestinationStream, Byte PropertyKey, Int32 PropertyValue);
    }
}
