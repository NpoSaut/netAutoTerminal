using System.Windows.Input;
using FirmwarePacker.Enpacking;
using FirmwarePacker.LaunchParameters;
using FirmwarePacker.Project;
using FirmwarePacker.TriggerActions.Notifications;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Interactivity.InteractionRequest;

namespace FirmwarePacker.ViewModels
{
    public class MainViewModel : ViewModel
    {
        private readonly ILaunchParameters _launchParameters;
        private readonly IPackageSavingTool _savingTool;
        private readonly IVariablesProcessor _variablesProcessor;

        public MainViewModel(FirmwareVersionViewModel Version, ProjectViewModel Project, IPackageSavingTool SavingTool, ILaunchParameters LaunchParameters,
                             IVariablesProcessor VariablesProcessor)
        {
            this.Project = Project;
            _savingTool = SavingTool;
            _launchParameters = LaunchParameters;
            _variablesProcessor = VariablesProcessor;
            this.Version = Version;
            SaveCommand = new DelegateCommand(BeginSave, Verify);
            SaveFileRequest = new InteractionRequest<SaveFileInteractionContext>();
        }

        public FirmwareVersionViewModel Version { get; private set; }
        public ProjectViewModel Project { get; private set; }
        public ICommand SaveCommand { get; private set; }
        public InteractionRequest<SaveFileInteractionContext> SaveFileRequest { get; private set; }

        private bool Verify() { return Project.Check(); }

        private void BeginSave()
        {
            SaveFileRequest.Raise(new SaveFileInteractionContext(new SaveFileRequestArguments(_savingTool.FileExtension,
                                                                                              new FileRequestArguments.FileTypeDescription(
                                                                                                  _savingTool.FileExtension, "Файл пакета прошивок"))
                                                                 {
                                                                     DefaultFileName = _variablesProcessor.ReplaceVariables(_launchParameters.OutputFileName
                                                                                                                            ?? "{cell} ver. {version}.sfp",
                                                                                                                            Project.GetModel(),
                                                                                                                            Version.GetModel())
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
            _savingTool.SavePackage(model, Version.GetModel(), FileName, Project.ProjectRoot);
        }
    }
}
