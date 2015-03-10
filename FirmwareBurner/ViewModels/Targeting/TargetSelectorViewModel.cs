using System.Collections.Generic;
using FirmwareBurner.Events;
using FirmwareBurner.ViewModels.Bases;
using FirmwareBurner.ViewModels.Tools;
using Microsoft.Practices.Prism.Events;

namespace FirmwareBurner.ViewModels.Targeting
{
    /// <summary>Модель представления выбора цели прошивки</summary>
    public class TargetSelectorViewModel : ViewModelBase
    {
        private readonly IEventAggregator _eventAggregator;

        public TargetSelectorViewModel(ICellsCatalogProvider CellsCatalogProvider, IEventAggregator EventAggregator)
        {
            _eventAggregator = EventAggregator;
            CellKinds = CellsCatalogProvider.GetCatalog();
            foreach (CellKindViewModel cellKind in CellKinds)
                cellKind.TargetSelected += CellKindOnTargetSelected;
        }

        /// <summary>Известные типы ячеек</summary>
        public IList<CellKindViewModel> CellKinds { get; private set; }

        private void CellKindOnTargetSelected(object Sender, TargetSelectedEventArgs e)
        {
            _eventAggregator.GetEvent<TargetSelectedEvent>().Publish(
                new TargetSelectedArgs(e.Cell.Id, e.Modification.Id));
        }
    }
}
