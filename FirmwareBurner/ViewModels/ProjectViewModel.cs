using FirmwareBurner.ViewModels.Bases;
using FirmwareBurner.ViewModels.FirmwareSources;
using FirmwareBurner.ViewModels.Targeting;

namespace FirmwareBurner.ViewModels
{
    public class ProjectViewModel : ViewModelBase
    {
        public ProjectViewModel(BlockDetailsViewModel BlockDetails, FirmwareSelectorViewModel FirmwareSelectorViewModel)
        {
            this.FirmwareSelectorViewModel = FirmwareSelectorViewModel;
            this.BlockDetails = BlockDetails;
        }

        public BlockDetailsViewModel BlockDetails { get; private set; }
        public FirmwareSelectorViewModel FirmwareSelectorViewModel { get; private set; }
    }
}
