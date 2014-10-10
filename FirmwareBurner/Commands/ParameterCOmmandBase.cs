using System;
using System.Windows.Input;

namespace FirmwareBurner.Commands
{
    public abstract class ParameterCommandBase<TParameter> : ICommand
    {
        /// <summary>Определяет метод, вызываемый при вызове данной команды.</summary>
        /// <param name="parameter">
        ///     Данные, используемые данной командой.Если данная команда не требует передачи данных, этот
        ///     объект может быть установлен в null.
        /// </param>
        public void Execute(object parameter) { ExecutedImpl((TParameter)parameter); }

        /// <summary>Определяет метод, который определяет, может ли данная команда выполняться в ее текущем состоянии.</summary>
        /// <returns>true, если команда может быть выполнена; в противном случае — false.</returns>
        /// <param name="parameter">
        ///     Данные, используемые данной командой.Если данная команда не требует передачи данных, этот
        ///     объект может быть установлен в null.
        /// </param>
        public bool CanExecute(object parameter) { return CanExecuteImpl((TParameter)parameter); }

        public event EventHandler CanExecuteChanged;

        protected virtual void OnCanExecuteChanged()
        {
            EventHandler handler = CanExecuteChanged;
            if (handler != null) handler(this, EventArgs.Empty);
        }

        /// <summary>Определяет метод, вызываемый при вызове данной команды.</summary>
        /// <param name="Parameter">
        ///     Данные, используемые данной командой.Если данная команда не требует передачи данных, этот
        ///     объект может быть установлен в null.
        /// </param>
        protected abstract void ExecutedImpl(TParameter Parameter);

        /// <summary>Определяет метод, который определяет, может ли данная команда выполняться в ее текущем состоянии.</summary>
        /// <returns>true, если команда может быть выполнена; в противном случае — false.</returns>
        /// <param name="Parameter">
        ///     Данные, используемые данной командой.Если данная команда не требует передачи данных, этот
        ///     объект может быть установлен в null.
        /// </param>
        protected abstract bool CanExecuteImpl(TParameter Parameter);
    }
}
