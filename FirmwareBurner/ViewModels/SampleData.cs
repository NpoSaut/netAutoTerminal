using System;
using System.Collections.Generic;
using System.Linq;
using FirmwareBurner.ViewModels.Dialogs;
using FirmwareBurner.ViewModels.FirmwareSources;
using FirmwareBurner.ViewModels.Targeting;
using FirmwareBurner.ViewModels.Tools;
using FirmwarePacking;
using FirmwarePacking.Repositories;
using Microsoft.Practices.Prism.Events;

namespace FirmwareBurner.ViewModels
{
    /// <summary>ViewModel-и для отображения в Design Time</summary>
    public static class SampleData
    {
        private static readonly IntegratedFirmwareSelectorViewModel _integratedFirmwareSelector =
            new IntegratedFirmwareSelectorViewModel(
                new[]
                {
                    new FirmwarePackageViewModel("asdasd", new FirmwareVersionViewModel("1.36", "ABC", DateTime.Today),
                                                 new FirmwarePackageAvailabilityViewModel(false), ReleaseStatus.Actual, null),
                    new FirmwarePackageViewModel("asdasd", new FirmwareVersionViewModel("1.36", "XYZ", DateTime.Today),
                                                 new FirmwarePackageAvailabilityViewModel(false), ReleaseStatus.Actual, null),
                    new FirmwarePackageViewModel("asdasd", new FirmwareVersionViewModel("1.32", "ABC", DateTime.Today),
                                                 new FirmwarePackageAvailabilityViewModel(false, true, 0.37), ReleaseStatus.Actual, null),
                    new FirmwarePackageViewModel("asdasd", new FirmwareVersionViewModel("1.32", "XYZ", DateTime.Today),
                                                 new FirmwarePackageAvailabilityViewModel(true), ReleaseStatus.Actual, null),
                    new FirmwarePackageViewModel("asdasd", new FirmwareVersionViewModel("1.30", null, DateTime.Today),
                                                 new FirmwarePackageAvailabilityViewModel(true), ReleaseStatus.Actual, null),
                    new FirmwarePackageViewModel("asdasd", new FirmwareVersionViewModel("1.29", null, DateTime.Today),
                                                 new FirmwarePackageAvailabilityViewModel(true), ReleaseStatus.Actual, null)
                });

        private static readonly FirmwareSetConstructorViewModel _firmwareSetConstructor =
            new FirmwareSetConstructorViewModel(new[]
                                                {
                                                    new FirmwareSetComponentViewModel(1, "Модуль 1", _integratedFirmwareSelector),
                                                    new FirmwareSetComponentViewModel(1, "Модуль 2", _integratedFirmwareSelector)
                                                });

        private static readonly ProjectViewModel _project =
            new ProjectViewModel(30, 1, new BlockDetailsViewModel { SerialNumber = 10007 }, _firmwareSetConstructor);

        private static readonly FirmwareSelectorDialogViewModel _firmwareSelectorDialog =
            new FirmwareSelectorDialogViewModel(_integratedFirmwareSelector);

        private static readonly IRepository _fakeRepository = new FakeRepository();

        public static FirmwareSetConstructorViewModel FirmwareSetConstructor
        {
            get { return _firmwareSetConstructor; }
        }

        public static ProjectViewModel Project
        {
            get { return _project; }
        }

        public static FirmwareSelectorDialogViewModel FirmwareSelectorDialog
        {
            get { return _firmwareSelectorDialog; }
        }

        public static IntegratedFirmwareSelectorViewModel IntegratedFirmwareSelector
        {
            get { return _integratedFirmwareSelector; }
        }

        public static TargetSelectorViewModel TargetSelector
        {
            get { return _targetSelector; }
            set { _targetSelector = value; }
        }

        private static TargetSelectorViewModel _targetSelector = new TargetSelectorViewModel(new FakeCellsCatalogProvider(), null);

        #region Fake Classes

        private class FakeFirmwareSelectorViewModel : FirmwareSelectorViewModel
        {
            private readonly PackageInformation _packageInformation;

            public FakeFirmwareSelectorViewModel(string Name, PackageInformation PackageInformation) : base(Name)
            {
                _packageInformation = PackageInformation;
                SelectedPackage = new FirmwarePackageViewModel("sdfsdf", new FirmwareVersionViewModel("3.2", "LDS", DateTime.Now),
                                                               new FirmwarePackageAvailabilityViewModel(true), ReleaseStatus.Unknown, null);
            }

            public override FirmwarePackageViewModel SelectedPackage { get; set; }
        }

        private class FakeRepository : Repository
        {
            private readonly IEnumerable<IRepositoryElement> _packages =
                Enumerable.Range(0, 6)
                          .Reverse()
                          .Select(i => new FakeRepositoryElement(new PackageInformation
                                                                 {
                                                                     FirmwareVersion = new Version(1, i),
                                                                     FirmwareVersionLabel = "LDP",
                                                                     ReleaseDate = DateTime.Today.AddHours(12).AddMonths(-i)
                                                                 }))
                          .ToList();

            /// <summary>Список всех пакетов в репозитории</summary>
            public override IEnumerable<IRepositoryElement> Packages
            {
                get { return _packages; }
            }

            private class FakeRepositoryElement : IRepositoryElement
            {
                public FakeRepositoryElement(PackageInformation Information, ReleaseStatus Status = ReleaseStatus.Unknown)
                {
                    this.Status = Status;
                    this.Information = Information;
                    Targets = new[] { new ComponentTarget(1, 1, 1, 1) };
                }

                /// <summary>Статус релиза пакета</summary>
                public ReleaseStatus Status { get; private set; }

                /// <summary>Информация о пакете</summary>
                public PackageInformation Information { get; private set; }

                /// <summary>Список целей, для которых имеются компоненты в данном пакете</summary>
                public ICollection<ComponentTarget> Targets { get; private set; }

                /// <summary>Загружает всё тело пакета</summary>
                public FirmwarePackage GetPackage() { throw new NotImplementedException(); }

                /// <summary>Загружает необходимый компонент из тела пакета</summary>
                /// <param name="Target">Цель, компонент для которой требуется</param>
                public FirmwareComponent GetComponent(ComponentTarget Target) { throw new NotImplementedException(); }
            }
        }

        class FakeCellsCatalogProvider : ICellsCatalogProvider
        {
            public IList<CellKindViewModel> GetCatalog()
            {
                return new[]
                       {
                           new CellKindViewModel(1, "БС-ДПС", new []
                                                              {
                                                                  new ModificationKindViewModel(1, "128Кб", string.Empty), 
                                                                  new ModificationKindViewModel(1, "64Кб", string.Empty), 
                                                              }, new ChannelViewModel[0]), 
                           new CellKindViewModel(1, "Монитор-КХ", new []
                                                              {
                                                                  new ModificationKindViewModel(1, "Базовая", string.Empty),
                                                              }, new ChannelViewModel[0]), 
                       };
            }
        }

        #endregion
    }
}
