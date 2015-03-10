using System;
using System.Windows.Input;
using FirmwareBurner.Events;
using FirmwareBurner.ViewModels.Bases;
using FirmwarePacking.SystemsIndexes;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Events;

namespace FirmwareBurner.ViewModels
{
    public class TargetPresenterViewModel : ViewModelBase
    {
        private readonly IIndexHelper _indexHelper;
        private readonly TargetSelectedEvent _targetSelectedEvent;

        public TargetPresenterViewModel(IEventAggregator EventAggregator, IIndexHelper IndexHelper)
        {
            _indexHelper = IndexHelper;
            _targetSelectedEvent = EventAggregator.GetEvent<TargetSelectedEvent>();
            _targetSelectedEvent.Subscribe(OnTargetSelected);

            ClearSelectionCommand = new DelegateCommand(ClearSelection);
        }

        public bool IsTargetSelected { get; private set; }

        public String CellName { get; private set; }

        public String ModificationName { get; private set; }

        public ICommand ClearSelectionCommand { get; private set; }

        private void ClearSelection() { _targetSelectedEvent.Publish(TargetSelectedArgs.Unselected); }

        private void OnTargetSelected(TargetSelectedArgs Target)
        {
            if (Target.IsUnselected)
            {
                CellName = string.Empty;
                ModificationName = string.Empty;
                IsTargetSelected = false;
            }
            else
            {
                BlockKind cell = _indexHelper.GetCell(Target.CellKindId);
                CellName = cell.Name;
                ModificationName = cell.Modifications.Count <= 1
                                       ? string.Empty
                                       : _indexHelper.GetModification(cell, Target.ModificationId).Name;
                IsTargetSelected = true;
            }
            RaisePropertyChanged(string.Empty);
        }
    }
}
