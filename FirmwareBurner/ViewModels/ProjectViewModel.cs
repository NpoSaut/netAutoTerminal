using System;
using System.ComponentModel;
using FirmwareBurner.Events;
using FirmwareBurner.ViewModels.Bases;
using FirmwareBurner.ViewModels.Targeting;
using Microsoft.Practices.Prism.Events;

namespace FirmwareBurner.ViewModels
{
    public class ProjectViewModel : ViewModelBase
    {
        private readonly ProjectChangedEvent _projectChangedEvent;

        public ProjectViewModel(int CellKindId, int CellModificationId,
                                BlockDetailsViewModel BlockDetails,
                                FirmwareSetConstructorViewModel FirmwareSetConstructor, IEventAggregator EventAggregator)
        {
            this.CellKindId = CellKindId;
            this.CellModificationId = CellModificationId;
            this.FirmwareSetConstructor = FirmwareSetConstructor;
            this.BlockDetails = BlockDetails;

            FirmwareSetConstructor.SomethingChanged += FirmwareSetConstructorOnSomethingChanged;
            BlockDetails.AssemblyDate.PropertyChanged += BlockDetailsOnPropertyChanged;
            BlockDetails.SerialNumber.PropertyChanged += BlockDetailsOnPropertyChanged;

            _projectChangedEvent = EventAggregator.GetEvent<ProjectChangedEvent>();
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
            _projectChangedEvent.Publish(new ProjectChangedArgs());
            EventHandler handler = ProjectChanged;
            if (handler != null) handler(this, EventArgs.Empty);
        }
    }
}
