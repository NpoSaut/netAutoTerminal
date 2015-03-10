﻿using Microsoft.Practices.Prism.Events;

namespace FirmwareBurner.Events
{
    public class TargetSelectedEvent : CompositePresentationEvent<TargetSelectedArgs> { }

    public class TargetSelectedArgs
    {
        public TargetSelectedArgs(int CellKindId, int ModificationId)
        {
            this.CellKindId = CellKindId;
            this.ModificationId = ModificationId;
        }

        public int CellKindId { get; private set; }
        public int ModificationId { get; private set; }

        public bool IsUnselected
        {
            get { return CellKindId == -1 || ModificationId == -1; }
        }

        public static TargetSelectedArgs Unselected
        {
            get { return new TargetSelectedArgs(-1, -1); }
        }
    }
}
