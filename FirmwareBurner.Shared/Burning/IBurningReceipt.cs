using System;
using AsyncOperations.Progress;
using FirmwareBurner.Burning.Exceptions;
using FirmwareBurner.Project;

namespace FirmwareBurner.Burning
{
    /// <summary>Рецепт прошивания устройства</summary>
    /// <remarks>Содержит в себе весь процесс приготовления образа и прошивки устройства по указанному проекту</remarks>
    public interface IBurningReceipt
    {
        /// <summary>Название рецепта</summary>
        String Name { get; }

        /// <summary>Прошивает указанный проект</summary>
        /// <param name="Project">Проект для прожигания</param>
        /// <param name="Progress">Токен для отчёта о процессе прошивки</param>
        /// <exception cref="BurningException">
        ///     В это исключение будет обёрнуто любое исключение, возникшее в процессе прожигания
        ///     проекта
        /// </exception>
        void Burn(FirmwareProject Project, IProgressToken Progress);
    }
}
