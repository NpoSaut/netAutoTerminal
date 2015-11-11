using System.Windows.Input;
using FirmwarePacker.LaunchParameters;
using FirmwarePacker.Project;
using FirmwarePacker.Shared;
using FirmwarePacker.TriggerActions.Notifications;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Interactivity.InteractionRequest;

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
            SaveFileRequest = new InteractionRequest<SaveFileInteractionContext>();
        }

        public FirmwareVersionViewModel Version { get; private set; }
        public ProjectViewModel Project { get; private set; }
        public ICommand SaveCommand { get; private set; }
        public InteractionRequest<SaveFileInteractionContext> SaveFileRequest { get; private set; }

        private bool Verify() { return Project.Check(); }

        private void Save()
        {
            SaveFileRequest.Raise(new SaveFileInteractionContext(new SaveFileRequestArguments("Куда сохранить?", _savingTool.FileExtension)
                                                                 {
                                                                     DefaultFileName = _launchParameters.OutputFileName,
                                                                     FileTypes = new[]
                                                                                 {
                                                                                     new FileRequestArguments.FileTypeDescription(_savingTool.FileExtension,
                                                                                                                                  "Файл пакета прошивок")
                                                                                 }
                                                                 }),
                                  OnFileSelected);
        }

        private void OnFileSelected(SaveFileInteractionContext InteractionContext)
        {
            if (InteractionContext.FileName == null)
                return;
            SavePackage(InteractionContext.FileName);
        }

        private void SavePackage(string FileName)
        {
            PackageProject model = Project.GetModel();
            _savingTool.SavePackage(model, Version.GetModel(), FileName);
        }
    }
}
