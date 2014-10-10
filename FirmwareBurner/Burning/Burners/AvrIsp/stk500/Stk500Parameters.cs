using System;
using System.IO;

namespace FirmwareBurner.Burning.Burners.AvrIsp.stk500
{
    internal abstract class Stk500Parameter
    {
        protected abstract string Combine();
        public string Get() { return string.Format("-{0}", Combine()); }
    }

    internal abstract class OneKeyParameter : Stk500Parameter
    {
        public abstract string Key { get; }
        protected override string Combine() { return Key; }
    }

    internal class ConnectionParameter : Stk500Parameter
    {
        public enum ConnectionType
        {
            USB
        }

        public ConnectionParameter(ConnectionType connection = ConnectionType.USB) { this.connection = connection; }
        public ConnectionType connection { get; set; }
        protected override string Combine() { return string.Format("c{0}", connection); }
    }

    internal class GetSignatureParameter : OneKeyParameter
    {
        public override string Key
        {
            get { return "s"; }
        }
    }

    internal class DeviceNameParameter : Stk500Parameter
    {
        public DeviceNameParameter(String DeviceName) { this.DeviceName = DeviceName; }
        public string DeviceName { get; set; }
        protected override string Combine() { return string.Format("d{0}", DeviceName); }
    }

    internal class EraseParameter : OneKeyParameter
    {
        public override string Key
        {
            get { return "e"; }
        }
    }

    internal class InputFileParameter : Stk500Parameter
    {
        public enum FilePlacement
        {
            flash,
            eeprom
        }

        public InputFileParameter(FileInfo File, FilePlacement placement)
        {
            this.File = File;
            this.placement = placement;
        }

        public FilePlacement placement { get; set; }
        public FileInfo File { get; private set; }

        protected override string Combine() { return string.Format("i{0}\"{1}\"", placement.ToString()[0], File.FullName); }
    }

    internal class ProgramParameter : Stk500Parameter
    {
        public enum ProgramTarget
        {
            flash,
            eeprom,
            both
        }

        public ProgramParameter(ProgramTarget target) { this.target = target; }
        public ProgramTarget target { get; set; }
        protected override string Combine() { return string.Format("p{0}", target.ToString()[0]); }
    }

    internal class WriteFuseParameter : Stk500Parameter
    {
        public WriteFuseParameter(byte High, byte Low)
        {
            this.High = High;
            this.Low = Low;
        }

        public Byte High { get; set; }
        public Byte Low { get; set; }

        protected override string Combine() { return string.Format("f{0:X2}{1:X2}", High, Low); }
    }

    internal class WriteExtendedFuseParameter : Stk500Parameter
    {
        public WriteExtendedFuseParameter(byte FuseE) { Value = FuseE; }
        public Byte Value { get; set; }

        protected override string Combine() { return string.Format("E{0:X2}", Value); }
    }

    internal class ReadFuseParameter : OneKeyParameter
    {
        public override string Key
        {
            get { return "q"; }
        }
    }
}
