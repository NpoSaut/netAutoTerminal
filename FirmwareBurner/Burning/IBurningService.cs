using System.Collections.Generic;
using FirmwareBurner.Progress;
using FirmwareBurner.Project;

namespace FirmwareBurner.Burning
{
    public interface IBurningService
    {
        /// <summary>Находит все варианты прошивки для указанного устройства</summary>
        /// <param name="DeviceName">Название устройства</param>
        ICollection<IBurningMethod> GetBurningMethods(string DeviceName);

        void BeginBurn(IBurningReceipt Receipt, FirmwareProject Project, IProgressToken ProgressToken);
    }
}
