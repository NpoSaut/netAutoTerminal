using System.Collections.Generic;

namespace FirmwareBurner.ViewModels
{
    public interface ICellsCatalog
    {
        IList<CellKindViewModel> CellKinds { get; }
    }
}