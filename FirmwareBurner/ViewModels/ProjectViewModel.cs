using FirmwareBurner.ViewModels.Bases;
using FirmwareBurner.ViewModels.Targeting;

namespace FirmwareBurner.ViewModels
{
    public class ProjectViewModel : ViewModelBase
    {
        public ProjectViewModel(BlockDetailsViewModel BlockDetails, FirmwareSetConstructorViewModel FirmwareSetConstructor)
        {
            this.FirmwareSetConstructor = FirmwareSetConstructor;
            this.BlockDetails = BlockDetails;
        }

        public BlockDetailsViewModel BlockDetails { get; private set; }
        public FirmwareSetConstructorViewModel FirmwareSetConstructor { get; private set; }
    }
}
