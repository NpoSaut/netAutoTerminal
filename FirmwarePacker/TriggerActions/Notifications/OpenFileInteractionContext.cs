using System;
using Microsoft.Practices.Prism.Interactivity.InteractionRequest;

namespace FirmwarePacker.TriggerActions.Notifications
{
    public class OpenFileInteractionContext : Notification
    {
        public OpenFileInteractionContext(OpenFileRequestArguments RequestArguments) { this.RequestArguments = RequestArguments; }

        public OpenFileRequestArguments RequestArguments { get; private set; }
        public String FileName { get; set; }
    }
}