using System;
using System.IO;
using FirmwareBurner.Burning;
using FirmwareBurner.Formating;
using FirmwarePacking;

namespace FirmwareBurner
{
    public interface IFirmwareCook
    {
        FileInfo BootloaderFile { get; set; }
        IFirmwareFormatter Formatter { get; set; }
        Pie Cook(FirmwarePackage Package, ComponentTarget Target, int SerialNumber, DateTime AssemblyDate);
    }
}
