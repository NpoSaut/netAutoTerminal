using System;
using System.Windows.Input;
using FirmwareBurner.ViewModels.Bases;
using FirmwareBurner.ViewModels.Dialogs;
using FirmwareBurner.ViewModels.FirmwareSources;
using FirmwarePacking;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Interactivity.InteractionRequest;

namespace FirmwareBurner.ViewModels
{
    public class FirmwareSetComponentViewModel : ViewModelBase
    {
        public FirmwareSetComponentViewModel(int ModuleIndex, string ModuleName, FirmwareSelectorViewModel FirmwareSelector)
        {
            this.FirmwareSelector = FirmwareSelector;
            this.ModuleName = ModuleName;
            this.ModuleIndex = ModuleIndex;

            FirmwareSelectionRequest = new InteractionRequest<RequestDialogInteractionContext<FirmwareSelectorDialogViewModel>>();
            SelectFirmwareCommand = new DelegateCommand(RequestFirmwareSelection);

            FirmwareSelector.SelectedPackageChanged += FirmwareSelectorOnSelectedPackageChanged;
        }

        public int ModuleIndex { get; private set; }
        public String ModuleName { get; private set; }
        public ICommand SelectFirmwareCommand { get; private set; }
        public FirmwareSelectorViewModel FirmwareSelector { get; private set; }
        public InteractionRequest<RequestDialogInteractionContext<FirmwareSelectorDialogViewModel>> FirmwareSelectionRequest { get; private set; }
        public FirmwarePackage SelectedFirmware { get; private set; }

        private void RequestFirmwareSelection()
        {
            FirmwareSelectionRequest.Raise(new RequestDialogInteractionContext<FirmwareSelectorDialogViewModel>(new FirmwareSelectorDialogViewModel(FirmwareSelector))
                                           {
                                               Title = "Выбор прошивки"
                                           },
                                           FirmwareSelectedCallback);
        }

        private void FirmwareSelectedCallback(RequestDialogInteractionContext<FirmwareSelectorDialogViewModel> Context)
        {
            SelectedFirmware = Context.ViewModel.SelectedPackage;
        }

        public event EventHandler SetChanged;

        private void FirmwareSelectorOnSelectedPackageChanged(object Sender, EventArgs Args) { OnSetChanged(); }

        protected virtual void OnSetChanged()
        {
            EventHandler handler = SetChanged;
            if (handler != null) handler(this, EventArgs.Empty);
        }
    }
}
