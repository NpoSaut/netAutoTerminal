namespace FirmwareBurner.Progress
{
    public interface IProgressControllerFactory
    {
        IProgressController CreateController(IProgressToken Token);
    }
}
