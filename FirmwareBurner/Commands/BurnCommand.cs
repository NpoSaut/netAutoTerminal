using FirmwareBurner.Model.Project;

namespace FirmwareBurner.Commands
{
    /// <summary>Команда на зашивку устройства</summary>
    public class BurnCommand : ParameterCommandBase<FirmwareProject>
    {
        private readonly IBurnManager _manager;

        /// <summary>Создаёт экземпляр команды на зашивку устройства</summary>
        /// <param name="Manager">Менеджер, используемый для зашивки устройства</param>
        public BurnCommand(IBurnManager Manager) { _manager = Manager; }

        /// <summary>Определяет метод, вызываемый при вызове данной команды.</summary>
        /// <param name="Parameter">
        ///     Данные, используемые данной командой.Если данная команда не требует передачи данных, этот
        ///     объект может быть установлен в null.
        /// </param>
        protected override void ExecutedImpl(FirmwareProject Parameter) { _manager.Burn(Parameter); }

        /// <summary>Определяет метод, который определяет, может ли данная команда выполняться в ее текущем состоянии.</summary>
        /// <returns>true, если команда может быть выполнена; в противном случае — false.</returns>
        /// <param name="Parameter">
        ///     Данные, используемые данной командой.Если данная команда не требует передачи данных, этот
        ///     объект может быть установлен в null.
        /// </param>
        protected override bool CanExecuteImpl(FirmwareProject Parameter) { return true; }
    }
}
