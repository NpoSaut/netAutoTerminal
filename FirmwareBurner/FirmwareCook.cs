using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FirmwareBurner.Burning;
using FirmwareBurner.FirmwareElements;
using FirmwareBurner.Formating;
using FirmwareBurner.IntelHex;
using FirmwarePacking;
using System.IO;
using Microsoft.Practices.Unity;

namespace FirmwareBurner
{
    public class FirmwareCook : FirmwareBurner.IFirmwareCook
    {
        [Dependency("BootloaderImageFileName")]
        public FileInfo BootloaderFile { get; set; }

        public IFirmwareFormatter Formatter { get; set; }

        public FirmwareCook(IFirmwareFormatter Formatter)
        {
            this.Formatter = Formatter;
        }

        private Pie GetPie(FirmwareImage Image, IFirmwareFormatter Formatter)
        {
            IntelHexStream FlashStream = new IntelHexStream();
            IntelHexStream EepromStream = new IntelHexStream();
            Formatter.WriteToStreams(Image, FlashStream, EepromStream);

            return new Pie() { FlashFile = FlashStream.GetHexFile(), EepromFile = EepromStream.GetHexFile() };
        }

        private FirmwareComponent GetComponent(FirmwarePackage Package, ComponentTarget Target)
        {
            return Package.Components.First(c => c.Targets.Contains(Target));
        }
        private FileRecord SortFile(FirmwareFile file)
        {
            var path = file.RelativePath.Split(new char[] { '\\' });
            if (path.Length != 2) throw new Exception("Прошивка содержит файл с именем, не соответствующим стандартам прошивок для AVR-устройств");
            var res =
                new FileRecord()
                {
                    Body = new MemoryStream(file.Content),
                    FileAdress = Convert.ToInt32(path[1], 16)
                };
            switch(path[0])
            {
                case "f": res.Placement = FileStorage.Flash; break;
                case "e": res.Placement = FileStorage.Eeprom; break;
                default: throw new Exception("Прошивка содержит файл с расположением, не поддерживаемым AVR-устройствами");
            }
            return res;
        }

        private FirmwareImage Depack(FirmwarePackage Package, ComponentTarget Target, int SerialNumber, DateTime AssemblyDate)
        {
            return
                new FirmwareImage()
                {
                    Bootloader = new BootloaderBody() { Body = BootloaderFile.OpenRead() },
                    FileTable = GetComponent(Package, Target).Files.Select(f => SortFile(f)).ToList(),
                    ParamList =
                        new List<ParamRecord>()
                        {
                            // Информация о блоке
                            new ParamRecord() { Key = 128, Value = Target.SystemId },
                            new ParamRecord() { Key = 129, Value = Target.CellId },
                            new ParamRecord() { Key = 130, Value = Target.CellModification },
                            new ParamRecord() { Key = 133, Value = Target.Channel },
                            new ParamRecord() { Key = 131, Value = Target.SystemId },
                            new ParamRecord() { Key = 131, Value = SerialNumber },
                            new ParamRecord() { Key = 132, Value = (int)(AssemblyDate.ToUniversalTime() - new DateTime(1970, 1, 1)).TotalSeconds },

                            // Информация о прошивке
                            new ParamRecord() { Key = 1, Value = Package.Information.FirmwareVersion.Major },
                            new ParamRecord() { Key = 2, Value = Package.Information.FirmwareVersion.Minor },
                            new ParamRecord() { Key = 3, Value = (int)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds },
                        }
                };
        }

        public Pie Cook(FirmwarePackage Package, ComponentTarget Target, int SerialNumber, DateTime AssemblyDate)
        {
            return
                GetPie(
                    Depack(Package, Target, SerialNumber, AssemblyDate),
                    Formatter);
        }
    }
}
