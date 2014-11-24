using FirmwareBurner.Project;

namespace FirmwareBurner.Burning
{
    /// <summary>Менеджер прошивки</summary>
    /// <remarks>Занимается приготовлением образа прошивки и отправкой его получателю</remarks>
    public interface IBurningReceipt
    {
        /// <summary>Прошивает указанный проект</summary>
        /// <param name="Project">Проект для прожигания</param>
        void Burn(FirmwareProject Project);
    }
}
