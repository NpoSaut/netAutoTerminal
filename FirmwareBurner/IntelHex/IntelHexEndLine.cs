using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FirmwareBurner.IntelHex
{
    public class IntelHexEndLine : IntelHexLine
    {
        public override byte Key { get { return 0x01; } }
        
        public override string ToString()
        {
            return string.Format("[{0}] EOF", Key);
        }
    }
}
