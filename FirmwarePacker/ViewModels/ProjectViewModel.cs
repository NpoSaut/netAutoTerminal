using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Input;
using FirmwarePacker.Project;
using FirmwarePacker.Project.Serializers;
using FirmwarePacker.TriggerActions.Notifications;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Interactivity.InteractionRequest;

namespace FirmwarePacker.ViewModels
{
    public class ProjectViewModel : ViewModel
    {
        private readonly ProjectInformationViewModelFactory _informationViewModelFactory;
        private readonly IProjectSerializer _projectSerializer;
        private ProjectInformationViewModel _information;
        private PackageProject _project;
        private string _projectRoot;

        public ProjectViewModel(string ProjectFileName, IProjectSerializer ProjectSerializer, ProjectInformationViewModelFactory InformationViewModelFactory,
                                string ProjectRoot)
        {
            _projectSerializer = ProjectSerializer;
            _informationViewModelFactory = InformationViewModelFactory;
            _projectRoot = ProjectRoot;
            LoadProjectCommand = new DelegateCommand(BeginLoadProject);
            OpenFileRequest = new InteractionRequest<OpenFileInteractionContext>();

            if (!string.IsNullOrWhiteSpace(ProjectFileName))
                LoadProject(ProjectFileName);
            else
                _project = new PackageProject(new Collection<ComponentProject>());
        }

        public ProjectInformationViewModel Information
        {
            get { return _information; }
            private set
            {
                if (Equals(value, _information)) return;
                _information = value;
                RaisePropertyChanged("Information");
            }
        }

        public ICommand LoadProjectCommand { get; private set; }
        public InteractionRequest<OpenFileInteractionContext> OpenFileRequest { get; private set; }

        public string ProjectRoot
        {
            get { return _projectRoot; }
        }

        private void BeginLoadProject()
        {
            OpenFileRequest.Raise(new OpenFileInteractionContext(new OpenFileRequestArguments("Открыть файл проекта", _projectSerializer.FileExtension)
                                                                 {
                                                                     FileTypes = new[]
                                                                                 {
                                                                                     new FileRequestArguments.FileTypeDescription(
                                                                                         _projectSerializer.FileExtension,
                                                                                         "Проект пакета прошивок")
                                                                                 }
                                                                 }),
                                  c => LoadProject(c.FileName));
        }

        private void LoadProject(string ProjectFileName)
        {
            if (ProjectFileName != null)
            {
                _project = _projectSerializer.Load(ProjectFileName);
                _projectRoot = Path.GetDirectoryName(ProjectFileName);
                Information = _informationViewModelFactory.GetViewModel(ProjectFileName, _project);
            }
        }

        public bool Check() { return true; }
        public PackageProject GetModel() { return _project; }
    }
}
