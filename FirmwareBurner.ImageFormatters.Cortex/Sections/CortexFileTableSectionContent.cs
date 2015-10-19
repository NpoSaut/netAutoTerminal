using System;
using System.Collections.Generic;
using FirmwareBurner.ImageFormatters.Binary;
using FirmwareBurner.ImageFormatters.Binary.DataSections;
using FirmwareBurner.ImageFormatters.Cortex.Exceptions;
using FirmwareBurner.Imaging.Binary.Buffers;

namespace FirmwareBurner.ImageFormatters.Cortex.Sections
{
    internal class CortexFileTableSectionContent : ISectionContent<CortexMemoryKind>
    {
        private readonly ICollection<BinaryImageFile<CortexMemoryKind>> _files;

        public CortexFileTableSectionContent(ICollection<BinaryImageFile<CortexMemoryKind>> Files)
        {
            _files = Files;
        }

        public void WriteTo(IWriter Writer)
        {
            if (_files.Count > byte.MaxValue)
                throw new TooManyFilesException();
            Writer.WriteByte((Byte)_files.Count);
            foreach (var file in _files)
            {
                Writer.WriteBytes(EncodeFilePath(file.Placement.MemoryKind));
                Writer.WriteInt32(file.Placement.Address);
                Writer.WriteInt32(file.Content.Length);
            }
        }
    }
}
