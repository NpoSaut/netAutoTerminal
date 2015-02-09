using System;
using System.Collections.Generic;
using System.Linq;
using FirmwareBurner.ViewModels.Dialogs;
using FirmwareBurner.ViewModels.FirmwareSources;
using FirmwareBurner.ViewModels.Targeting;
using FirmwarePacking;
using FirmwarePacking.Repositories;

namespace FirmwareBurner.ViewModels
{
    /// <summary>ViewModel-и для отображения в Design Time</summary>
    public static class SampleData
    {
        private static readonly CompositeFirmwareSelectorViewModel _compositeFirmwareSelector =
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

        private static readonly IntegratedFirmwareSelectorViewModel _integratedFirmwareSelector =
            new IntegratedFirmwareSelectorViewModel(
                Enumerable.Range(0, 6)
                          .Reverse()
                          .Select(i => new FirmwarePackageViewModel(new FirmwareVersionViewModel(String.Format("{0}.{1}", 1, i),
                                                                                                 "LDP",
                                                                                                 DateTime.Today.AddHours(12).AddMonths(-i)),
                                                                    i % 2 == 0))
                          .ToList());

        private static readonly FirmwareSetConstructorViewModel _firmwareSetConstructor =
            new FirmwareSetConstructorViewModel(new[]
                                                {
                                                    new FirmwareSetComponentViewModel(1, "Модуль 1", _compositeFirmwareSelector),
                                                    new FirmwareSetComponentViewModel(1, "Модуль 2", _compositeFirmwareSelector)
                                                });

        private static readonly ProjectViewModel _project =
            new ProjectViewModel(30, 1, new BlockDetailsViewModel { SerialNumber = 10007 }, _firmwareSetConstructor);

        private static readonly FirmwareSelectorDialogViewModel _firmwareSelectorDialog =
            new FirmwareSelectorDialogViewModel(_compositeFirmwareSelector);

        private static readonly IRepository _fakeRepository = new FakeRepository();

        private static readonly RepositoryFirmwareSelectorViewModel _repositoryFirmwareSelector =
            new RepositoryFirmwareSelectorViewModel("Из репозитория", _fakeRepository, new ComponentTarget[0]);

        public static CompositeFirmwareSelectorViewModel CompositeFirmwareSelector
        {
            get { return _compositeFirmwareSelector; }
        }

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

        public static RepositoryFirmwareSelectorViewModel RepositoryFirmwareSelector
        {
            get { return _repositoryFirmwareSelector; }
        }

        public static IntegratedFirmwareSelectorViewModel IntegratedFirmwareSelector
        {
            get { return _integratedFirmwareSelector; }
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
                public FakeRepositoryElement(PackageInformation Information)
                {
                    this.Information = Information;
                    Targets = new[] { new ComponentTarget(1, 1, 1, 1) };
                }

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

        #endregion
    }
}
