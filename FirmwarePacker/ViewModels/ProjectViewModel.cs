using FirmwarePacker.Project;

namespace FirmwarePacker.ViewModels
{
    public class ProjectViewModel : ViewModel
    {
        private readonly PackageProject _project;
        public ProjectViewModel(PackageProject Project) { _project = Project; }

        public bool Check() { return true; }
        public PackageProject GetModel() { return _project; }
    }
}
