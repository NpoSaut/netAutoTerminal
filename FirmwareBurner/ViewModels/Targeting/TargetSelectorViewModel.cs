using System.Collections.Generic;
using System.Windows.Input;
using FirmwareBurner.Events;
using FirmwareBurner.ViewModels.Bases;
using FirmwareBurner.ViewModels.Tools;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Events;

namespace FirmwareBurner.ViewModels.Targeting
{
    /// <summary>Модель представления выбора цели прошивки</summary>
    public class TargetSelectorViewModel : ViewModelBase
    {
        private readonly TargetSelectedEvent _targetSelectedEvent;

        public TargetSelectorViewModel(ICellsCatalogProvider CellsCatalogProvider, IEventAggregator EventAggregator)
        {
            _targetSelectedEvent = EventAggregator.GetEvent<TargetSelectedEvent>();
            CellKinds = CellsCatalogProvider.GetCatalog();
            foreach (CellKindViewModel cellKind in CellKinds)
                cellKind.TargetSelected += CellKindOnTargetSelected;

            ClearSelectionCommand = new DelegateCommand(ClearSelection);
        }

        /// <summary>Известные типы ячеек</summary>
        public IList<CellKindViewModel> CellKinds { get; private set; }

        public ICommand ClearSelectionCommand { get; private set; }

        private void ClearSelection() { _targetSelectedEvent.Publish(TargetSelectedArgs.Unselected); }

        private void CellKindOnTargetSelected(object Sender, TargetSelectedEventArgs e)
        {
            _targetSelectedEvent.Publish(new TargetSelectedArgs(e.Cell.Id, e.Modification.Id));
        }
    }
}
