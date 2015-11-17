using System.Collections.Generic;
using System.Linq;
using FirmwarePacker.Properties;
using FirmwarePacking.Annotations;

namespace FirmwarePacker.RecentProjects
{
    [UsedImplicitly]
    internal class SettingsRecentProjectsService : IRecentProjectsService
    {
        private const int StoringCount = 10;

        public IEnumerable<RecentProject> GetRecentProjects() { return Settings.Default.LastProjects ?? Enumerable.Empty<RecentProject>(); }

        public void UpdateRecentProject(RecentProject Record)
        {
            if (Settings.Default.LastProjects == null)
                Settings.Default.LastProjects = new RecentProjectsList();
            Settings.Default.LastProjects.RemoveAll(p => p.FileName == Record.FileName);
            Settings.Default.LastProjects.Insert(0, Record);

            //if (Settings.Default.LastProjects.Count > StoringCount)
            //    Settings.Default.LastProjects.RemoveRange(StoringCount, Settings.Default.LastProjects.Count);

            Settings.Default.Save();
        }
    }
}
