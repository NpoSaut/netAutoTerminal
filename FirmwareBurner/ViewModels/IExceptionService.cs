using System;

namespace FirmwareBurner.ViewModels
{
    /// <summary>Сервис обработки исключений</summary>
    /// <remarks>В этот сервис можно пожаловаться на возникшее исключение</remarks>
    public interface IExceptionService
    {
        /// <summary>Уведомляет сервис о возникшем исключении</summary>
        /// <param name="Title">Описание ситуации, в которой возникло (или к которой привело) исключение</param>
        /// <param name="exc">Само исключение</param>
        void PublishException(string Title, Exception exc);
    }
}
