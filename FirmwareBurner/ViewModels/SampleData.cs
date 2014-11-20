using FirmwareBurner.ViewModels.FirmwareSources;

namespace FirmwareBurner.ViewModels
{
    /// <summary>ViewModel-и для отображения в Design Time</summary>
    public static class SampleData
    {
        private static readonly CompositeFirmwareSelectorViewModel _firmwareSelector =
            new CompositeFirmwareSelectorViewModel(new[] { new ManualFirmwareSelectorViewModel("Вручную") });

        public static CompositeFirmwareSelectorViewModel FirmwareSelector
        {
            get { return _firmwareSelector; }
        }
    }
}
