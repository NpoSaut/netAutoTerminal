using FirmwareBurner.ViewModels.Dialogs;
using Microsoft.Practices.Prism.Interactivity.InteractionRequest;

namespace FirmwareBurner.ViewModels
{
    /// <summary>Источник диалогов об возникших исключениях :-/</summary>
    public interface IExceptionDialogSource
    {
        InteractionRequest<RequestDialogInteractionContext<ExceptionDialogViewModel>> DialogInteractionRequest { get; }
    }
}
