using FirmwareBurner.ViewModels.Bases;
using FirmwareBurner.ViewModels.FirmwareSources;
using FirmwareBurner.ViewModels.Targeting;
using FirmwareBurner.ViewModels.Tools;

namespace FirmwareBurner.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public MainViewModel(RepoFirmwareSource AutoFirmwareSource, ManualFirmwareSource UserFirmwareSource, ProjectViewModel Project,
                             ICellsCatalogProvider CellsCatalogProvider)
        {
            this.Project = Project;
            this.AutoFirmwareSource = AutoFirmwareSource;
            this.UserFirmwareSource = UserFirmwareSource;
            CellsCatalog = CellsCatalogProvider.GetCatalog();
        }

        public ProjectViewModel Project { get; private set; }

        public ICellsCatalog CellsCatalog { get; private set; }
        public RepoFirmwareSource AutoFirmwareSource { get; private set; }
        public ManualFirmwareSource UserFirmwareSource { get; private set; }
    }
}
