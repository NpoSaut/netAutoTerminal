using System;
using System.Runtime.Serialization;
using FirmwarePacking;

namespace FirmwareBurner.ImageFormatters.Avr
{
    /// <Summary>Ошибка при разборе имени файла в sfp-пакете</Summary>
    [Serializable]
    public class AvrImageFormatterParseFilenameException : ApplicationException
    {
        public AvrImageFormatterParseFilenameException(FirmwareFile File)
            : base(
                String.Format("Ошибка при разборе имени файла в sfp-пакете (имя файла: \"{0} не соответствует формату \"тип_памяти/адрес\")", File.RelativePath)
                ) { }

        public AvrImageFormatterParseFilenameException(FirmwareFile File, Exception inner)
            : base(
                String.Format("Ошибка при разборе имени файла в sfp-пакете (имя файла: {0} не соответствует формату \"тип_памяти/адрес\")", File.RelativePath),
                inner) { }

        public AvrImageFormatterParseFilenameException(FirmwareFile File, String Reason)
            : base(
                String.Format("Ошибка при разборе имени файла в sfp-пакете: {1}\n (имя файла: {0} не соответствует формату \"тип_памяти/адрес\")",
                              File.RelativePath, Reason)) { }

        public AvrImageFormatterParseFilenameException(FirmwareFile File, String Reason, Exception inner)
            : base(
                String.Format("Ошибка при разборе имени файла в sfp-пакете: {1}\n (имя файла: {0} не соответствует формату \"тип_памяти/адрес\")",
                              File.RelativePath, Reason), inner) { }

        protected AvrImageFormatterParseFilenameException(
            SerializationInfo info,
            StreamingContext context) : base(info, context) { }
    }
}
