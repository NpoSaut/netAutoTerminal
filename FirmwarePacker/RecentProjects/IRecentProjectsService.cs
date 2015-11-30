using System.Collections.Generic;

namespace FirmwarePacker.RecentProjects
{
    public interface IRecentProjectsService
    {
        IEnumerable<RecentProject> GetRecentProjects();
        void RemoveRecentProject(string Path);
        void UpdateRecentProject(RecentProject Project);
    }
}
