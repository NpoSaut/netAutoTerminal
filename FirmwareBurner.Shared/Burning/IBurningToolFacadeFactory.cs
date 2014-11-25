using System;

namespace FirmwareBurner.Burning
{
    /// <summary>Фабрика фасадов взаимодействия с инструментом по прошивке</summary>
    /// <typeparam name="TImage">Тип прошиваемого образа</typeparam>
    public interface IBurningToolFacadeFactory<in TImage>
    {
        /// <summary>Создаёт фасад взаимодействия с инструментом прошивки для указанного типа устройства</summary>
        /// <param name="DeviceName">Название типа прошиваемого устройства</param>
        IBurningToolFacade<TImage> GetBurningToolFacade(String DeviceName);
    }
}
