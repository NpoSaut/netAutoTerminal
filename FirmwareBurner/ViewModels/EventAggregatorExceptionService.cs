using System;
using FirmwareBurner.Events;
using Microsoft.Practices.Prism.Events;

namespace FirmwareBurner.ViewModels
{
    public class EventAggregatorExceptionService : IExceptionService
    {
        private readonly IEventAggregator _eventAggregator;

        public EventAggregatorExceptionService(IEventAggregator EventAggregator) { _eventAggregator = EventAggregator; }

        /// <summary>Уведомляет сервис о возникшем исключении</summary>
        /// <param name="Title">Описание ситуации, в которой возникло (или к которой привело) исключение</param>
        /// <param name="exc">Само исключение</param>
        public void PublishException(string Title, Exception exc)
        {
            _eventAggregator.GetEvent<HandledExceptionEvent>().Publish(new HandledExceptionArgs(Title, exc));
        }
    }
}
