using System.Collections.Generic;
using FirmwareBurner.Imaging.Binary;

namespace FirmwareBurner.Imaging
{
    public interface IPropertiesProvider
    {
        IEnumerable<ParamRecord> GetProperties();
    }
}
