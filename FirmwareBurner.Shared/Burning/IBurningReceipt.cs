namespace FirmwareBurner
{
    /// <summary>Рецепт прошивки образа</summary>
    /// <remarks>Описывает всё, что касается взаимодействия с утилитой про прошивке для указанного типа образа</remarks>
    /// <typeparam name="TImage">Тип прошиваемого образа</typeparam>
    public interface IBurningReceipt<in TImage>
    {
        void Burn(TImage Image);
    }
}
