namespace FirmwareBurner.Burning
{
    /// <summary>Рецепт прошивки образа</summary>
    /// <remarks>Описывает всё, что касается взаимодействия с утилитой про прошивке для указанного типа образа</remarks>
    /// <typeparam name="TImage">Тип прошиваемого образа</typeparam>
    public interface IBurningReceipt<in TImage>
    {
        /// <summary>Название рецепта прошивки</summary>
        /// <remarks>
        ///     Это название будет отображаться в интерфейсе в виде подписи к команде прошивки. Например "Прошить через
        ///     AvrDude" или "Сохранить в файл"
        /// </remarks>
        string Name { get; }

        /// <summary>Подготавливает инструментарий и прошивает указанный образ</summary>
        /// <param name="Image">Образ для прошивки</param>
        void Burn(TImage Image);
    }
}
