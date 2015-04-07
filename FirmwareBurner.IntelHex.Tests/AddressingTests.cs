using System;
using System.IO;
using System.Linq;
using NUnit.Framework;

namespace FirmwareBurner.IntelHex.Tests
{
    [TestFixture]
    public class AddressingTests
    {
        private byte[] GetDataBytes(int Length) { return Enumerable.Range(0, Length).Select(i => (Byte)i).ToArray(); }

        [Test]
        public void TestAddressing()
        {
            byte[] data = GetDataBytes(3);
            var hs = new IntelHexStream();
            hs.Seek(0x100, SeekOrigin.Begin);
            hs.Write(data, 0, data.Length);
            IntelHexFile hf = hs.GetHexFile();
            IntelHexDataLine dataLine = hf.OfType<IntelHexDataLine>().SingleOrDefault();
            Assert.NotNull(dataLine);
            Assert.AreEqual(0x100, dataLine.Address, "Данные записались по неверному адресу");
        }

        [Test]
        public void TestExtendedAddressing()
        {
            byte[] data = GetDataBytes(3);
            var hs = new IntelHexStream();
            hs.Seek(0xeeeeaaaa, SeekOrigin.Begin);
            hs.Write(data, 0, data.Length);
            IntelHexFile hf = hs.GetHexFile();
            IntelHexExAddressLine addressLine = hf.OfType<IntelHexExAddressLine>().SingleOrDefault();
            Assert.NotNull(addressLine, "Не добавилась строчка смещения адреса");
            Assert.AreEqual(0xeeee, addressLine.AddressExtension, "Адрес смещения \"страницы\" указан неверно");

            IntelHexDataLine dataLine = hf.OfType<IntelHexDataLine>().SingleOrDefault();
            Assert.NotNull(dataLine);
            Assert.AreEqual(0xaaaa, dataLine.Address, "Адрес смещения внутри \"страницы\" указан неверно");
        }
    }
}
