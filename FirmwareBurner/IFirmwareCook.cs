using System;
using FirmwarePacking;
namespace FirmwareBurner
{
    public interface IFirmwareCook
    {
        System.IO.FileInfo BootloaderFile { get; set; }
        FirmwareBurner.Burning.Pie Cook(FirmwarePackage Package, ComponentTarget Target, int SerialNumber, DateTime AssemblyDate);
        FirmwareBurner.Formating.IFirmwareFormatter Formatter { get; set; }
    }
}
