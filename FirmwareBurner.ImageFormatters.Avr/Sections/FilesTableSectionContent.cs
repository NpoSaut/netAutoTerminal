using System;
using System.Collections.Generic;
using FirmwareBurner.ImageFormatters.Binary;
using FirmwareBurner.ImageFormatters.Binary.DataSections;
using FirmwareBurner.Imaging.Binary.Buffers;

namespace FirmwareBurner.ImageFormatters.Avr.Sections
{
    internal class FilesTableSectionContent : ISectionContent<AvrMemoryKind>
    {
        private readonly IChecksumProvider _checksumProvider;

        public FilesTableSectionContent(IChecksumProvider ChecksumProvider, ICollection<BinaryImageFile<AvrMemoryKind>> Files)
        {
            this.Files = Files;
            _checksumProvider = ChecksumProvider;
        }

        public ICollection<BinaryImageFile<AvrMemoryKind>> Files { get; set; }

        public void WriteTo(IWriter Writer)
        {
            Writer.WriteByte((byte)Files.Count);
            foreach (var file in Files)
            {
                Int32 address = file.Placement.Address | GetMemoryAddressOffset(file.Placement.MemoryKind);
                Writer.WriteInt32(address);
                Writer.WriteInt32(file.Content.Length);
                Writer.WriteInt32(_checksumProvider.GetChecksum(file.Content));
            }
        }

        /// <summary>Вычисляет отступ адреса для указанного типа памяти</summary>
        /// <param name="AvrMemory">Тип памяти, в котором будет располагаться файл</param>
        private int GetMemoryAddressOffset(AvrMemoryKind AvrMemory)
        {
            int bit = AvrMemory == AvrMemoryKind.Flash ? 0 : 1;
            return bit << 24;
        }
    }
}
