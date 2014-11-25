using System.Collections.Generic;
using System.Linq;
using FirmwareBurner.Attributes;
using FirmwareBurner.Burning;

namespace FirmwareBurner
{
    public static class BurningToolFacadeHelper
    {
        public static IEnumerable<string> GetTargetDeviceNames<TImage>(this IBurningToolFacadeFactory<TImage> ToolFacade)
        {
            return ToolFacade.GetType()
                             .GetCustomAttributes(typeof (TargetDeviceAttribute), false)
                             .OfType<TargetDeviceAttribute>()
                             .Select(target => target.DeviceName).ToArray();
        }
    }
}
