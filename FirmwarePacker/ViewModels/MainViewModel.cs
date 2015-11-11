using System.Windows.Input;
using FirmwarePacker.LaunchParameters;
using FirmwarePacker.Project;
using FirmwarePacker.Shared;
using Microsoft.Practices.Prism.Commands;

namespace FirmwarePacker.ViewModels
{
    public class MainViewModel : ViewModel
    {
        private readonly ILaunchParameters _launchParameters;
        private readonly IPackageSavingTool _savingTool;

        public MainViewModel(FirmwareVersionViewModel Version, ProjectViewModel Project, IPackageSavingTool SavingTool, ILaunchParameters LaunchParameters)
        {
            this.Project = Project;
            _savingTool = SavingTool;
            _launchParameters = LaunchParameters;
            this.Version = Version;
            SaveCommand = new DelegateCommand(Save, Verify);
        }

        public FirmwareVersionViewModel Version { get; private set; }
        public ProjectViewModel Project { get; private set; }
        public ICommand SaveCommand { get; private set; }

        private bool Verify() { return Project.Check(); }

        private void Save()
        {
            PackageProject model = Project.GetModel();
            _savingTool.SavePackage(model, Version.GetModel(), _launchParameters.OutputFileName);
        }
    }
}
