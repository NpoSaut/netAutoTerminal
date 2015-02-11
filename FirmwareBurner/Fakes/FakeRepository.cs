using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using FirmwarePacking;
using FirmwarePacking.Repositories;

namespace FirmwareBurner.Fakes
{
    public class FakeRepository : IRepository
    {
        private readonly int _loadingDelayMs;
        public FakeRepository(int LoadingDelayMs = 0) { _loadingDelayMs = LoadingDelayMs; }

        /// <summary>Список всех пакетов в репозитории</summary>
        public IEnumerable<IRepositoryElement> Packages
        {
            get { throw new NotImplementedException(); }
        }

        /// <summary>Находит пакет прошивки, содержащий компоненты для всех указанных целей</summary>
        /// <param name="Targets">Цели прошивки</param>
        public IEnumerable<IRepositoryElement> GetPackagesForTargets(ICollection<ComponentTarget> Targets) { return GetPackages(Targets); }

        private IEnumerable<IRepositoryElement> GetPackages(ICollection<ComponentTarget> Targets)
        {
            for (int i = 0; i < 8; i++)
            {
                foreach (string label in new[] { "P", "DZR" })
                {
                    Thread.Sleep(_loadingDelayMs);
                    yield return new FakeRepositoryElement(
                        new PackageInformation
                        {
                            FirmwareVersion = new Version(1, i),
                            FirmwareVersionLabel = label,
                            ReleaseDate = DateTime.Now.AddMonths(-1).AddDays(i)
                        },
                        Targets.ToList());
                }
            }
        }

        private class FakeRepositoryElement : IRepositoryElement
        {
            /// <summary>Инициализирует новый экземпляр класса <see cref="T:System.Object" />.</summary>
            public FakeRepositoryElement(PackageInformation Information, ICollection<ComponentTarget> Targets)
            {
                this.Information = Information;
                this.Targets = Targets;
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
}
