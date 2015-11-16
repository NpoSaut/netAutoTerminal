using System;
using FirmwarePacker.TriggerActions.Notifications;
using FirmwarePacker.ViewModels;
using Microsoft.Practices.Prism.Interactivity.InteractionRequest;

namespace FirmwarePacker
{
    public interface ILoadProjectService
    {
        void RequestLoadProject(InteractionRequest<OpenFileInteractionContext> OpenFileRequest, Action<ProjectViewModel> CallbackAction);
        ProjectViewModel LoadProject(string FileName);
    }
}
