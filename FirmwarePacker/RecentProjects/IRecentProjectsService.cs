using System.Collections.Generic;

namespace FirmwarePacker.RecentProjects
{
    public interface IRecentProjectsService
    {
        IEnumerable<RecentProject> GetRecentProjects();
        void UpdateRecentProject(RecentProject Project);
    }
}
