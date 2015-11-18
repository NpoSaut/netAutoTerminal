using FirmwarePacker.Enpacking;
using FirmwarePacker.LaunchParameters;
using FirmwarePacker.Project;
using FirmwarePacker.TriggerActions.Notifications;
using FirmwarePacking.Annotations;
using Microsoft.Practices.Prism.Interactivity.InteractionRequest;

namespace FirmwarePacker
{
    [UsedImplicitly]
    internal class PackageSavingService : IPackageSavingService
    {
        private readonly ILaunchParameters _launchParameters;
        private readonly IPackageSavingTool _savingTool;
        private readonly IVariablesProcessor _variablesProcessor;

        public PackageSavingService(ILaunchParameters LaunchParameters, IPackageSavingTool SavingTool, IVariablesProcessor VariablesProcessor)
        {
            _launchParameters = LaunchParameters;
            _savingTool = SavingTool;
            _variablesProcessor = VariablesProcessor;

            SaveFileRequest = new InteractionRequest<SaveFileInteractionContext>();
        }

        public InteractionRequest<SaveFileInteractionContext> SaveFileRequest { get; private set; }

        public void SavePackage(PackageProject PackageProject, PackageVersion PackageVersion,
                                string ProjectRoot)
        {
            string defaultFileName = _variablesProcessor.ReplaceVariables(_launchParameters.OutputFileName ?? "{cell} ver. {version}.sfp",
                                                                          PackageProject, PackageVersion);
            SaveFileRequest.Raise(new SaveFileInteractionContext(new SaveFileRequestArguments(_savingTool.FileExtension,
                                                                                              new FileRequestArguments.FileTypeDescription(
                                                                                                  _savingTool.FileExtension, "Файл пакета прошивок"))
                                                                 {
                                                                     DefaultFileName = defaultFileName
                                                                 }),
                                  c => SavePackage(c.FileName, PackageProject, PackageVersion, ProjectRoot));
        }

        private void SavePackage(string FileName, PackageProject PackageProject, PackageVersion PackageVersion, string ProjectRoot)
        {
            if (FileName == null) return;
            _savingTool.SavePackage(PackageProject, PackageVersion, FileName, ProjectRoot);
        }
    }
}
