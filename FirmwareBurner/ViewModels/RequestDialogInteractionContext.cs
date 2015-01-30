using FirmwareBurner.ViewModels.Bases;
using Microsoft.Practices.Prism.Interactivity.InteractionRequest;

namespace FirmwareBurner.ViewModels
{
    public class RequestDialogInteractionContext : Notification
    {
        public RequestDialogInteractionContext(ViewModelBase ViewModel) { this.ViewModel = ViewModel; }
        public ViewModelBase ViewModel { get; private set; }
    }
}