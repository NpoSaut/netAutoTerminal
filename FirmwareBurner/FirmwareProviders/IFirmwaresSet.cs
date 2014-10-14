using System.Collections.Generic;
using FirmwarePacking;

namespace FirmwareBurner.FirmwareProviders
{
    /// <summary>Набор прошивок для всех модулей устройства</summary>
    /// <remarks>
    ///     Это тот набор, который будет использоваться для создания образа. В образ входят прошивки для всех программных
    ///     модулей одной аппаратной единицы.
    /// </remarks>
    public interface IFirmwaresSet
    {
        /// <summary>Список идентификаторов модулей в наборе</summary>
        ICollection<int> Modules { get; }

        /// <summary>Получает компонент для указанного модуля из набора</summary>
        /// <param name="ModuleId">Идентификатор модуля</param>
        FirmwareComponent GetComponent(int ModuleId);

        /// <summary>Получает информацию о прошивке для указанного модуля</summary>
        /// <param name="ModuleId">Идентификатор модуля</param>
        PackageInformation GetFirmwareInformation(int ModuleId);
    }
}
