using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FirmwareBurner.IntelHex;
using FirmwareBurner.FirmwareElements;
using FirmwareBurner.Formating;

namespace FirmwareBurner.Burning
{
    /// <summary>
    /// Представляет HEX-файлы прошивки, EEPROM и фузи-биты
    /// </summary>
    /// <remarks>
    ///          (
    ///           )
    ///      __..---..__
    ///  ,-='  /  |  \  `=-.
    /// :--..___________..--;
    ///  \.,_____________,./ 
    /// </remarks>
    public class Pie
    {
        public IntelHexFile FlashFile { get; set; }
        public IntelHexFile EepromFile { get; set; }
    }
}
