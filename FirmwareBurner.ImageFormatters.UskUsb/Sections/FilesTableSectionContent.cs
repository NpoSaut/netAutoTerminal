using System;
using System.Collections.Generic;
using FirmwareBurner.ImageFormatters.Binary;
using FirmwareBurner.ImageFormatters.Binary.DataSections;
using FirmwareBurner.ImageFormatters.Cortex;
using FirmwareBurner.Imaging.Binary.Buffers;

namespace FirmwareBurner.ImageFormatters.UskUsb.Sections
{
    public class FilesTableSectionContent : ISectionContent<CortexMemoryKind>
    {
        private readonly IChecksumProvider _checksumProvider;

        public FilesTableSectionContent(IChecksumProvider ChecksumProvider, ICollection<BinaryImageFile<CortexMemoryKind>> Files)
        {
            _checksumProvider = ChecksumProvider;
            this.Files = Files;
        }

        public ICollection<BinaryImageFile<CortexMemoryKind>> Files { get; set; }

        public void WriteTo(IWriter Writer)
        {
            Writer.WriteByte((byte)Files.Count);
            foreach (var file in Files)
            {
                Int32 address = file.Placement.Address;
                Writer.WriteInt32(address);
                Writer.WriteInt32(file.Content.Length);
                Writer.WriteInt32(_checksumProvider.GetChecksum(file.Content));
            }
        }
    }
}
