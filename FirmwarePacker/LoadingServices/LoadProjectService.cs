﻿using System;
using System.IO;
using FirmwarePacker.Exceptions;
using FirmwarePacker.Project;
using FirmwarePacker.Project.Serializers;
using FirmwarePacker.TriggerActions.Notifications;
using FirmwarePacker.ViewModels;
using FirmwarePacker.ViewModels.Factories;
using FirmwarePacking.Annotations;
using Microsoft.Practices.Prism.Interactivity.InteractionRequest;

namespace FirmwarePacker.LoadingServices
{
    [UsedImplicitly]
    public class LoadProjectService : ILoadProjectService
    {
        private readonly IProjectSerializer _projectSerializer;
        private readonly ProjectViewModelFactory _projectViewModelFactory;

        public LoadProjectService(IProjectSerializer ProjectSerializer, ProjectViewModelFactory ProjectViewModelFactory)
        {
            _projectSerializer = ProjectSerializer;
            _projectViewModelFactory = ProjectViewModelFactory;

            OpenFileRequest = new InteractionRequest<OpenFileInteractionContext>();
        }

        public InteractionRequest<OpenFileInteractionContext> OpenFileRequest { get; private set; }

        public void RequestLoadProject(Action<ProjectViewModel> CallbackAction)
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
            PackageProject project;
            try
            {
                project = _projectSerializer.Load(FileName);
            }
            catch (Exception exception)
            {
                throw new BadProjectFileException(exception);
            }
            return _projectViewModelFactory.GetInstance(project, FileName, Path.GetDirectoryName(FileName), this);
        }
    }
}