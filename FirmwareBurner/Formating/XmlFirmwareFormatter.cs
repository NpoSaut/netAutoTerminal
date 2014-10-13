using System;
using System.IO;
using System.Xml.Linq;
using FirmwareBurner.Models.Images.Binary;

namespace FirmwareBurner.Formating
{
    public class XmlFirmwareFormatter : IFirmwareFormatter
    {
        private XmlFirmwareFormatter() { }

        private XmlFirmwareFormatter(ElementFormat FileTableFormat, ElementFormat ParamListFormat, ElementFormat BootloaderBodyFormat)
            : this()
        {
            this.FileTableFormat = FileTableFormat;
            this.ParamListFormat = ParamListFormat;
            this.BootloaderBodyFormat = BootloaderBodyFormat;
        }

        public XmlFirmwareFormatter(XElement XFormat)
            : this(
                new ElementFormat(XFormat.Element("FilesTable")),
                new ElementFormat(XFormat.Element("ParamList")),
                new ElementFormat(XFormat.Element("BootloaderBody"))) { }

        private ElementFormat FileTableFormat { get; set; }
        private ElementFormat ParamListFormat { get; set; }
        private ElementFormat BootloaderBodyFormat { get; set; }

        public void WriteToStreams(FirmwareImage Image, Stream FlashOutput, Stream EepromOutput)
        {
            foreach (FileRecord file in Image.FilesTable)
            {
//                Stream FileOutput;
//                switch (file.Placement)
//                {
//                    case FileStorage.Flash:
//                        FileOutput = FlashOutput;
//                        break;
//                    case FileStorage.Eeprom:
//                        FileOutput = EepromOutput;
//                        break;
//                    default:
//                        continue;
//                }
//                FileOutput.Seek(file.FileAddress, SeekOrigin.Begin);
//                file.Body.CopyTo(FileOutput);
            }
            FileTableFormat.WriteTo(Image.FilesTable, FlashOutput);
            ParamListFormat.WriteTo(Image.ParamList, FlashOutput);
            BootloaderBodyFormat.WriteTo(Image.Bootloader, FlashOutput);
            Image.Bootloader.Body.CopyTo(FlashOutput);
        }

        public static IFirmwareFormatter ReadFormat(String FileName) { return ReadFormat(new FileInfo(FileName)); }
        public static IFirmwareFormatter ReadFormat(FileInfo File) { return ReadFormat(XDocument.Load(File.OpenRead()).Root); }
        public static XmlFirmwareFormatter ReadFormat(XElement XFormat) { return new XmlFirmwareFormatter(XFormat); }
    }
}
