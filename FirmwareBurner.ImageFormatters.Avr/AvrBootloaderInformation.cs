using System;
using FirmwareBurner.ImageFormatters.Binary;
using FirmwareBurner.ImageFormatters.Binary.BodyLoaders;

namespace FirmwareBurner.ImageFormatters.Avr
{
    public class AvrBootloaderInformation : BinaryBootloaderInformation<AvrMemoryKind>
    {
        public AvrBootloaderInformation(String DeviceName, AvrFuses RequiredFuses, IBodyLoader BodyLoader, Placement<AvrMemoryKind> BootloaderPlacement,
                                        Placement<AvrMemoryKind> FilesTablePlacement, Placement<AvrMemoryKind> PropertiesTablePlacement) : base(DeviceName, BootloaderPlacement, BodyLoader)
        {
            this.PropertiesTablePlacement = PropertiesTablePlacement;
            this.FilesTablePlacement = FilesTablePlacement;
            this.RequiredFuses = RequiredFuses;
        }

        /// <summary>Размещение таблицы свойств</summary>
        public Placement<AvrMemoryKind> PropertiesTablePlacement { get; private set; }

        /// <summary>Размещение таблицы файлов</summary>
        public Placement<AvrMemoryKind> FilesTablePlacement { get; private set; }

        /// <summary>Необходимые значения FUSE-битов</summary>
        public AvrFuses RequiredFuses { get; private set; }
    }
}
