﻿using System.Collections.Generic;
using System.Linq;
using FirmwareBurner.ImageFormatters.Binary.DataSections;
using FirmwareBurner.Imaging;
using FirmwareBurner.Imaging.Binary;
using FirmwareBurner.Imaging.Binary.Buffers;

namespace FirmwareBurner.ImageFormatters.Avr.Sections
{
    internal class PropertiesTableSectionContent : ISectionContent<AvrMemoryKind>
    {
        private readonly IPropertiesProvider _propertiesProvider;
        public PropertiesTableSectionContent(IPropertiesProvider PropertiesProvider) { _propertiesProvider = PropertiesProvider; }

        public void WriteTo(IWriter Writer)
        {
            List<ParamRecord> properties = _propertiesProvider.GetProperties().ToList();
            Writer.WriteByte((byte)properties.Count);
            foreach (ParamRecord property in properties)
            {
                Writer.WriteByte(property.Key);
                Writer.WriteInt32(property.Value);
            }
        }
    }
}
