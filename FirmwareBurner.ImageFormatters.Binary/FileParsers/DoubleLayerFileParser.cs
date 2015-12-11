using System.Collections.Generic;
using System.Globalization;
using FirmwareBurner.ImageFormatters.Binary.Exceptions;
using FirmwarePacking;

namespace FirmwareBurner.ImageFormatters.Binary.FileParsers
{
    public class DoubleLayerFileParser<TMemoryKind> : IFileParser<TMemoryKind>
    {
        private readonly IDictionary<string, TMemoryKind> _memoryKinds;
        public DoubleLayerFileParser(IDictionary<string, TMemoryKind> MemoryKinds) { _memoryKinds = MemoryKinds; }

        public BinaryImageFile<TMemoryKind> GetImageFile(FirmwareFile FirmwareFile)
        {
            string[] nameParts = FirmwareFile.RelativePath.Split(FirmwarePackage.PathSeparator);
            if (nameParts.Length != 2)
            {
                throw new BadPackageFilenameFormatException(
                    FirmwareFile.RelativePath,
                    string.Format(
                        "Файл должен находиться в папке, обозначающей тип памяти (допустимые значения: {0}) и иметь имя, соответствующее адресу его размещения (в HEX, без расширения)",
                        string.Join(", ", _memoryKinds.Keys)));
            }
            TMemoryKind memoryKind;
            if (!_memoryKinds.TryGetValue(nameParts[0], out memoryKind))
            {
                throw new BadPackageFilenameFormatException(
                    FirmwareFile.RelativePath,
                    string.Format("Файл должен находиться в папке, обозначающей тип памяти (допустимые значения: {0})", string.Join(", ", _memoryKinds.Keys)));
            }
            int address;
            if (!int.TryParse(nameParts[1], NumberStyles.HexNumber, CultureInfo.InvariantCulture, out address))
            {
                throw new BadPackageFilenameFormatException(
                    FirmwareFile.RelativePath,
                    "Имя файла должно представлять адрес его размещения в HEX-формате (без \"0x\" и расширения");
            }
            return new BinaryImageFile<TMemoryKind>(new Placement<TMemoryKind>(memoryKind, address), FirmwareFile.Content);
        }
    }
}
