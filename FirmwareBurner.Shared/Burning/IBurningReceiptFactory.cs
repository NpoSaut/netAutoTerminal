using System.Collections.Generic;

namespace FirmwareBurner.Burning
{
    /// <summary>Фабрика для изготовления <see cref="IBurningReceipt" />
    /// </summary>
    public interface IBurningReceiptFactory
    {
        /// <summary>Имя изготавливаемого рецепта</summary>
        string ReceiptName { get; }

        /// <summary>Типы устройств, для которых может использоваться этот рецепт</summary>
        IEnumerable<string> TargetDevices { get; }

        /// <summary>Создаёт экземпляр <see cref="IBurningReceipt" />, пригодный для прошивания указанного типа устройства</summary>
        /// <param name="DeviceName">Название типа прошиваемого устройства</param>
        IBurningReceipt GetReceipt(string DeviceName);
    }
}
