using System.Collections.Generic;

namespace FirmwarePacker.ViewModels
{
    public class ProjectInformationViewModel : ViewModel
    {
        public ProjectInformationViewModel(string FileName, ICollection<TargetViewModel> Targets)
        {
            this.FileName = FileName;
            this.Targets = Targets;
        }

        public string FileName { get; private set; }
        public ICollection<TargetViewModel> Targets { get; private set; }
    }
}
