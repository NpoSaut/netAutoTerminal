using System;
using Microsoft.Practices.Prism.Interactivity.InteractionRequest;

namespace FirmwarePacker.TriggerActions.Notifications
{
    public class SaveFileInteractionContext : Notification
    {
        public SaveFileInteractionContext(SaveFileRequestArguments RequestArguments) { this.RequestArguments = RequestArguments; }

        public SaveFileRequestArguments RequestArguments { get; private set; }
        public String FileName { get; set; }
    }
}
