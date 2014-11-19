using FirmwarePacking;

namespace FirmwareBurner.FirmwareProviders
{
    public interface IFirmwareProvider
    {
        FirmwarePackage GetFirmwarePackage(ComponentTarget Target);
    }
}