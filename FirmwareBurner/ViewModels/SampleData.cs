using System;
using FirmwareBurner.ViewModels.FirmwareSources;
using FirmwareBurner.ViewModels.Targeting;
using FirmwarePacking;

namespace FirmwareBurner.ViewModels
{
    /// <summary>ViewModel-и для отображения в Design Time</summary>
    public static class SampleData
    {
        private static readonly CompositeFirmwareSelectorViewModel _firmwareSelector =
            new CompositeFirmwareSelectorViewModel(new[]
                                                   {
                                                       new FakeFirmwareSelectorViewModel("Вручную",
                                                                                         new PackageInformation
                                                                                         {
                                                                                             FirmwareVersion = new Version(1, 3),
                                                                                             FirmwareVersionLabel = "",
                                                                                             ReleaseDate = DateTime.Now
                                                                                         }),
                                                       new FakeFirmwareSelectorViewModel("Из папки",
                                                                                         new PackageInformation
                                                                                         {
                                                                                             FirmwareVersion = new Version(2, 6),
                                                                                             FirmwareVersionLabel = "BPL",
                                                                                             ReleaseDate = DateTime.Now
                                                                                         })
                                                   });

        private static readonly FirmwareSetConstructorViewModel _firmwareSetConstructor =
            new FirmwareSetConstructorViewModel(new[]
                                                {
                                                    new FirmwareSetComponentViewModel(1, "Модуль 1", _firmwareSelector),
                                                    new FirmwareSetComponentViewModel(1, "Модуль 2", _firmwareSelector)
                                                });

        private static readonly ProjectViewModel _project =
            new ProjectViewModel(30, 1, new BlockDetailsViewModel { SerialNumber = 10007 }, _firmwareSetConstructor);

        public static CompositeFirmwareSelectorViewModel FirmwareSelector
        {
            get { return _firmwareSelector; }
        }

        public static FirmwareSetConstructorViewModel FirmwareSetConstructor
        {
            get { return _firmwareSetConstructor; }
        }

        public static ProjectViewModel Project
        {
            get { return _project; }
        }

        #region Fake Classes

        private class FakeFirmwareSelectorViewModel : FirmwareSelectorViewModel
        {
            private readonly PackageInformation _packageInformation;
            public FakeFirmwareSelectorViewModel(string Name, PackageInformation PackageInformation) : base(Name) { _packageInformation = PackageInformation; }

            public override FirmwarePackage SelectedPackage
            {
                get { return new FirmwarePackage { Information = _packageInformation }; }
            }
        }

        #endregion
    }
}
