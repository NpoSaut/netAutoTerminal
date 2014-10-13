using System.Collections.Generic;

namespace FirmwareBurner.ViewModels.Targeting
{
    public interface ICellsCatalog
    {
        IList<CellKindViewModel> CellKinds { get; }
    }
}