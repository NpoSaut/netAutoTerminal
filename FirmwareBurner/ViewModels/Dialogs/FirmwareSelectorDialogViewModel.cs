using System;
using FirmwareBurner.ViewModels.Bases;
using FirmwareBurner.ViewModels.FirmwareSources;
using FirmwarePacking;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Interactivity.InteractionRequest;

namespace FirmwareBurner.ViewModels.Dialogs
{
    public class FirmwareSelectorDialogViewModel : ViewModelBase
    {
        public FirmwareSelectorDialogViewModel(FirmwareSelectorViewModel Selector)
        {
            this.Selector = Selector;

            this.Selector.SelectedPackageChanged += SelectorOnSelectedPackageChanged;

            CloseDialogRequest = new InteractionRequest<Notification>();
            SubmitCommand = new DelegateCommand(Submit, CanSubmit);
        }

        public InteractionRequest<Notification> CloseDialogRequest { get; set; }

        public FirmwareSelectorViewModel Selector { get; private set; }

        public DelegateCommand SubmitCommand { get; private set; }
        public FirmwarePackage SelectedPackage { get; private set; }

        private void SelectorOnSelectedPackageChanged(object Sender, EventArgs EventArgs) { SubmitCommand.RaiseCanExecuteChanged(); }

        private bool CanSubmit() { return Selector.IsPackageSelected; }

        private void Submit()
        {
            SelectedPackage = Selector.SelectedPackage.GetPackageBody();
            CloseDialogRequest.Raise(new Notification());
        }
    }
}
