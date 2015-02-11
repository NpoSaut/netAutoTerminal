using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using FirmwarePacking.Repositories;
using Microsoft.Practices.Prism.Events;

namespace FirmwareBurner.ViewModels.FirmwareSources.LoadControllers
{
    internal abstract class DispatcherRepositoryLoadControllerBase : IRepositoryLoadController
    {
        private readonly CancellationTokenSource _cancellationTokenSource;
        private readonly IDispatcherFacade _dispatcher;
        private readonly IFirmwarePackageViewModelKeyFormatter _keyFormatter;
        private readonly IRepositoryLoader _loader;
        private readonly ICollection<FirmwarePackageViewModel> _packagesCollection;

        public DispatcherRepositoryLoadControllerBase(IRepositoryLoader Loader, ICollection<FirmwarePackageViewModel> PackagesCollection,
                                                      IDispatcherFacade Dispatcher, IFirmwarePackageViewModelKeyFormatter KeyFormatter,
                                                      CancellationTokenSource CancellationTokenSource)
        {
            _loader = Loader;
            _packagesCollection = PackagesCollection;
            _dispatcher = Dispatcher;
            _keyFormatter = KeyFormatter;
            _cancellationTokenSource = CancellationTokenSource;
        }

        public void BeginLoad()
        {
            _loader.ElementsLoaded += LoaderOnElementsLoaded;
            _loader.StartLoading(_cancellationTokenSource.Token);
        }

        private void LoaderOnElementsLoaded(object Sender, RepositoryElementsLoadedEventArgs e)
        {
            _dispatcher.BeginInvoke((Action<ICollection<IRepositoryElement>>)UpdateElements, e.Elements);
        }

        private void UpdateElements(ICollection<IRepositoryElement> Elements)
        {
            foreach (IRepositoryElement element in Elements)
            {
                string elementKey = _keyFormatter.GetKey(element);
                FirmwarePackageViewModel existingViewModel = _packagesCollection.SingleOrDefault(p => p.Key == elementKey);
                if (existingViewModel != null)
                    UpdateExistingViewModel(existingViewModel, element);
                else
                    AddNewViewModel(elementKey, element);
            }
        }

        protected abstract void AddNewViewModel(string ElementKey, IRepositoryElement Element);

        protected abstract void UpdateExistingViewModel(FirmwarePackageViewModel ExistingViewModel, IRepositoryElement Element);
        protected void AddModel(FirmwarePackageViewModel PackageViewModel) { _packagesCollection.Add(PackageViewModel); }
    }
}
