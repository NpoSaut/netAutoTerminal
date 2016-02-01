using JetBrains.Annotations;

namespace Saut.AutoTerminal.Interfaces
{
    /// <summary>Фабрика терминалов</summary>
    public interface ITerminalFactory
    {
        /// <summary>Открывает терминал</summary>
        [NotNull]
        ITerminal OpenTerminal();
    }
}
