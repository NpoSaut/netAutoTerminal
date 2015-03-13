using Microsoft.Practices.Prism.Events;

namespace FirmwareBurner.Events
{
    public class ProjectChangedEvent : CompositePresentationEvent<ProjectChangedArgs> { }

    public class ProjectChangedArgs { }
}
