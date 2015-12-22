using System;
using System.Globalization;
using System.Linq;
using FirmwarePacker.Project;
using FirmwarePacking.SystemsIndexes;

namespace FirmwarePacker.Enpacking
{
    public class VariablesProcessor : IVariablesProcessor
    {
        private readonly IIndexHelper _indexHelper;
        public VariablesProcessor(IIndexHelper IndexHelper) { _indexHelper = IndexHelper; }

        public string ReplaceVariables(string Source, PackageProject Project, PackageVersion Version)
        {
            ComponentProjectTarget sampleTarget = Project.Components.First().Targets.First();

            return Source.Replace("{version-major}", Version.Major.ToString(CultureInfo.InvariantCulture))
                         .Replace("{version-minor}", Version.Minor.ToString(CultureInfo.InvariantCulture))
                         .Replace("{version-label}", Version.Label)
                         .Replace("{release-date}", Version.ReleaseDate.ToString("yyyy.MM.dd"))
                         .Replace("{version}", CombineVersion(Version))
                         .Replace("{cell}", _indexHelper.GetCellName(sampleTarget.Cell))
                         .Replace("{module}", _indexHelper.GetModuleName(sampleTarget.Cell, sampleTarget.Module))
                         .Replace("{modification}", _indexHelper.GetModificationName(sampleTarget.Cell, sampleTarget.Modification));
        }

        private string CombineVersion(PackageVersion Version)
        {
            return String.Format(string.IsNullOrWhiteSpace(Version.Label) ? "{0}.{1}" : "{0}.{1} {2}",
                                 Version.Major, Version.Minor, Version.Label);
        }
    }
}
