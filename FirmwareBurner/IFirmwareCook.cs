using System;
namespace FirmwareBurner
{
    public interface IFirmwareCook
    {
        System.IO.FileInfo BootloaderFile { get; set; }
        FirmwareBurner.Burning.Pie Cook(FirmwarePacking.FirmwarePackage Package, FirmwarePacking.ComponentTarget Target);
        FirmwareBurner.Formating.IFirmwareFormatter Formatter { get; set; }
    }
}
