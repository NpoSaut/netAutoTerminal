using System;
using NUnit.Framework;

namespace FirmwareBurner.IntelHex.Tests
{
    [TestFixture, Description("Сравнение строк, получаемых в программе с образцами из Википедии")]
    public class HexLineFortamTests
    {
        [Test(Description = "Проверка правильности формирования строки с данными")]
        public void TestFormatDataLine()
        {
            var data = new Byte[] { 0x19, 0x4E, 0x79, 0x23, 0x46, 0x23, 0x96, 0x57, 0x78, 0x23, 0x9E, 0xDA, 0x3F, 0x01, 0xB2, 0xCA };
            var line = new IntelHexDataLine(0x0120, data);
            Assert.AreEqual(":10012000194E79234623965778239EDA3F01B2CAA7", line.ToHexString(), "Полученная строка не совпадает с образцом");
        }

        [Test, Description("Проверка правильности формирования строки окончания файла")]
        public void TestFormatEofLine()
        {
            var line = new IntelHexEndLine();
            Assert.AreEqual(":00000001FF", line.ToHexString(), "Полученная строка не совпадает с образцом");
        }

        [Test, Description("Проверка правильности формирования строки расширенного адреса")]
        public void TestFormatExtendedAddressLine()
        {
            var line = new IntelHexExAddressLine(0x00ff);
            Assert.AreEqual(":0200000400FFFB", line.ToHexString(), "Полученная строка не совпадает с образцом");
        }
    }
}
