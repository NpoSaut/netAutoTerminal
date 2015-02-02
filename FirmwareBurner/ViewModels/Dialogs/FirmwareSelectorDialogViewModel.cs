using FirmwareBurner.ViewModels.Bases;
using FirmwareBurner.ViewModels.FirmwareSources;

namespace FirmwareBurner.ViewModels.Dialogs
{
    public class FirmwareSelectorDialogViewModel : ViewModelBase
    {
        public FirmwareSelectorDialogViewModel(FirmwareSelectorViewModel Selector) { this.Selector = Selector; }
        public FirmwareSelectorViewModel Selector { get; private set; }
    }
}
