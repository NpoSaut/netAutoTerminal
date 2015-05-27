using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using FirmwareBurner.Burning;
using FirmwareBurner.Progress;
using FirmwareBurner.Project;
using FirmwareBurner.Settings;
using FirmwareBurner.ViewModels.Dialogs;
using FirmwareBurner.ViewModels.FirmwareSources;
using FirmwareBurner.ViewModels.Property;
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
            new ProjectViewModel(30, 1, new BlockDetailsViewModel(10007, DateTime.Now), _firmwareSetConstructor, new EventAggregator());

        private static readonly ValidateableTextPropertyViewModel<int> _validateableTextProperty =
            new ValidateableTextPropertyViewModel<int>(new IntTextValueConverter());

        private static readonly FirmwareSelectorDialogViewModel _firmwareSelectorDialog =
            new FirmwareSelectorDialogViewModel(_integratedFirmwareSelector);

        private static readonly IRepository _fakeRepository = new FakeRepository();

        private static readonly ExceptionDialogViewModel _exceptionDialog =
            new ExceptionDialogViewModel("Не удалось отобразить страницу",
                                         "Не удаётся установить соединение с сервером. Возможно, сервер недоступен или требуется проверка настроек сетевого подключения",
                                         // ReSharper disable StringLiteralTypo
                                         "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum");
                                         // ReSharper restore StringLiteralTypo

        private static readonly BurningViewModel _burning =
            new BurningViewModel(new[]
                                 {
                                     new BurningOptionViewModel("Канал 1", 1),
                                     new BurningOptionViewModel("Канал 2", 2)
                                 },
                                 new[]
                                 {
                                     new BurningMethodViewModel("Прошить через AVRDude", new FakeReceipt()),
                                     new BurningMethodViewModel("Сохранить в .hex", new FakeReceipt())
                                 }) { SelectedBurningMethod = new BurningMethodViewModel("Прошить через AVRDude", new FakeReceipt()) };

        private static TargetSelectorViewModel _targetSelector = new TargetSelectorViewModel(new FakeCellsCatalogProvider(), new EventAggregator());

        #region Fake Classes

        private class FakeReceipt : IBurningReceipt
        {
            public string Name
            {
                get { return "Прошить через палец"; }
            }

            public void Burn(FirmwareProject Project, IProgressToken Progress) {  }
        }

        private class FakeCellsCatalogProvider : ICellsCatalogProvider
        {
            public IList<CellKindViewModel> GetCatalog()
            {
                return new[]
                       {
                           new CellKindViewModel(1, "БС-ДПС", new[]
                                                              {
                                                                  new ModificationKindViewModel(1, "128Кб", string.Empty, true),
                                                                  new ModificationKindViewModel(1, "64Кб", string.Empty, false)
                                                              }, new ChannelViewModel[0]),
                           new CellKindViewModel(1, "Монитор-КХ", new[]
                                                                  {
                                                                      new ModificationKindViewModel(1, "Базовая", string.Empty, true)
                                                                  }, new ChannelViewModel[0])
                       };
            }
        }

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

        private class FakeRepository : RepositoryBase
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
            public override ICollection<IRepositoryElement> Packages
            {
                get { return (ICollection<IRepositoryElement>)_packages; }
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

        #endregion

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

        public static ValidateableTextPropertyViewModel<int> ValidateableTextProperty
        {
            get { return _validateableTextProperty; }
        }

        public static IntegratedFirmwareSelectorViewModel IntegratedFirmwareSelector
        {
            get { return _integratedFirmwareSelector; }
        }

        public static ExceptionDialogViewModel ExceptionDialog
        {
            get { return _exceptionDialog; }
        }

        public static TargetSelectorViewModel TargetSelector
        {
            get { return _targetSelector; }
            set { _targetSelector = value; }
        }

        public static BurningViewModel Burning
        {
            get { return _burning; }
        }
    }
}
