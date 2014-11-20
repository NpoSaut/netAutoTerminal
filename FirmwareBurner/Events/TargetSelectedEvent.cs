using Microsoft.Practices.Prism.Events;

namespace FirmwareBurner.Events
{
    public class TargetSelectedEvent : CompositePresentationEvent<TargetSelectedArgs> { }

    public class TargetSelectedArgs
    {
        public TargetSelectedArgs(int CellKindId, int ModificationId, int ChannelsCount)
        {
            this.ChannelsCount = ChannelsCount;
            this.CellKindId = CellKindId;
            this.ModificationId = ModificationId;
        }

        public int CellKindId { get; private set; }
        public int ModificationId { get; private set; }
        public int ChannelsCount { get; private set; }
    }
}
