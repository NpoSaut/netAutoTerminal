using System;
using System.Collections.Generic;
using System.Linq;
using FirmwarePacking;
using FirmwarePacking.Repositories;

namespace FirmwareBurner.Fakes
{
    public class FakeRepository : INotifyRepository
    {
        private readonly int _endVersionIndex;
        private readonly bool _setReleaseStatus;
        private readonly int _startVersionIndex;

        public FakeRepository(int StartVersionIndex, int EndVersionIndex, bool SetReleaseStatus = false)
        {
            _setReleaseStatus = SetReleaseStatus;
            _startVersionIndex = StartVersionIndex;
            _endVersionIndex = EndVersionIndex;
        }

        /// <summary>Список всех пакетов в репозитории</summary>
        public ICollection<IRepositoryElement> Packages
        {
            get { throw new NotImplementedException(); }
        }

        /// <summary>Находит пакет прошивки, содержащий компоненты для всех указанных целей</summary>
        /// <param name="Targets">Цели прошивки</param>
        public ICollection<IRepositoryElement> GetPackagesForTargets(ICollection<ComponentTarget> Targets) { return GetPackages(Targets); }

        /// <summary>
        ///     Выполняет определяемые приложением задачи, связанные с удалением, высвобождением или сбросом неуправляемых
        ///     ресурсов.
        /// </summary>
        public void Dispose() { throw new NotImplementedException(); }

        public event EventHandler<RepositoryUpdatedEventArgs> Updated;

        private ICollection<IRepositoryElement> GetPackages(ICollection<ComponentTarget> Targets)
        {
            return
                Enumerable.Range(_startVersionIndex, _endVersionIndex)
                          .SelectMany(i =>
                                      new[] { "P", "DZR" }.Select(label =>
                                                                  new FakeRepositoryElement(
                                                                      new PackageInformation
                                                                      {
                                                                          FirmwareVersion = new Version(1, i),
                                                                          FirmwareVersionLabel = label,
                                                                          ReleaseDate = DateTime.Now.AddMonths(-1).AddDays(i),
                                                                      },
                                                                      Targets.ToList(),
                                                                      _setReleaseStatus
                                                                          ? (i == 5 ? ReleaseStatus.Actual : ReleaseStatus.Archive)
                                                                          : ReleaseStatus.Unknown)))
                          .OfType<IRepositoryElement>()
                          .ToList();
        }

        private class FakeRepositoryElement : IRepositoryElement
        {
            /// <summary>Инициализирует новый экземпляр класса <see cref="T:System.Object" />.</summary>
            public FakeRepositoryElement(PackageInformation Information, ICollection<ComponentTarget> Targets, ReleaseStatus Status)
            {
                this.Status = Status;
                this.Information = Information;
                this.Targets = Targets;
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
}
