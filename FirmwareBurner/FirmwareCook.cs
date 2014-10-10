using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using FirmwareBurner.Burning;
using FirmwareBurner.Formating;
using FirmwareBurner.IntelHex;
using FirmwareBurner.Models.Images.Binary;
using FirmwarePacking;
using Microsoft.Practices.Unity;

namespace FirmwareBurner
{
    public class FirmwareCook : IFirmwareCook
    {
        public FirmwareCook(IFirmwareFormatter Formatter) { this.Formatter = Formatter; }

        [Dependency("BootloaderImageFileName")]
        public FileInfo BootloaderFile { get; set; }

        public IFirmwareFormatter Formatter { get; set; }

        public Pie Cook(FirmwarePackage Package, ComponentTarget Target, int SerialNumber, DateTime AssemblyDate)
        {
            return
                GetPie(
                    Depack(Package, Target, SerialNumber, AssemblyDate),
                    Formatter);
        }

        private Pie GetPie(FirmwareImage Image, IFirmwareFormatter Formatter)
        {
            var FlashStream = new IntelHexStream();
            var EepromStream = new IntelHexStream();
            Formatter.WriteToStreams(Image, FlashStream, EepromStream);

            return new Pie { FlashFile = FlashStream.GetHexFile(), EepromFile = EepromStream.GetHexFile() };
        }

        private FirmwareComponent GetComponent(FirmwarePackage Package, ComponentTarget Target)
        {
            return Package.Components.First(c => c.Targets.Contains(Target));
        }

        private FileRecord SortFile(FirmwareFile file)
        {
            string[] path = file.RelativePath.Split(new[] { '/' });
            if (path.Length != 2) throw new Exception("Прошивка содержит файл с именем, не соответствующим стандартам прошивок для AVR-устройств");
            var res =
                new FileRecord
                {
                    Body = new MemoryStream(file.Content),
                    FileAddress = Convert.ToInt32(path[1], 16),
                    Checksum = FudpCrc.CalcCrc(file.Content)
                };
            switch (path[0])
            {
                case "f":
                    res.Placement = FileStorage.Flash;
                    break;
                case "e":
                    res.Placement = FileStorage.Eeprom;
                    break;
                default:
                    throw new Exception("Прошивка содержит файл с расположением, не поддерживаемым AVR-устройствами");
            }
            return res;
        }

        private FirmwareImage Depack(FirmwarePackage Package, ComponentTarget Target, int SerialNumber, DateTime AssemblyDate)
        {
            return
                new FirmwareImage
                {
                    Bootloader = new BootloaderBody { Body = BootloaderFile.OpenRead() },
                    FilesTable = GetComponent(Package, Target).Files.Select(f => SortFile(f)).ToList(),
                    ParamList =
                        new List<ParamRecord>
                        {
                            // Информация о блоке
                            new ParamRecord { Key = 129, Value = Target.CellId },
                            new ParamRecord { Key = 130, Value = Target.Module },
                            new ParamRecord { Key = 131, Value = SerialNumber },
                            new ParamRecord { Key = 132, Value = AssemblyDate.Year * 100 + AssemblyDate.Month },
                            new ParamRecord { Key = 133, Value = Target.Channel },
                            new ParamRecord { Key = 134, Value = Target.CellModification },

                            // Информация о прошивке
                            new ParamRecord { Key = 1, Value = Package.Information.FirmwareVersion.Major },
                            new ParamRecord { Key = 2, Value = Package.Information.FirmwareVersion.Minor },
                            new ParamRecord { Key = 3, Value = (int)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds },
                        }
                };
        }
    }

    public class FudpCrc
    {
        public static ushort CalcCrc(Byte[] data)
        {
            ushort crc = 0xffff;
            for (int i = 0; i < data.Length; i++)
                crc = crc_ccitt(crc, data[i]);
            return crc;
        }

        private static ushort crc_ccitt(ushort crc, byte cdata)
        {
            byte b = 0xff;
            cdata ^= (byte)(crc & b);
            cdata ^= (byte)(cdata << 4);

            return (ushort)((((cdata << 8)) | ((crc >> 8))) ^
                            (ushort)(cdata >> 4) ^
                            (cdata << 3));
        }
    }
}
