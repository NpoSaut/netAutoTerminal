using System;
using FirmwareBurner.FirmwareElements;
using System.IO;
namespace FirmwareBurner.Formating
{
    public interface IFirmwareFormatter
    {
        void WriteToStreams(FirmwareImage Image, Stream FlashOutput, Stream EepromOutput);
    }
}
