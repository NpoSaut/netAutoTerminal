using System;
using System.Collections.Generic;
using System.Linq;
using FirmwareBurner.ViewModels.Bases;
using FirmwareBurner.ViewModels.Targeting;
using FirmwarePacking.SystemsIndexes;

namespace FirmwareBurner.ViewModels
{
    public class BurningViewModel : ViewModelBase
    {
        private ChannelViewModel _selectedChannel;

        public BurningViewModel(int ChannelsCount, IList<BurningVariantViewModel> BurningVariants)
        {
            this.BurningVariants = BurningVariants;

            Channels = Enumerable.Range(0, ChannelsCount).Select(i => new ChannelViewModel(i + 1)).ToList();
            SelectedChannel = Channels.FirstOrDefault();
        }

        public IList<ChannelViewModel> Channels { get; private set; }

        public ChannelViewModel SelectedChannel
        {
            get { return _selectedChannel; }
            set
            {
                if (Equals(value, _selectedChannel)) return;
                _selectedChannel = value;
                RaisePropertyChanged("SelectedChannel");
            }
        }

        /// <summary>Способ прошивки по-умолчанию</summary>
        public IList<BurningVariantViewModel> BurningVariants { get; private set; }

        /// <summary>Варианты способов прошивки</summary>
        public BurningVariantViewModel DefaultVariant
        {
            get { return BurningVariants.FirstOrDefault(v => v.IsDefault); }
        }
    }

    public interface IBurningViewModelProvider
    {
        BurningViewModel GetViewModel(int CellKindId, int ModificationId, IProjectAssembler projectAssembler);
    }

    public class BurningViewModelProvider : IBurningViewModelProvider
    {
        private readonly IBurnersRepository _burnersRepository;
        private readonly IIndex _index;

        public BurningViewModelProvider(IBurnersRepository BurnersRepository, IIndex Index)
        {
            _burnersRepository = BurnersRepository;
            _index = Index;
        }

        public BurningViewModel GetViewModel(int CellKindId, int ModificationId, IProjectAssembler projectAssembler)
        {
            BlockKind cellKind = _index.Blocks.SingleOrDefault(c => c.Id == CellKindId);
            if (cellKind == null) throw new ArgumentException("Ячейка с таким идентификатором отсутствует в каталоге", "CellKindId");

            ModificationKind modification = cellKind.Modifications.SingleOrDefault(m => m.Id == ModificationId);
            if (modification == null) throw new ArgumentException("Модификация с таким идентификатором отсутствует в каталоге", "ModificationId");

            return new BurningViewModel(cellKind.ChannelsCount,
                                        _burnersRepository.GetBurnManagers(modification.DeviceName)
                                                          .Select(bmf => new BurningVariantViewModel(bmf.BurnerName, true, bmf, projectAssembler))
                                                          .ToList());
        }
    }
}
