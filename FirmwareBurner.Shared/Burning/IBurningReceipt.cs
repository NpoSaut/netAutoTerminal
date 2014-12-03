using System;
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
        void Burn(FirmwareProject Project);
    }
}
