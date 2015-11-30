using System.Collections.Generic;
using System.Linq;
using FirmwarePacker.Properties;
using FirmwarePacking.Annotations;

namespace FirmwarePacker.RecentProjects
{
    [UsedImplicitly]
    internal class SettingsRecentProjectsService : IRecentProjectsService
    {
        private const int StoringCount = 20;

        public IEnumerable<RecentProject> GetRecentProjects() { return Settings.Default.LastProjects ?? Enumerable.Empty<RecentProject>(); }

        public void RemoveRecentProject(string Path) { Settings.Default.LastProjects.RemoveAll(p => p.FileName == Path); }

        public void UpdateRecentProject(RecentProject Record)
        {
            if (Settings.Default.LastProjects == null)
                Settings.Default.LastProjects = new RecentProjectsList();
            Settings.Default.LastProjects.RemoveAll(p => p.FileName == Record.FileName);
            Settings.Default.LastProjects.Insert(0, Record);
            CleanUp();

            Settings.Default.Save();
        }

        private static void CleanUp()
        {
            int count = Settings.Default.LastProjects.Count;
            if (count <= StoringCount)
                return;
            Settings.Default.LastProjects.RemoveRange(StoringCount, count - StoringCount);
        }
    }
}
