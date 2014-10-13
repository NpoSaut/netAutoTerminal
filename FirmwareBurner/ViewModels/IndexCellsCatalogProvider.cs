using System.Collections.Generic;
using System.Linq;
using FirmwarePacking.SystemsIndexes;

namespace FirmwareBurner.ViewModels
{
    /// <summary>Провайдер каталога ячеек, основывающийся на объекте интерфейса <seealso cref="IIndex" />
    /// </summary>
    public class IndexCellsCatalogProvider : ICellsCatalogProvider
    {
        private readonly IIndex _index;
        public IndexCellsCatalogProvider(IIndex Index) { _index = Index; }

        public ICellsCatalog GetCatalog()
        {
            return
                new CellsCatalog(
                    _index.Blocks.Select(
                        cell => new CellKindViewModel(
                                    cell.Id,
                                    cell.Name,
                                    cell.Modifications.Select(
                                        modification => new ModificationKindViewModel(
                                                            modification.Id,
                                                            modification.Name)).ToList(),
                                    Enumerable.Range(0, cell.ChannelsCount).Select(i => new ChannelViewModel(i + 1)).ToList()))
                          .ToList());
        }

        public class CellsCatalog : ICellsCatalog
        {
            public CellsCatalog(IList<CellKindViewModel> CellKinds) { this.CellKinds = CellKinds; }
            public IList<CellKindViewModel> CellKinds { get; private set; }
        }
    }
}