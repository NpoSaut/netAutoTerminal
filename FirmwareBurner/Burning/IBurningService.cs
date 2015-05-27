using System.Collections.Generic;
using FirmwareBurner.AsyncOperations;
using FirmwareBurner.Project;

namespace FirmwareBurner.Burning
{
    public interface IBurningService
    {
        /// <summary>Находит все варианты прошивки для указанного устройства</summary>
        /// <param name="DeviceName">Название устройства</param>
        ICollection<IBurningMethod> GetBurningMethods(string DeviceName);

        IAsyncOperationToken BeginBurn(IBurningReceipt Receipt, FirmwareProject Project);
    }
}
