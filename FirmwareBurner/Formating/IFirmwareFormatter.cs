using System.IO;
using FirmwareBurner.Imaging.Binary;

namespace FirmwareBurner.Formating
{
    public interface IFirmwareFormatter
    {
        void WriteToStreams(FirmwareImage Image, Stream FlashOutput, Stream EepromOutput);
    }
}
