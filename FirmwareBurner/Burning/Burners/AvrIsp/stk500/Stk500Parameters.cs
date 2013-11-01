using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace FirmwareBurner.Burning.Burners.AvrIsp.stk500
{
    abstract class Stk500Parameter
    {
        protected abstract string Combine();
        public string Get()
        {
            return string.Format("-{0}", Combine());
        }
    }
    abstract class OneKeyParameter : Stk500Parameter
    {
        public abstract string Key { get; }
        protected override string  Combine() { return Key; }
    }

    class ConnectionParameter : Stk500Parameter
    {
        public enum ConnectionType { USB }
        public ConnectionType connection { get; set; }
        public ConnectionParameter(ConnectionType connection = ConnectionType.USB)
        {
            this.connection = connection;
        }
        protected override string Combine() { return string.Format("c{0}", connection); }
    }
    class GetSignatureParameter : OneKeyParameter
    {
        public override string Key { get { return "s"; } }
    }
    class DeviceNameParameter : Stk500Parameter
    {
        public string DeviceName { get; set; }
        public DeviceNameParameter(String DeviceName)
        {
            this.DeviceName = DeviceName;
        }
        protected override string Combine() { return string.Format("d{0}", DeviceName); }
    }
    class EraseParameter : OneKeyParameter
    {
        public override string Key { get { return "e"; } }
    }
    class InputFileParameter : Stk500Parameter
    {
        public enum FilePlacement { flash, eeprom }
        public FilePlacement placement { get; set; }
        public FileInfo File { get; private set; }
        public InputFileParameter(FileInfo File, FilePlacement placement)
        {
            this.File = File;
            this.placement = placement;
        }
        protected override string Combine() { return string.Format("i{0}\"{1}\"", placement.ToString()[0], File.FullName); }
    }
    class ProgramParameter : Stk500Parameter
    {
        public enum ProgramTarget { flash, eeprom, both }
        public ProgramTarget target { get; set; }
        public ProgramParameter(ProgramTarget target)
        {
            this.target = target;
        }
        protected override string Combine() { return string.Format("p{0}", target.ToString()[0]); }
    }

    class WriteFuseParameter : Stk500Parameter
    {
        public Byte High { get; set; }
        public Byte Low { get; set; }

        public WriteFuseParameter(byte High, byte Low)
        {
            this.High = High;
            this.Low = Low;
        }

        protected override string Combine() { return string.Format("f{0:X2}{1:X2}", High, Low); }
    }
    class WriteExtendedFuseParameter : Stk500Parameter
    {
        public Byte Value { get; set; }

        public WriteExtendedFuseParameter(byte FuseE)
        {
            this.Value = FuseE;
        }

        protected override string Combine() { return string.Format("E{0:X2}", Value); }
    }
    class ReadFuseParameter : OneKeyParameter
    {
        public override string Key
        {
            get { return "q"; }
        }
    }
}
