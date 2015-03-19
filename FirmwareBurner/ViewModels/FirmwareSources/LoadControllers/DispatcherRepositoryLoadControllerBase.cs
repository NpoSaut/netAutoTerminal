using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FirmwarePacking;
using FirmwarePacking.Repositories;
using Microsoft.Practices.Prism.Events;

namespace FirmwareBurner.ViewModels.FirmwareSources.LoadControllers
{
    internal abstract class DispatcherRepositoryLoadControllerBase : IRepositoryLoadController
    {
        private readonly CancellationTokenSource _cancellationTokenSource;
        private readonly IDispatcherFacade _dispatcher;
        private readonly IFirmwarePackageViewModelKeyFormatter _keyFormatter;
        private readonly ICollection<FirmwarePackageViewModel> _packagesCollection;
        private readonly INotifyRepository _repository;
        private readonly List<ComponentTarget> _requiredTargets;

        public DispatcherRepositoryLoadControllerBase(INotifyRepository Repository, ICollection<FirmwarePackageViewModel> PackagesCollection,
                                                      List<ComponentTarget> RequiredTargets, IDispatcherFacade Dispatcher,
                                                      IFirmwarePackageViewModelKeyFormatter KeyFormatter, CancellationTokenSource CancellationTokenSource)
        {
            _repository = Repository;
            _packagesCollection = PackagesCollection;
            _requiredTargets = RequiredTargets;
            _dispatcher = Dispatcher;
            _keyFormatter = KeyFormatter;
            _cancellationTokenSource = CancellationTokenSource;
        }

        public void BeginLoad()
        {
            _repository.Updated += RepositoryOnUpdated;
            Task.Factory.StartNew(() => UpdateCollection(_repository.Packages, new IRepositoryElement[0]));
        }

        private void RepositoryOnUpdated(object Sender, RepositoryUpdatedEventArgs e) { UpdateCollection(e.AddedElements, e.RemovedElements); }

        private IEnumerable<IRepositoryElement> FilterPackages(IEnumerable<IRepositoryElement> Packages)
        {
            return Packages.Where(p => _requiredTargets.All(t => p.Targets.Contains(t)));
        }

        private void UpdateCollection(IEnumerable<IRepositoryElement> NewPackages, IEnumerable<IRepositoryElement> RemovedPackages)
        {
            IEnumerable<IRepositoryElement> filteredNewPackages = FilterPackages(NewPackages);
            IEnumerable<IRepositoryElement> filteredRemovedPackages = FilterPackages(RemovedPackages);

            _dispatcher.BeginInvoke((Action<Object>)(x => SynchronizedUpdateCollection(filteredNewPackages, filteredRemovedPackages)), null);
        }

        private void SynchronizedUpdateCollection(IEnumerable<IRepositoryElement> NewElements, IEnumerable<IRepositoryElement> RemovedElements)
        {
            foreach (IRepositoryElement element in NewElements)
            {
                string elementKey = _keyFormatter.GetKey(element);
                FirmwarePackageViewModel existingViewModel = _packagesCollection.SingleOrDefault(p => p.Key == elementKey);
                if (existingViewModel != null)
                    UpdateExistingViewModel(existingViewModel, element);
                else
                    AddNewViewModel(elementKey, element);
            }
            foreach (IRepositoryElement element in RemovedElements)
            {
                string elementKey = _keyFormatter.GetKey(element);
                FirmwarePackageViewModel existingViewModel = _packagesCollection.SingleOrDefault(p => p.Key == elementKey);
                if (existingViewModel != null)
                    RemoveExistingViewModel(existingViewModel);
            }
        }

        private void RemoveExistingViewModel(FirmwarePackageViewModel PackageViewModel) { _packagesCollection.Remove(PackageViewModel); }

        protected abstract void AddNewViewModel(string ElementKey, IRepositoryElement Element);

        protected abstract void UpdateExistingViewModel(FirmwarePackageViewModel ExistingViewModel, IRepositoryElement Element);

        protected void AddModel(FirmwarePackageViewModel PackageViewModel) { _packagesCollection.Add(PackageViewModel); }
    }
}
