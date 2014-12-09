using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FirmwareBurner.Imaging.Binary.Buffers
{
    public class SegmentedBuffer : IBuffer
    {
        private readonly IList<BufferSegment> _segments = new List<BufferSegment>();

        /// <summary>Проверяет, пуст ли буфер</summary>
        public bool IsEmpty { get { return !_segments.Any(s => s.Length > 0); } }

        /// <summary>Записывает массив байт в указанное место буфера</summary>
        /// <param name="Position">Адрес, по которому следует разместить первый байт</param>
        /// <param name="Bytes">Данные для записи</param>
        public void Write(int Position, params byte[] Bytes)
        {
            BufferSegment segment = _segments.SingleOrDefault(s => s.StartPosition <= Position && s.StartPosition + s.Length >= Position);
            if (segment == null)
            {
                segment = new BufferSegment(Position);
                _segments.Add(segment);
            }
            List<BufferSegment> overlappedSegments =
                _segments.Where(s => s != segment && s.StartPosition >= Position && s.StartPosition < Position + Bytes.Length).ToList();
            foreach (BufferSegment s in overlappedSegments)
            {
                segment.Seek(s.StartPosition - segment.StartPosition, SeekOrigin.Begin);
                s.Seek(0, SeekOrigin.Begin);
                s.CopyTo(segment);
                _segments.Remove(s);
            }

            segment.Seek(Position - segment.StartPosition, SeekOrigin.Begin);
            segment.Write(Bytes, 0, Bytes.Length);
        }

        /// <summary>Записывает данные из буфера в указанный поток</summary>
        /// <param name="DestinationStream">Поток, в который будут записаны данные из буфера</param>
        public void CopyTo(Stream DestinationStream)
        {
            foreach (BufferSegment segment in _segments.OrderBy(s => s.StartPosition))
            {
                segment.Seek(0, SeekOrigin.Begin);
                DestinationStream.Seek(segment.StartPosition, SeekOrigin.Begin);
                segment.CopyTo(DestinationStream);
            }
        }

        public override string ToString() { return String.Format("SegmentedBuffer: {0} сегментов на {1} Байт", _segments.Count, _segments.Sum(s => s.Length)); }

        private class BufferSegment : MemoryStream
        {
            public BufferSegment(int StartPosition) { this.StartPosition = StartPosition; }
            public int StartPosition { get; private set; }

            public override string ToString() { return String.Format("{0:X4} -> {1} Байт", StartPosition, Length); }
        }
    }

    public class SegmentedBufferFactory : IBufferFactory
    {
        public IBuffer CreateBuffer() { return new SegmentedBuffer(); }
    }
}
