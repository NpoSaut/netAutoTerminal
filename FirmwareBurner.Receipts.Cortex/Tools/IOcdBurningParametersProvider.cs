using FirmwareBurner.Annotations;

namespace FirmwareBurner.Receipts.Cortex.Tools
{
    /// <summary>Провайдер информации параметрах прошивки ячейки через утилиту OpenOCD</summary>
    public interface IOcdBurningParametersProvider
    {
        /// <summary>Находит BoardName для ячейки</summary>
        /// <param name="Target">Цель прошивки</param>
        string GetBoardName(TargetInformation Target);

        /// <summary>Находит TargetName для ячейки</summary>
        /// <param name="Target">Цель прошивки</param>
        /// <returns>Название цели прошивки или null, если оно не требуется</returns>
        [CanBeNull]
        string GetTargetName(TargetInformation Target);
    }
}
