using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FirmwareBurner.IntelHex
{
    public class IntelHexEndLine : IntelHexLine
    {
        public override byte Key { get { return 0x01; } }
    }
}
