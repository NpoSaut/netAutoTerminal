using System;
using System.ComponentModel;
using FirmwareBurner.ViewModels.Bases;
using FirmwareBurner.ViewModels.Targeting;

namespace FirmwareBurner.ViewModels
{
    public class ProjectViewModel : ViewModelBase
    {
        public ProjectViewModel(int CellKindId, int CellModificationId,
                                BlockDetailsViewModel BlockDetails,
                                FirmwareSetConstructorViewModel FirmwareSetConstructor)
        {
            this.CellKindId = CellKindId;
            this.CellModificationId = CellModificationId;
            this.FirmwareSetConstructor = FirmwareSetConstructor;
            this.BlockDetails = BlockDetails;

            FirmwareSetConstructor.SomethingChanged += FirmwareSetConstructorOnSomethingChanged;
            BlockDetails.AssemblyDate.PropertyChanged += BlockDetailsOnPropertyChanged;
            BlockDetails.SerialNumber.PropertyChanged += BlockDetailsOnPropertyChanged;
        }

        public int CellKindId { get; private set; }
        public int CellModificationId { get; private set; }

        public BlockDetailsViewModel BlockDetails { get; private set; }
        public FirmwareSetConstructorViewModel FirmwareSetConstructor { get; private set; }
        private void BlockDetailsOnPropertyChanged(object Sender, PropertyChangedEventArgs PropertyChangedEventArgs) { OnProjectChanged(); }

        private void FirmwareSetConstructorOnSomethingChanged(object Sender, EventArgs Args) { OnProjectChanged(); }

        public event EventHandler ProjectChanged;

        protected virtual void OnProjectChanged()
        {
            EventHandler handler = ProjectChanged;
            if (handler != null) handler(this, EventArgs.Empty);
        }
    }
}
