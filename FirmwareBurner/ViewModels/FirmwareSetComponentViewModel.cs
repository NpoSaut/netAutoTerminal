using System;
using System.Windows.Input;
using FirmwareBurner.ViewModels.Bases;
using FirmwareBurner.ViewModels.FirmwareSources;
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

            FirmwareSelectionRequest = new InteractionRequest<RequestDialogInteractionContext>();
            SelectFirmwareCommand = new DelegateCommand(RequestFirmwareSelection);

            FirmwareSelector.SelectedPackageChanged += FirmwareSelectorOnSelectedPackageChanged;
        }

        public int ModuleIndex { get; private set; }
        public String ModuleName { get; private set; }
        public ICommand SelectFirmwareCommand { get; private set; }
        public FirmwareSelectorViewModel FirmwareSelector { get; private set; }
        public InteractionRequest<RequestDialogInteractionContext> FirmwareSelectionRequest { get; private set; }
        private void RequestFirmwareSelection() { FirmwareSelectionRequest.Raise(new RequestDialogInteractionContext(FirmwareSelector)); }

        public event EventHandler SetChanged;

        private void FirmwareSelectorOnSelectedPackageChanged(object Sender, EventArgs Args) { OnSetChanged(); }

        protected virtual void OnSetChanged()
        {
            EventHandler handler = SetChanged;
            if (handler != null) handler(this, EventArgs.Empty);
        }
    }
}
