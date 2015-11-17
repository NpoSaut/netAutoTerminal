using System;
using System.IO;
using FirmwarePacker.Project.Serializers;
using FirmwarePacker.TriggerActions.Notifications;
using FirmwarePacker.ViewModels;
using FirmwarePacker.ViewModels.Factories;
using Microsoft.Practices.Prism.Interactivity.InteractionRequest;

namespace FirmwarePacker
{
    public class LoadProjectService : ILoadProjectService
    {
        private readonly IProjectSerializer _projectSerializer;
        private readonly ProjectViewModelFactory _projectViewModelFactory;

        public LoadProjectService(IProjectSerializer ProjectSerializer, ProjectViewModelFactory ProjectViewModelFactory)
        {
            _projectSerializer = ProjectSerializer;
            _projectViewModelFactory = ProjectViewModelFactory;
        }

        public void RequestLoadProject(InteractionRequest<OpenFileInteractionContext> OpenFileRequest, Action<ProjectViewModel> CallbackAction)
        {
            OpenFileRequest.Raise(
                new OpenFileInteractionContext(
                    new OpenFileRequestArguments(_projectSerializer.FileExtension,
                                                 new FileRequestArguments.FileTypeDescription(_projectSerializer.FileExtension, "Проект пакета прошивок"))),
                c =>
                {
                    if (c.FileName != null)
                        CallbackAction(LoadProject(c.FileName));
                });
        }

        public ProjectViewModel LoadProject(string FileName)
        {
            return _projectViewModelFactory.GetInstance(_projectSerializer.Load(FileName),
                                                        FileName, Path.GetDirectoryName(FileName), this);
        }
    }
}
