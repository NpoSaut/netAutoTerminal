namespace ExternalTools.Interfaces
{
    /// <summary>Параметр запуска внешней утилиты</summary>
    public interface ILaunchParameter
    {
        /// <summary>Получает строковое представление параметра, которое можно подставить в строку запуска утилиты</summary>
        string GetStringPresentation();
    }
}
