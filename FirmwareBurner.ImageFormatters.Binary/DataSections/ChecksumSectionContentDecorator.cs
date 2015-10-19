using System;
using System.IO;
using FirmwareBurner.Imaging.Binary.Buffers;

namespace FirmwareBurner.ImageFormatters.Binary.DataSections
{
    public enum RelativePosition
    {
        Leading,
        Enclosing
    }

    public class ChecksumSectionContentDecorator<TMemoryKind> : ISectionContent<TMemoryKind>
    {
        private readonly RelativePosition _checksumPosition;
        private readonly IChecksumProvider _checksumProvider;
        private readonly ISectionContent<TMemoryKind> _core;

        public ChecksumSectionContentDecorator(RelativePosition ChecksumPosition, IChecksumProvider ChecksumProvider, ISectionContent<TMemoryKind> Core)
        {
            _checksumProvider = ChecksumProvider;
            _checksumPosition = ChecksumPosition;
            _core = Core;
        }

        public void WriteTo(IWriter Writer)
        {
            var internalWriter = new MemorizedWriter();
            _core.WriteTo(internalWriter);

            byte[] data = internalWriter.Data;
            ushort checksum = _checksumProvider.GetChecksum(data);

            switch (_checksumPosition)
            {
                case RelativePosition.Leading:
                    Writer.WriteUInt16(checksum);
                    Writer.WriteBytes(data);
                    break;
                case RelativePosition.Enclosing:
                    Writer.WriteBytes(data);
                    Writer.WriteUInt16(checksum);
                    break;
            }
        }

        private class MemorizedWriter : WriterBase
        {
            private readonly MemoryStream _stream = new MemoryStream();

            public Byte[] Data
            {
                get { return _stream.ToArray(); }
            }

            public override void WriteBytes(params byte[] Bytes) { _stream.Write(Bytes, 0, Bytes.Length); }
        }
    }
}
