﻿using System;

namespace FirmwareBurner
{
    public class FudpCrc
    {
        public static ushort CalcCrc(Byte[] data)
        {
            ushort crc = 0xffff;
            for (int i = 0; i < data.Length; i++)
                crc = crc_ccitt(crc, data[i]);
            return crc;
        }

        private static ushort crc_ccitt(ushort crc, byte cdata)
        {
            byte b = 0xff;
            cdata ^= (byte)(crc & b);
            cdata ^= (byte)(cdata << 4);

            return (ushort)((((cdata << 8)) | ((crc >> 8))) ^
                            (ushort)(cdata >> 4) ^
                            (cdata << 3));
        }
    }
}
