using FirmwareBurner.Models;
using Microsoft.Practices.Prism.Events;

namespace FirmwareBurner.Events
{
    public class TargetSelectedEvent : CompositePresentationEvent<TargetSelectedArgs> { }

    public class TargetSelectedArgs
    {
    }
}
