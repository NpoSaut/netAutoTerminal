using FirmwarePacker.ViewModels;
using Microsoft.Practices.Prism.Events;

namespace FirmwarePacker.Events
{
    public class ProjectLoadedEvent : CompositePresentationEvent<ProjectLoadedEvent.Payload>
    {
        public class Payload
        {
            public Payload(ProjectViewModel Project) { this.Project = Project; }
            public ProjectViewModel Project { get; private set; }
        }
    }
}