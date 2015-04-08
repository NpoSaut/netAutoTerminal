using System;
using System.Collections.Generic;
using System.Linq;

namespace FirmwareBurner.IntelHex
{
    /// <summary>Реализации контрольных сумм для IntelHex</summary>
    public static class IntelHexChecksum
    {
        /// <summary>Подсчитывает контрольную сумму для строки с указанным содержимым</summary>
        /// <param name="Content">Содержимое строки</param>
        public static Byte GetChecksum(IEnumerable<Byte> Content) { return (byte)(0x100 - Content.Aggregate(0, (s, b) => unchecked(s + b))); }
    }
}
