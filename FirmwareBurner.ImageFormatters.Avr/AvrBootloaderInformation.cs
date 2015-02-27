using System;
using System.IO;
using System.Reflection;
using FirmwareBurner.Annotations;

namespace FirmwareBurner.ImageFormatters.Avr
{
    public class AvrBootloaderInformation
    {
        private readonly string _bootloaderBodyResourceName;

        /// <summary>�������������� ����� ��������� ������ <see cref="T:System.Object" />.</summary>
        public AvrBootloaderInformation(AvrFuses RequiredFuses, PlacementsInformation Placements, string BootloaderBodyResourceName)
        {
            _bootloaderBodyResourceName = BootloaderBodyResourceName;
            this.Placements = Placements;
            this.RequiredFuses = RequiredFuses;
        }

        /// <summary>���������� �������� � �������� ��������</summary>
        public PlacementsInformation Placements { get; private set; }

        /// <summary>����������� �������� FUSE-�����</summary>
        public AvrFuses RequiredFuses { get; private set; }

        /// <summary>�������� ���� ����������</summary>
        [NotNull]
        public byte[] GetBootloaderBody()
        {
            Stream bodyResourceStream = Assembly.GetAssembly(typeof (StaticAvrBootloadersCatalog)).GetManifestResourceStream(_bootloaderBodyResourceName);
            if (bodyResourceStream == null)
                throw new ApplicationException("�� ������� ����� ������ � ����� ����������");
            using (var body = new MemoryStream())
            {
                bodyResourceStream.CopyTo(body);
                return body.ToArray();
            }
        }
    }
}
