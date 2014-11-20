using System.Collections.Generic;
using FirmwareBurner.ViewModels.Bases;

namespace FirmwareBurner.ViewModels
{
    public class FirmwareSetConstructorViewModel : ViewModelBase
    {
        public FirmwareSetConstructorViewModel(ICollection<FirmwareSetComponentViewModel> Components) { this.Components = Components; }
        public ICollection<FirmwareSetComponentViewModel> Components { get; private set; }
    }
}
