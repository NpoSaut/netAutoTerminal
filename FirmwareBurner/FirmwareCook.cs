using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using FirmwareBurner.Burning;
using FirmwareBurner.Formating;
using FirmwareBurner.Imaging.Binary;
using FirmwareBurner.IntelHex;
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
                    //res.Placement = FileStorage.Flash;
                    break;
                case "e":
                    //res.Placement = FileStorage.Eeprom;
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
                            new ParamRecord(129, Target.CellId),
                            new ParamRecord(130, Target.Module),
                            new ParamRecord(131, SerialNumber),
                            new ParamRecord(132, AssemblyDate.Year * 100 + AssemblyDate.Month),
                            new ParamRecord(133, Target.Channel),
                            new ParamRecord(134, Target.CellModification),

                            // Информация о прошивке
                            new ParamRecord(1, Package.Information.FirmwareVersion.Major),
                            new ParamRecord(2, Package.Information.FirmwareVersion.Minor),
                            new ParamRecord(3, (int)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds),
                        }
                };
        }
    }
}
