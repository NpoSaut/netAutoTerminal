using System.Collections.Generic;
using FirmwareBurner.ViewModels.Targeting;

namespace FirmwareBurner.ViewModels.Tools
{
    /// <summary>Интерфейс провайдера каталогов ячеек</summary>
    public interface ICellsCatalogProvider
    {
        IList<CellKindViewModel> GetCatalog();
    }
}
