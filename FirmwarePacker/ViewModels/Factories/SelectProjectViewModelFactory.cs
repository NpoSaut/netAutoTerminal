using System;
using System.Collections.Generic;
using System.Linq;
using FirmwarePacker.LoadingServices;
using FirmwarePacker.RecentProjects;
using FirmwarePacking.Annotations;
using Microsoft.Practices.Prism.Events;

namespace FirmwarePacker.ViewModels.Factories
{
    [UsedImplicitly]
    public class SelectProjectViewModelFactory
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly ILoadProjectService _loadProjectService;
        private readonly RecentProjectViewModelFactory _recentProjectViewModelFactory;
        private readonly IRecentProjectsService _recentProjectsService;

        public SelectProjectViewModelFactory(IEventAggregator EventAggregator, ILoadProjectService LoadProjectService,
                                             IRecentProjectsService RecentProjectsService, RecentProjectViewModelFactory RecentProjectViewModelFactory)
        {
            _eventAggregator = EventAggregator;
            _loadProjectService = LoadProjectService;
            _recentProjectsService = RecentProjectsService;
            _recentProjectViewModelFactory = RecentProjectViewModelFactory;
        }

        public SelectProjectViewModel GetViewModel()
        {
            return new SelectProjectViewModel(_loadProjectService, _eventAggregator,
                                              GetRecentViewModels().ToList());
        }

        private IEnumerable<RecentProjectViewModel> GetRecentViewModels()
        {
            List<RecentProject> recentProjects = _recentProjectsService.GetRecentProjects().ToList();
            foreach (RecentProject project in recentProjects)
            {
                RecentProjectViewModel viewModel = null;
                try
                {
                    viewModel = _recentProjectViewModelFactory.GetViewModel(project);
                }
                catch (Exception)
                {
                    _recentProjectsService.RemoveRecentProject(project.FileName);
                }
                if (viewModel != null)
                    yield return viewModel;
            }
        }
    }
}
