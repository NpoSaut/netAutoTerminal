namespace FirmwareBurner.ViewModels
{
    /// <summary>Интерфейс провайдера каталогов ячеек</summary>
    public interface ICellsCatalogProvider
    {
        ICellsCatalog GetCatalog();
    }
}