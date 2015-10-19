using System.Collections.Generic;
using System.Linq;
using FirmwareBurner.ImageFormatters.Binary.DataSections;
using FirmwareBurner.ImageFormatters.Cortex.Exceptions;
using FirmwareBurner.Imaging;
using FirmwareBurner.Imaging.Binary;
using FirmwareBurner.Imaging.Binary.Buffers;

namespace FirmwareBurner.ImageFormatters.Cortex.Sections
{
    public class CortexPropertiesSectionContent : ISectionContent<CortexMemoryKind>
    {
        private readonly IPropertiesProvider _propertiesProvider;
        public CortexPropertiesSectionContent(IPropertiesProvider PropertiesProvider) { _propertiesProvider = PropertiesProvider; }

        public void WriteTo(IWriter Writer)
        {
            List<ParamRecord> properties = _propertiesProvider.GetProperties().ToList();
            if (properties.Count > byte.MaxValue)
                throw new TooManyPropertiesException();
            Writer.WriteByte((byte)properties.Count);
            foreach (ParamRecord property in properties)
            {
                Writer.WriteByte(property.Key);
                Writer.WriteInt32(property.Value);
            }
        }
    }
}