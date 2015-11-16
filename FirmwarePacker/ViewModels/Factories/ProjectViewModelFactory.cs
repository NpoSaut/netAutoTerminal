using System.Collections.Generic;
using System.Linq;
using FirmwarePacker.LaunchParameters;
using FirmwarePacker.Project;
using FirmwarePacker.Project.Serializers;
using FirmwarePacking.Annotations;
using FirmwarePacking.SystemsIndexes;
using Microsoft.Practices.Prism.Events;

namespace FirmwarePacker.ViewModels.Factories
{
    [UsedImplicitly]
    public class ProjectViewModelFactory
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IIndexHelper _indexHelper;

        public ProjectViewModelFactory(IProjectSerializer ProjectSerializer, ILaunchParameters LaunchParameters, IIndexHelper IndexHelper,
                                       IEventAggregator EventAggregator)
        {
            _indexHelper = IndexHelper;
            _eventAggregator = EventAggregator;
        }

        public ProjectViewModel GetInstance(PackageProject Project, string ProjectFileName, string RootDirectory, ILoadProjectService LoadProjectService)
        {
            List<TargetViewModel> targets = Project.Components.SelectMany(c =>
                                                                          c.Targets.Select(t => new TargetViewModel(_indexHelper.GetCellName(t.Cell),
                                                                                                                    _indexHelper.GetModuleName(t.Cell, t.Module))))
                                                   .Distinct(TargetViewModel.Comparer)
                                                   .ToList();
            return new ProjectViewModel(Project, ProjectFileName, RootDirectory,
                                        targets, LoadProjectService, _eventAggregator);
        }
    }
}
