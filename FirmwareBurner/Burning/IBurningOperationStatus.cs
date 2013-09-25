using System;
namespace FirmwareBurner.Burning
{
    public interface IBurningOperationStatusReceiver
    {
        void OnFinish();
        double Progress { set; }
    }
}
