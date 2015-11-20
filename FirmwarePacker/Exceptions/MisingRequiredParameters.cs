using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace FirmwarePacker.Exceptions
{
    /// <Summary>Не указаны необходимые параметры</Summary>
    [Serializable]
    public class MisingRequiredParameters : ApplicationException
    {
        public MisingRequiredParameters(ICollection<String> MissingParameters)
            : base(string.Format("Не указаны необходимые параметры ({0})",
                                 string.Join(", ", MissingParameters))) { this.MissingParameters = MissingParameters; }

        protected MisingRequiredParameters(
            SerializationInfo info,
            StreamingContext context) : base(info, context) { }

        public ICollection<string> MissingParameters { get; set; }
    }
}
