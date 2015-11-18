using System;
using FirmwarePacker.TriggerActions.Notifications;
using FirmwarePacker.ViewModels;
using Microsoft.Practices.Prism.Interactivity.InteractionRequest;

namespace FirmwarePacker.LoadingServices
{
    public interface ILoadProjectService
    {
        void RequestLoadProject(Action<ProjectViewModel> CallbackAction);
        ProjectViewModel LoadProject(string FileName);
        InteractionRequest<OpenFileInteractionContext> OpenFileRequest { get; }
    }
}
