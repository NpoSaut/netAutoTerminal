﻿using System;

namespace FirmwareBurner.Progress
{
    /// <summary>Помогает контролировать работу с <see cref="IProgressToken" />
    /// </summary>
    /// <remarks>
    ///     <list type="bullet">
    ///         <item>Проверяет на null-значение ссылку на <see cref="IProgressToken" /></item>
    ///         <item>Запускает выполняет метод Start при создании</item>
    ///         <item>Выполняет метод OnCompleated при вызове Dispose</item>
    ///     </list>
    /// </remarks>
    /// <example>
    ///     <code>
    ///         using (IProgressController progress = _progressControllerFactory.CreateController(ProgressToken))
    ///         {
    ///             ProgressToken.SetProgress(0.35);
    ///         }
    ///     </code>
    /// </example>
    public interface IProgressController : IDisposable
    {
        /// <summary>Устанавливает текущее значение прогресса операции</summary>
        /// <param name="Progress">Доля выполнения операции (0.0 - 1.0)</param>
        void SetProgress(Double Progress);

        /// <summary>Устанавливает Intermediate-значение прогресса операции</summary>
        void SetToIntermediate();
    }
}
