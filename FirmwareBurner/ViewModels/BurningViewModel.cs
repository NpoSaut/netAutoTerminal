using System;
using System.Collections.Generic;
using System.Linq;
using FirmwareBurner.Burning;
using FirmwareBurner.Project;
using FirmwareBurner.ViewModels.Bases;
using FirmwarePacking.SystemsIndexes;

namespace FirmwareBurner.ViewModels
{
    public class BurningViewModel : ViewModelBase
    {
        private readonly IProjectAssembler _projectAssembler;

        public BurningViewModel(IProjectAssembler ProjectAssembler, ChannelSelectorViewModel ChannelSelector, IList<BurningVariantViewModel> BurningVariants)
        {
            this.ChannelSelector = ChannelSelector;
            this.BurningVariants = BurningVariants;
            _projectAssembler = ProjectAssembler;
            foreach (BurningVariantViewModel burningVariant in BurningVariants)
                burningVariant.Activated += BurningVariantOnActivated;
        }

        public ChannelSelectorViewModel ChannelSelector { get; private set; }

        /// <summary>Способ прошивки по-умолчанию</summary>
        public IList<BurningVariantViewModel> BurningVariants { get; private set; }

        /// <summary>Варианты способов прошивки</summary>
        public BurningVariantViewModel DefaultVariant
        {
            get { return BurningVariants.FirstOrDefault(v => v.IsDefault); }
        }

        private void BurningVariantOnActivated(object Sender, BurningVariantActivatedEventArgs e)
        {
            FirmwareProject project = _projectAssembler.GetProject(ChannelSelector.SelectedChannel.Number);
            e.BurningReceipt.Burn(project);
        }
    }

    public interface IBurningViewModelProvider
    {
        BurningViewModel GetViewModel(int CellKindId, int ModificationId, IProjectAssembler projectAssembler);
    }

    public class BurningViewModelProvider : IBurningViewModelProvider
    {
        private readonly IBurningReceiptsCatalog _burningReceiptsCatalog;
        private readonly IIndex _index;

        public BurningViewModelProvider(IBurningReceiptsCatalog BurningReceiptsCatalog, IIndex Index)
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
