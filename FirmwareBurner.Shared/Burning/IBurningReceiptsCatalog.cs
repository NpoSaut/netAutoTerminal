using System;
using System.Collections.Generic;

namespace FirmwareBurner.Burning
{
    /// <summary>Каталог рецептов прошивки</summary>
    /// <remarks>Позволяет найти все рецепты прошивки, подходящий для прошивки того или иного устройства</remarks>
    public interface IBurningReceiptsCatalog
    {
        /// <summary>Находит рецепты, применимые для указанного типа устройства</summary>
        /// <param name="DeviceName">Тип устройства для прошивания</param>
        /// <returns>Фабрики для изготовления нужных <see cref="IBurningReceipt" /></returns>
        IEnumerable<IBurningReceiptFactory> GetBurningReceiptFactories(string DeviceName);
    }
}
