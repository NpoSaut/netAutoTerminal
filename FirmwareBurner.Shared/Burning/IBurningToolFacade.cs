using FirmwareBurner.Progress;

namespace FirmwareBurner.Burning
{
    /// <summary>Фасад взаимодействия в инструментом прошивки</summary>
    /// <remarks>Описывает всё, что касается взаимодействия с утилитой про прошивке для указанного типа образа</remarks>
    /// <typeparam name="TImage">Тип прошиваемого образа</typeparam>
    public interface IBurningToolFacade<in TImage>
    {
        /// <summary>Подготавливает инструментарий и прошивает указанный образ</summary>
        /// <param name="Image">Образ для прошивки</param>
        /// <param name="ProgressToken">Токен прогресса выполнения операции</param>
        void Burn(TImage Image, IProgressToken ProgressToken);
    }
}
