using System;
using System.Linq;
using FirmwareBurner.Annotations;
using FirmwareBurner.Burning;
using FirmwarePacking.SystemsIndexes;

namespace FirmwareBurner.ViewModels
{
    [UsedImplicitly]
    public class BurningViewModelFactory : IBurningViewModelFactory
    {
        private readonly IBurningReceiptsCatalog _burningReceiptsCatalog;
        private readonly IIndex _index;

        public BurningViewModelFactory(IBurningReceiptsCatalog BurningReceiptsCatalog, IIndex Index)
        {
            _burningReceiptsCatalog = BurningReceiptsCatalog;
            _index = Index;
        }

        public BurningViewModel GetViewModel(int CellKindId, int ModificationId, IProjectAssembler projectAssembler)
        {
            BlockKind cellKind = _index.Blocks.SingleOrDefault(c => c.Id == CellKindId);
            if (cellKind == null) throw new ArgumentException("Ячейка с таким идентификатором отсутствует в каталоге", "CellKindId");

            ModificationKind modification = cellKind.Modifications.SingleOrDefault(m => m.Id == ModificationId);
            if (modification == null) throw new ArgumentException("Модификация с таким идентификатором отсутствует в каталоге", "ModificationId");

            var channelSelector = new ChannelSelectorViewModel(cellKind.ChannelsCount);

            return new BurningViewModel(projectAssembler, channelSelector,
                                        _burningReceiptsCatalog.GetBurningReceiptFactories(modification.DeviceName)
                                                               .Select(
                                                                   receiptFactory =>
                                                                   new BurningVariantViewModel(receiptFactory.GetReceipt(modification.DeviceName), true))
                                                               .ToList());
        }
    }
}