using System;
using System.Threading;
using FirmwareBurner.Interaction;
using FirmwareBurner.ViewModels.Bases;
using Microsoft.Practices.Prism.Interactivity.InteractionRequest;

namespace FirmwareBurner.ViewModels
{
    public class FileRequestServiceViewModel : ViewModelBase, IFileSelectorService
    {
        public FileRequestServiceViewModel() { SaveFileRequest = new InteractionRequest<SaveFileInteractionContext>(); }

        public InteractionRequest<SaveFileInteractionContext> SaveFileRequest { get; private set; }

        /// <summary>Спрашивает пользователя о месте сохранения файла</summary>
        /// <param name="Arguments">Аргументы запроса</param>
        /// <returns>Путь к месту расположения файла</returns>
        public string RequestSaveFileLocation(SaveFileRequestArguments Arguments)
        {
            var resetEvent = new AutoResetEvent(false);
            var context = new SaveFileInteractionContext(Arguments, resetEvent);
            SaveFileRequest.Raise(context, OnSaveInteractinCompleated);
            resetEvent.WaitOne();
            return context.FileName;
        }

        private void OnSaveInteractinCompleated(SaveFileInteractionContext Obj) { Obj.ResetEvent.Set(); }
    }

    public class SaveFileInteractionContext : Notification
    {
        public SaveFileInteractionContext(SaveFileRequestArguments RequestArguments, AutoResetEvent ResetEvent)
        {
            this.RequestArguments = RequestArguments;
            this.ResetEvent = ResetEvent;
        }

        public SaveFileRequestArguments RequestArguments { get; private set; }
        public AutoResetEvent ResetEvent { get; private set; }
        public String FileName { get; set; }
    }
}
