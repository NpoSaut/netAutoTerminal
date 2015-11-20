using System.Collections.Generic;
using FirmwareBurner.Imaging.Binary;

namespace FirmwareBurner.Imaging.PropertiesProviders
{
    /// <summary>Провайдер свойств ячейки</summary>
    public class DevicePropertiesProvider : IPropertiesProvider
    {
        private readonly TargetInformation _target;
        public DevicePropertiesProvider(TargetInformation Target) { _target = Target; }

        public IEnumerable<ParamRecord> GetProperties()
        {
            yield return new ParamRecord(129, _target.CellId);
            yield return new ParamRecord(131, _target.SerialNumber);
            yield return new ParamRecord(132, _target.AssemblyDate.Year * 100 + _target.AssemblyDate.Month);
            yield return new ParamRecord(133, _target.ChannelNumber);
            yield return new ParamRecord(134, _target.ModificationId);
        }
    }
}
