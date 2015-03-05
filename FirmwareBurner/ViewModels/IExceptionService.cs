using System;
using System.Collections.Generic;
using System.Linq;
using FirmwareBurner.ViewModels.Dialogs;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Interactivity.InteractionRequest;

namespace FirmwareBurner.ViewModels
{
    public interface IExceptionService
    {
        InteractionRequest<RequestDialogInteractionContext<ExceptionDialogViewModel>> DialogInteractionRequest { get; }

        void PublishException(string Title, Exception exc);
    }

    public class DispatchedExceptionService : IExceptionService
    {
        private readonly IDispatcherFacade _dispatcherFacade;

        public DispatchedExceptionService(IDispatcherFacade DispatcherFacade)
        {
            _dispatcherFacade = DispatcherFacade;
            DialogInteractionRequest = new InteractionRequest<RequestDialogInteractionContext<ExceptionDialogViewModel>>();
        }

        public InteractionRequest<RequestDialogInteractionContext<ExceptionDialogViewModel>> DialogInteractionRequest { get; private set; }

        public void PublishException(string Title, Exception exc)
        {
            var vm = new ExceptionDialogViewModel(Title, exc.Message, GetDetails(exc));
            _dispatcherFacade.BeginInvoke((Action<RequestDialogInteractionContext<ExceptionDialogViewModel>>)(DialogInteractionRequest.Raise),
                                          new RequestDialogInteractionContext<ExceptionDialogViewModel>(vm) { Title = Title });
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
