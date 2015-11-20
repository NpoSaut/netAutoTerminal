using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FirmwareBurner.Annotations;
using FirmwareBurner.ImageFormatters.Binary;
using FirmwareBurner.ImageFormatters.Binary.DataSections;
using FirmwareBurner.ImageFormatters.Cortex.Exceptions;
using FirmwareBurner.Imaging.Binary.Buffers;

namespace FirmwareBurner.ImageFormatters.Cortex.Sections
{
    internal class CortexFileTableSectionContent : ISectionContent<CortexMemoryKind>
    {
        private static readonly Encoding _encoding = Encoding.GetEncoding(1251);
        private readonly IChecksumProvider _checksumProvider;
        private readonly ICollection<BinaryImageFile<CortexMemoryKind>> _files;
        private readonly IDictionary<string, CortexMemoryKind> _memoryKinds;

        public CortexFileTableSectionContent([NotNull] ICollection<BinaryImageFile<CortexMemoryKind>> Files,
                                             [NotNull] IDictionary<string, CortexMemoryKind> MemoryKinds,
                                             [NotNull] IChecksumProvider ChecksumProvider)
        {
            if (ChecksumProvider == null) throw new ArgumentNullException("ChecksumProvider");
            _files = Files;
            _memoryKinds = MemoryKinds;
            _checksumProvider = ChecksumProvider;
        }

        public void WriteTo(IWriter Writer)
        {
            if (_files.Count > byte.MaxValue)
                throw new TooManyFilesException();
            Writer.WriteByte((Byte)_files.Count);
            foreach (var file in _files)
            {
                Writer.WriteBytes(EncodeMemoryKind(file.Placement.MemoryKind));
                Writer.WriteInt32(file.Placement.Address);
                Writer.WriteInt32(file.Content.Length);
                Writer.WriteUInt16(_checksumProvider.GetChecksum(file.Content));
            }
        }

        private byte[] EncodeMemoryKind(CortexMemoryKind MemoryKind)
        {
            string nameString = _memoryKinds.Single(x => x.Value == MemoryKind).Key;
            return _encoding.GetBytes(nameString.PadRight(4, (char)0));
        }
    }
}
