using System;
using System.Collections.Generic;
using System.Linq;
using FirmwareBurner.Events;
using FirmwareBurner.ViewModels.Dialogs;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Interactivity.InteractionRequest;

namespace FirmwareBurner.ViewModels
{
    public class EventAggregatorExceptionDialogSource : IExceptionDialogSource
    {
        private readonly IDispatcherFacade _dispatcherFacade;

        public EventAggregatorExceptionDialogSource(IEventAggregator EventAggregator, IDispatcherFacade DispatcherFacade)
        {
            _dispatcherFacade = DispatcherFacade;
            DialogInteractionRequest = new InteractionRequest<RequestDialogInteractionContext<ExceptionDialogViewModel>>();

            EventAggregator.GetEvent<HandledExceptionEvent>().Subscribe(OnExceptionHandled);
        }

        public InteractionRequest<RequestDialogInteractionContext<ExceptionDialogViewModel>> DialogInteractionRequest { get; private set; }

        private void OnExceptionHandled(HandledExceptionArgs ExceptionArgs)
        {
            var vm = new ExceptionDialogViewModel(ExceptionArgs.Title, ExceptionArgs.Exception.Message, GetDetails(ExceptionArgs.Exception));
            _dispatcherFacade.BeginInvoke((Action<RequestDialogInteractionContext<ExceptionDialogViewModel>>)(DialogInteractionRequest.Raise),
                                          new RequestDialogInteractionContext<ExceptionDialogViewModel>(vm) { Title = ExceptionArgs.Title });
        }

        private string GetDetails(Exception Exc)
        {
            return string.Join("-------------------------------------" + Environment.NewLine,
                               DigInnerExceptions(Exc).Select(exc => exc.ToString()));
        }

        private static IEnumerable<Exception> DigInnerExceptions(Exception Exc)
        {
            Exception currentException = Exc;
            while (currentException != null)
            {
                yield return currentException;
                currentException = currentException.InnerException;
            }
        }
    }
}
