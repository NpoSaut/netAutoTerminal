using FirmwareBurner.ViewModels.FirmwareSources;

namespace FirmwareBurner.ViewModels
{
    /// <summary>ViewModel-и для отображения в Design Time</summary>
    public static class SampleData
    {
        private static readonly CompositeFirmwareSelectorViewModel _firmwareSelector =
            new CompositeFirmwareSelectorViewModel(new[]
                                                   {
                                                       new ManualFirmwareSelectorViewModel("Вручную"),
                                                       new ManualFirmwareSelectorViewModel("Вручную")
                                                   });

        private static readonly FirmwareSetConstructorViewModel _firmwareSetConstructor =
            new FirmwareSetConstructorViewModel(new[]
                                                {
                                                    new FirmwareSetComponentViewModel(1, "Модуль 1", _firmwareSelector),
                                                    new FirmwareSetComponentViewModel(1, "Модуль 2", _firmwareSelector)
                                                });

        public static CompositeFirmwareSelectorViewModel FirmwareSelector
        {
            get { return _firmwareSelector; }
        }

        public static FirmwareSetConstructorViewModel FirmwareSetConstructor
        {
            get { return _firmwareSetConstructor; }
        }
    }
}
