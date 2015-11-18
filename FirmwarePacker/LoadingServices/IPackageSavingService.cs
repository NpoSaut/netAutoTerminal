using FirmwarePacker.Project;
using FirmwarePacker.TriggerActions.Notifications;
using Microsoft.Practices.Prism.Interactivity.InteractionRequest;

namespace FirmwarePacker.LoadingServices
{
    public interface IPackageSavingService
    {
        void SavePackage(PackageProject PackageProject, PackageVersion PackageVersion, string ProjectRoot);
        InteractionRequest<SaveFileInteractionContext> SaveFileRequest { get; }
    }
}
