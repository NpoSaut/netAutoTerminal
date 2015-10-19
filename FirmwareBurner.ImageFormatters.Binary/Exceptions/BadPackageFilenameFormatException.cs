using System;
using System.Runtime.Serialization;
using FirmwareBurner.Imaging;

namespace FirmwareBurner.ImageFormatters.Binary.Exceptions
{
    /// <Summary>Не верный формат имени файла в sfp пакете</Summary>
    [Serializable]
    public class BadPackageFilenameFormatException : ImageFormatterException
    {
        public BadPackageFilenameFormatException(string FileName) : base(string.Format("Не верный формат имени файла в sfp пакете: {0}", FileName))
        {
            this.FileName = FileName;
        }

        public BadPackageFilenameFormatException(string FileName, string Explanation)
            : base(string.Format("Не верный формат имени файла в sfp пакете: {0} ({1})", FileName, Explanation))
        {
            this.FileName = FileName;
            this.Explanation = Explanation;
        }

        protected BadPackageFilenameFormatException(
            SerializationInfo info,
            StreamingContext context) : base(info, context) { }

        public string FileName { get; set; }
        public string Explanation { get; set; }
    }
}
