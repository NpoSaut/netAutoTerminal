using System;
using NUnit.Framework;

namespace FirmwareBurner.IntelHex.Tests
{
    [TestFixture]
    public class ChecksumTests
    {
        [Test, Description("Проверка правильности подсчёта контрольной суммы для строки файла")]
        public void TestChecksum()
        {
            var data = new Byte[] { 0x10, 0x01, 0x00, 0x00, 0x21, 0x46, 0x01, 0x36, 0x01, 0x21, 0x47, 0x01, 0x36, 0x00, 0x7E, 0xFE, 0x09, 0xD2, 0x19, 0x01 };
            byte calculatedChecksum = IntelHexChecksum.GetChecksum(data);
            Assert.AreEqual(0x40, calculatedChecksum, "Не правильно считается контрольная сумма");
        }
    }
}
