namespace FirmwareBurner.Imaging.Binary.Buffers
{
    public class BufferWriter : WriterBase
    {
        private readonly IBuffer _buffer;

        public BufferWriter(IBuffer Buffer, int Position = 0)
        {
            _buffer = Buffer;
            this.Position = Position;
        }

        private int Position { get; set; }

        public override void WriteBytes(params byte[] Bytes)
        {
            _buffer.Write(Position, Bytes);
            Position += Bytes.Length;
        }
    }
}
