using System;
using System.IO;
using FirmwarePacker.LoadingServices;
using FirmwarePacker.RecentProjects;
using Microsoft.Practices.Prism.Events;

namespace FirmwarePacker.ViewModels.Factories
{
    public class RecentProjectViewModelFactory
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly ILoadProjectService _loadProjectService;

        public RecentProjectViewModelFactory(IEventAggregator EventAggregator, ILoadProjectService LoadProjectService)
        {
            _eventAggregator = EventAggregator;
            _loadProjectService = LoadProjectService;
        }

        public RecentProjectViewModel GetViewModel(RecentProject ProjectRecord)
        {
            return new RecentProjectViewModel(Path.GetFileName(ProjectRecord.FileName),
                                              Path.GetDirectoryName(ProjectRecord.FileName),
                                              ProjectRecord.FileName,
                                              String.Format("{0}.{1}", ProjectRecord.MajorVersion, ProjectRecord.MinorVersion),
                                              _eventAggregator, _loadProjectService);
        }
    }
}
