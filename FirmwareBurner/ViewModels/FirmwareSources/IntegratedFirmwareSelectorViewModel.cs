using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using FirmwareBurner.Annotations;
using FirmwarePacking;
using FirmwarePacking.Repositories;
using Microsoft.Practices.Prism.Events;

namespace FirmwareBurner.ViewModels.FirmwareSources
{
    public class IntegratedFirmwareSelectorViewModel : FirmwareSelectorViewModel
    {
        private readonly CancellationTokenSource _cancellationTokenSource;
        private readonly IDispatcherFacade _dispatcher;
        private readonly ObservableCollection<FirmwarePackageViewModel> _packages;

        public IntegratedFirmwareSelectorViewModel([NotNull] IList<FirmwarePackageViewModel> Packages, IDispatcherFacade Dispatcher) : base("Интегрированный")
        {
            _dispatcher = Dispatcher;
            _packages = new ObservableCollection<FirmwarePackageViewModel>(Packages);
            this.Packages = new ReadOnlyObservableCollection<FirmwarePackageViewModel>(_packages);
        }

        public IntegratedFirmwareSelectorViewModel(ICollection<IRepositoryLoader> LocalLoaders, ICollection<IRepositoryLoader> RemoteLoaders,
                                                   IDispatcherFacade Dispatcher) : base("Интегрированный")
        {
            _dispatcher = Dispatcher;
            _packages = new ObservableCollection<FirmwarePackageViewModel>();
            Packages = new ReadOnlyObservableCollection<FirmwarePackageViewModel>(_packages);

            _cancellationTokenSource = new CancellationTokenSource();

            foreach (IRepositoryLoader loader in LocalLoaders)
                InitializeLocalLoader(loader);

            foreach (IRepositoryLoader loader in RemoteLoaders)
                InitializeRemoteLoader(loader);
        }

        public ReadOnlyObservableCollection<FirmwarePackageViewModel> Packages { get; private set; }

        public override FirmwarePackage SelectedPackage
        {
            get { return null; }
        }

        #region Local

        private void LocalLoaderOnElementsLoaded(object Sender, RepositoryElementsLoadedEventArgs e)
        {
            _dispatcher.BeginInvoke((Action<ICollection<IRepositoryElement>>)UpdateLocalElements, e.Elements);
        }

        private void UpdateLocalElements(ICollection<IRepositoryElement> RepositoryElements)
        {
            foreach (IRepositoryElement element in RepositoryElements)
                OnLocalElementLoad(element);
        }

        private void OnLocalElementLoad(IRepositoryElement element)
        {
            string elementKey = GetElementKey(element);
            FirmwarePackageViewModel existingViewModel = _packages.SingleOrDefault(p => p.Key == elementKey);
            if (existingViewModel != null)
                UpdateLocalExistingViewModel(existingViewModel, element);
            else
                AddNewLocalViewModel(elementKey, element);
        }

        private void AddNewLocalViewModel(string ElementKey, IRepositoryElement Element)
        {
            var viewModel =
                new FirmwarePackageViewModel(ElementKey,
                                             new FirmwareVersionViewModel(Element.Information.FirmwareVersion.ToString(2),
                                                                          Element.Information.FirmwareVersionLabel,
                                                                          Element.Information.ReleaseDate),
                                             new FirmwarePackageAvailabilityViewModel(true),
                                             ReleaseStatus.Unknown);
            _packages.Add(viewModel);
        }

        private void UpdateLocalExistingViewModel(FirmwarePackageViewModel ExistingViewModel, IRepositoryElement Element)
        {
            ExistingViewModel.Availability = new FirmwarePackageAvailabilityViewModel(true);
        }

        #endregion

        #region Remote

        private void RemoteLoaderOnElementsLoaded(object Sender, RepositoryElementsLoadedEventArgs e)
        {
            _dispatcher.BeginInvoke((Action<ICollection<IRepositoryElement>>)UpdateRemoteElements, e.Elements);
        }

        private void UpdateRemoteElements(ICollection<IRepositoryElement> RepositoryElements)
        {
            foreach (IRepositoryElement element in RepositoryElements)
                OnRemoteElementLoad(element);
        }

        private void OnRemoteElementLoad(IRepositoryElement element)
        {
            string elementKey = GetElementKey(element);
            FirmwarePackageViewModel existingViewModel = _packages.SingleOrDefault(p => p.Key == elementKey);
            if (existingViewModel != null)
                UpdateRemoteExistingViewModel(existingViewModel, element);
            else
                AddNewRemoteViewModel(elementKey, element);
        }

        private void AddNewRemoteViewModel(string ElementKey, IRepositoryElement Element)
        {
            var viewModel =
                new FirmwarePackageViewModel(ElementKey,
                                             new FirmwareVersionViewModel(Element.Information.FirmwareVersion.ToString(2),
                                                                          Element.Information.FirmwareVersionLabel,
                                                                          Element.Information.ReleaseDate),
                                             new FirmwarePackageAvailabilityViewModel(false),
                                             Element.Status);
            _packages.Add(viewModel);
        }

        private void UpdateRemoteExistingViewModel(FirmwarePackageViewModel ExistingViewModel, IRepositoryElement Element)
        {
            ExistingViewModel.Status = Element.Status;
        }

        #endregion

        private void InitializeLocalLoader(IRepositoryLoader Loader)
        {
            Loader.ElementsLoaded += LocalLoaderOnElementsLoaded;
            Loader.StartLoading(_cancellationTokenSource.Token);
        }

        private void InitializeRemoteLoader(IRepositoryLoader Loader)
        {
            Loader.ElementsLoaded += RemoteLoaderOnElementsLoaded;
            Loader.StartLoading(_cancellationTokenSource.Token);
        }

        private string GetElementKey(IRepositoryElement element)
        {
            return String.Format("{0}.{1} {2}", element.Information.FirmwareVersion.Major, element.Information.FirmwareVersion.Minor,
                                 element.Information.FirmwareVersionLabel);
        }
    }
}
