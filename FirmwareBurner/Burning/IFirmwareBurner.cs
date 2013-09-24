using System;
using FirmwareBurner.IntelHex;
namespace FirmwareBurner.Burning
{
    /// <summary>
    /// Интерфейс члена, способного прошить прошивку в AVR-устройство. Реализует взаимодействие с программатором, либо чем-то вроде того.
    /// </summary>
    public interface IFirmwareBurner
    {
        void Burn(Pie pie);
    }
}
