using FirmwareBurner.ViewModels.Bases;
using FirmwareBurner.ViewModels.Targeting;

namespace FirmwareBurner.ViewModels
{
    public class ProjectViewModel : ViewModelBase
    {
        public ProjectViewModel(TargetSelectorViewModel TargetSelector, BlockDetailsViewModel BlockDetails)
        {
            this.BlockDetails = BlockDetails;
            this.TargetSelector = TargetSelector;
        }

        public TargetSelectorViewModel TargetSelector { get; private set; }
        public BlockDetailsViewModel BlockDetails { get; private set; }
    }
}
