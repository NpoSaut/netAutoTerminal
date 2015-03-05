using System;
using Microsoft.Practices.Prism.Events;

namespace FirmwareBurner.Events
{
    public class HandledExceptionEvent : CompositePresentationEvent<HandledExceptionArgs> { }

    public class HandledExceptionArgs
    {
        public HandledExceptionArgs(string Title, Exception Exception)
        {
            this.Title = Title;
            this.Exception = Exception;
        }

        public String Title { get; private set; }
        public Exception Exception { get; private set; }
    }
}
