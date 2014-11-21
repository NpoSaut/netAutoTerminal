﻿using System;
using System.Collections.Generic;

namespace FirmwareBurner.Burning
{
    /// <summary>Репозиторий менеджеров прошивки</summary>
    /// <remarks>Позволяет найти все менеджеры прошивки, подходящий для прошивки того или иного устройства</remarks>
    public interface IBurnersRepository
    {
        /// <summary>Находит всех менеджеров, способных прошить указанный тип устройства</summary>
        /// <param name="DeviceName">Тип устройства для прошивания</param>
        /// <returns>Фабрики для изготовления нужных <see cref="IBurnManager" /></returns>
        IEnumerable<IBurnManagerFactory> GetBurnManagers(String DeviceName);
    }
}
