using System;
using System.Collections.Generic;
using System.Linq;

namespace FirmwareBurner
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

    /// <summary>Репозиторий менеджеров прошивок, работающий на переданных ему в конструкторе менеджерах</summary>
    /// <remarks>
    ///     Спроектирован для работы через метод ResolveAll() контейнера, который подаст ему в конструктор экземпляры всех
    ///     зарегистрированных фабрик менеджеров прошивки
    /// </remarks>
    public class BurnersRepository : IBurnersRepository
    {
        private readonly ILookup<string, IBurnManagerFactory> _repo;
        public BurnersRepository(params IBurnManagerFactory[] BurnManagerFactories) { _repo = BurnManagerFactories.ToLookup(bmf => bmf.BurningDeviceName); }

        /// <summary>Находит всех менеджеров, способных прошить указанный тип устройства</summary>
        /// <param name="DeviceName">Тип устройства для прошивания</param>
        /// <returns>Фабрики для изготовления нужных <see cref="IBurnManager" /></returns>
        public IEnumerable<IBurnManagerFactory> GetBurnManagers(string DeviceName) { return _repo[DeviceName]; }
    }
}
