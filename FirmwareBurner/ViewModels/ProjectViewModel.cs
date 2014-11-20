using FirmwareBurner.ViewModels.Bases;
using FirmwareBurner.ViewModels.Targeting;

namespace FirmwareBurner.ViewModels
{
    public class ProjectViewModel : ViewModelBase
    {
        public ProjectViewModel(int CellKindId, int CellModificationId,
                                BlockDetailsViewModel BlockDetails,
                                FirmwareSetConstructorViewModel FirmwareSetConstructor)
        {
            this.CellKindId = CellKindId;
            this.CellModificationId = CellModificationId;
            this.FirmwareSetConstructor = FirmwareSetConstructor;
            this.BlockDetails = BlockDetails;
        }

        public int CellKindId { get; private set; }
        public int CellModificationId { get; private set; }

        public BlockDetailsViewModel BlockDetails { get; private set; }
        public FirmwareSetConstructorViewModel FirmwareSetConstructor { get; private set; }
    }
}
