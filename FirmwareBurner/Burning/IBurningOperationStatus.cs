namespace FirmwareBurner.Burning
{
    public interface IBurningOperationStatusReceiver
    {
        double Progress { set; }
        void OnFinish();
    }
}
