using System.Collections.Generic;
using System.ComponentModel;

namespace FirmwareBurner.ViewModels.Property
{
    public interface IValidateable : INotifyPropertyChanged
    {
        bool IsInitialized { get; }
        bool IsValid { get; }
        IEnumerable<string> ValidationErrors { get; }
        string ValidationErrorsText { get; }
    }
}
