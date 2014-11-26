﻿using System.Collections.Generic;
using System.Linq;

namespace FirmwareBurner.Burning
{
    /// <summary>Репозиторий менеджеров прошивок, работающий на переданных ему в конструкторе менеджерах</summary>
    /// <remarks>
    ///     Спроектирован для работы через метод ResolveAll() контейнера, который подаст ему в конструктор экземпляры всех
    ///     зарегистрированных фабрик менеджеров прошивки
    /// </remarks>
    public class BurningReceiptsCatalog : IBurningReceiptsCatalog
    {
        private readonly ILookup<string, IBurningReceiptFactory> _repo;

        public BurningReceiptsCatalog(params IBurningReceiptFactory[] BurningReceiptFactories)
        {
            _repo = BurningReceiptFactories.SelectMany(f => f.TargetDevices.Select(d => new { f, d })).ToLookup(fx => fx.d, fx => fx.f);
        }

        /// <summary>Находит фабрики рецептов, применимые для указанного типа устройства</summary>
        /// <param name="DeviceName">Тип устройства для прошивания</param>
        /// <returns>Фабрики для изготовления нужных <see cref="IBurningReceipt" /></returns>
        public IEnumerable<IBurningReceiptFactory> GetBurningReceiptFactories(string DeviceName)
        {
            return _repo[DeviceName];
        }
    }
}