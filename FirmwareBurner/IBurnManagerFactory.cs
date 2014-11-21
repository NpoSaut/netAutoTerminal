namespace FirmwareBurner
{
    /// <summary>Фабрика для изготовления <see cref="IBurnManager" />
    /// </summary>
    public interface IBurnManagerFactory
    {
        /// <summary>Имя изготавливаемого менеджера прошивок</summary>
        string BurnerName { get; }

        /// <summary>Тип устройства, которое может прошить изготавливаемый менеджер</summary>
        string BurningDeviceName { get; }

        /// <summary>Создаёт экземпляр <see cref="IBurnManager" />
        /// </summary>
        IBurnManager GetBurnManager();
    }
}
