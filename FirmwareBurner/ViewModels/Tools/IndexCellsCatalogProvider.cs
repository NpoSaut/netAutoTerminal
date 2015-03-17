using System.Collections.Generic;
using System.Linq;
using FirmwareBurner.Burning;
using FirmwareBurner.ViewModels.Targeting;
using FirmwarePacking.SystemsIndexes;

namespace FirmwareBurner.ViewModels.Tools
{
    /// <summary>Провайдер каталога ячеек, основывающийся на объекте интерфейса <seealso cref="IIndex" />
    /// </summary>
    public class IndexCellsCatalogProvider : ICellsCatalogProvider
    {
        private readonly IBurningReceiptsCatalog _burningReceiptsCatalog;
        private readonly IIndex _index;

        public IndexCellsCatalogProvider(IIndex Index, IBurningReceiptsCatalog BurningReceiptsCatalog)
        {
            _index = Index;
            _burningReceiptsCatalog = BurningReceiptsCatalog;
        }

        public IList<CellKindViewModel> GetCatalog()
        {
            return _index.Blocks
                         .Select(cell => new CellKindViewModel(
                                             cell.Id,
                                             cell.Name,
                                             cell.Modifications
                                                 .Select(modification => new ModificationKindViewModel(
                                                                             modification.Id,
                                                                             modification.Name,
                                                                             modification.DeviceName,
                                                                             _burningReceiptsCatalog.GetBurningReceiptFactories(modification.DeviceName).Any()))
                                                 .ToList(),
                                             Enumerable.Range(1, cell.ChannelsCount).Select(i => new ChannelViewModel(i)).ToList()))
                         .Where(cm => cm.Modifications.Any(m => m.CanBeBurned))
                         .ToList();
        }
    }
}
