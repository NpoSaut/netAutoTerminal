using System.Linq;
using FirmwarePacker.Project;
using FirmwarePacking.Annotations;
using FirmwarePacking.SystemsIndexes;

namespace FirmwarePacker.ViewModels.Factories
{
    [UsedImplicitly]
    public class ProjectInformationViewModelFactory
    {
        private readonly IIndexHelper _indexHelper;
        public ProjectInformationViewModelFactory(IIndexHelper IndexHelper) { _indexHelper = IndexHelper; }

        public ProjectInformationViewModel GetViewModel(string FileName, PackageProject Project)
        {
            return new ProjectInformationViewModel(
                FileName,
                Project.Components.SelectMany(c => c.Targets.Select(t => new TargetViewModel(_indexHelper.GetCellName(t.Cell),
                                                                                             _indexHelper.GetModuleName(t.Cell, t.Module))))
                       .Distinct(TargetViewModel.Comparer)
                       .ToList());
        }
    }
}