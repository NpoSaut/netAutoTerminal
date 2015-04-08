using System;
using System.Linq;
using System.Text;

namespace FirmwareBurner
{
    public interface IStringEncoder
    {
        int Encode(string str);
        object Decode(int Code);
    }

    public class Cp1251StringEncoder : IStringEncoder
    {
        public const int StringLength = 4;

        public int Encode(string str)
        {
            if (string.IsNullOrWhiteSpace(str))
                return 0;
            return BitConverter.ToInt32(Encoding.GetEncoding(1251).GetBytes(str.PadRight(StringLength)).Reverse().ToArray(), 0);
        }

        public object Decode(int Code)
        {
            return Encoding.GetEncoding(1251).GetString(BitConverter.GetBytes(Code).Reverse().ToArray()).Trim(new[] { ' ', '\0' });
        }
    }
}
