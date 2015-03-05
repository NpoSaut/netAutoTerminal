using FirmwareBurner.ViewModels.Bases;
using Microsoft.Practices.Prism.Interactivity.InteractionRequest;

namespace FirmwareBurner.ViewModels.Dialogs
{
    public abstract class DialogViewModelBase : ViewModelBase
    {
        public DialogViewModelBase() { CloseDialogRequest = new InteractionRequest<Notification>(); }
        public InteractionRequest<Notification> CloseDialogRequest { get; set; }
        protected void Close() { CloseDialogRequest.Raise(new Notification()); }
    }
}
