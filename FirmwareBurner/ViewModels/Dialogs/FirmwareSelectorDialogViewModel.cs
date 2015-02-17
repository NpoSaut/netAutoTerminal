using System;
using FirmwareBurner.ViewModels.Bases;
using FirmwareBurner.ViewModels.FirmwareSources;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Interactivity.InteractionRequest;

namespace FirmwareBurner.ViewModels.Dialogs
{
    public class FirmwareSelectorDialogViewModel : ViewModelBase
    {
        public FirmwareSelectorDialogViewModel(FirmwareSelectorViewModel Selector, FirmwarePackageViewModel PreselectedPackage = null)
        {
            this.Selector = Selector;
            SelectedPackage = PreselectedPackage;

            this.Selector.SelectedPackageChanged += SelectorOnSelectedPackageChanged;

            CloseDialogRequest = new InteractionRequest<Notification>();
            SubmitCommand = new DelegateCommand(Submit, CanSubmit);
            CancelCommand = new DelegateCommand(Cancel);
        }

        public InteractionRequest<Notification> CloseDialogRequest { get; set; }

        public FirmwareSelectorViewModel Selector { get; private set; }

        public DelegateCommand SubmitCommand { get; private set; }
        public DelegateCommand CancelCommand { get; private set; }

        public FirmwarePackageViewModel SelectedPackage { get; private set; }

        private void SelectorOnSelectedPackageChanged(object Sender, EventArgs EventArgs) { SubmitCommand.RaiseCanExecuteChanged(); }

        private bool CanSubmit() { return Selector.IsPackageSelected; }

        private void Submit()
        {
            SelectedPackage = Selector.SelectedPackage;
            CloseDialogRequest.Raise(new Notification());
        }

        private void Cancel()
        {
            Selector.SelectedPackage = SelectedPackage;
            CloseDialogRequest.Raise(new Notification());
        }
    }
}
