using FirmwareBurner.ViewModels.Bases;
using Microsoft.Practices.Prism.Interactivity.InteractionRequest;

namespace FirmwareBurner.ViewModels
{
    public class RequestDialogInteractionContext<TViewModel> : Notification where TViewModel : ViewModelBase
    {
        public RequestDialogInteractionContext(TViewModel ViewModel)
        {
            this.ViewModel = ViewModel;
            Content = ViewModel;
        }
        public TViewModel ViewModel { get; private set; }
    }
}
