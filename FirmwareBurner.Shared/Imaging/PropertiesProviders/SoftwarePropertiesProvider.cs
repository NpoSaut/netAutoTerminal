using System;
using System.Collections.Generic;
using System.Linq;
using FirmwareBurner.Imaging.Binary;
using FirmwareBurner.Project;
using FirmwarePacking;

namespace FirmwareBurner.Imaging.PropertiesProviders
{
    public class SoftwarePropertiesProvider : IPropertiesProvider
    {
        private readonly IChecksumProvider _checksumProvider;
        private readonly ModuleProject _project;
        private readonly IStringEncoder _stringEncoder;

        public SoftwarePropertiesProvider(ModuleProject Project, IStringEncoder StringEncoder, IChecksumProvider ChecksumProvider)
        {
            _project = Project;
            _stringEncoder = StringEncoder;
            _checksumProvider = ChecksumProvider;
        }

        public IEnumerable<ParamRecord> GetProperties()
        {
            yield return new ParamRecord(1, _project.FirmwareInformation.FirmwareVersion.Major);
            yield return new ParamRecord(2, _project.FirmwareInformation.FirmwareVersion.Minor);
            yield return new ParamRecord(3, (int)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds);
            yield return new ParamRecord(6, GetGlobalChecksum(_project.FirmwareContent.Files));
            yield return new ParamRecord(7, _stringEncoder.Encode(_project.FirmwareInformation.FirmwareVersionLabel));
        }

        private ushort GetGlobalChecksum(IEnumerable<FirmwareFile> Files)
        {
            return Files.Select(f => _checksumProvider.GetChecksum(f.Content)).Aggregate((res, fcs) => (ushort)(res ^ fcs));
        }
    }
}
