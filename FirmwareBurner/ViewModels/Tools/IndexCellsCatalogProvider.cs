using System.Collections.Generic;
using System.Linq;
using FirmwareBurner.ViewModels.Targeting;
using FirmwarePacking.SystemsIndexes;

namespace FirmwareBurner.ViewModels.Tools
{
    /// <summary>Провайдер каталога ячеек, основывающийся на объекте интерфейса <seealso cref="IIndex" />
    /// </summary>
    public class IndexCellsCatalogProvider : ICellsCatalogProvider
    {
        private readonly IIndex _index;
        public IndexCellsCatalogProvider(IIndex Index) { _index = Index; }

        public IList<CellKindViewModel> GetCatalog()
        {
            return _index.Blocks.Select(
                cell => new CellKindViewModel(
                            cell.Id,
                            cell.Name,
                            cell.Modifications.Select(
                                modification => new ModificationKindViewModel(
                                                    modification.Id,
                                                    modification.Name,
                                                    modification.DeviceName)).ToList(),
                            Enumerable.Range(0, cell.ChannelsCount).Select(i => new ChannelViewModel(i + 1)).ToList()))
                         .ToList();
        }
    }
}
