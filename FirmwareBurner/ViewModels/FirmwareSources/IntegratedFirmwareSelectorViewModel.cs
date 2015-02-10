using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Windows.Threading;
using FirmwareBurner.Annotations;
using FirmwarePacking;
using Microsoft.Practices.Prism;
using Microsoft.Practices.Prism.Events;

namespace FirmwareBurner.ViewModels.FirmwareSources
{
    public class IntegratedFirmwareSelectorViewModel : FirmwareSelectorViewModel
    {
        private readonly IRepositoryLoader _loader;
        private IDispatcherFacade _dispatcher;
        private readonly ObservableCollection<FirmwarePackageViewModel> _packages;

        public IntegratedFirmwareSelectorViewModel([NotNull] IList<FirmwarePackageViewModel> Packages, IDispatcherFacade Dispatcher) : base("Интегрированный")
        {
            _dispatcher = Dispatcher;
            _packages = new ObservableCollection<FirmwarePackageViewModel>(Packages);
            this.Packages = new ReadOnlyObservableCollection<FirmwarePackageViewModel>(_packages);
        }

        public IntegratedFirmwareSelectorViewModel(IRepositoryLoader Loader, IDispatcherFacade Dispatcher) : base("Интегрированный")
        {
            _loader = Loader;
            _dispatcher = Dispatcher;
            _packages = new ObservableCollection<FirmwarePackageViewModel>();
            Packages = new ReadOnlyObservableCollection<FirmwarePackageViewModel>(_packages);

            _loader.ElementsLoaded += LoaderOnElementsLoaded;
            var cts = new CancellationTokenSource();
            _loader.StartLoading(cts.Token);
        }

        public ReadOnlyObservableCollection<FirmwarePackageViewModel> Packages { get; private set; }

        public override FirmwarePackage SelectedPackage
        {
            get { return null; }
        }

        private void LoaderOnElementsLoaded(object Sender, RepositoryElementsLoadedEventArgs e)
        {
            ICollection<FirmwarePackageViewModel> viewModels = e.Elements.Select(p =>
                                                                                 new FirmwarePackageViewModel(
                                                                                     new FirmwareVersionViewModel(p.Information.FirmwareVersion.ToString(2),
                                                                                                                  p.Information.FirmwareVersionLabel,
                                                                                                                  p.Information.ReleaseDate),
                                                                                     new FirmwarePackageAvailabilityViewModel(true)))
                                                                .ToList();
            _dispatcher.BeginInvoke((Action<ICollection<FirmwarePackageViewModel>>)UpdateElements, viewModels);
        }

        private void UpdateElements(ICollection<FirmwarePackageViewModel> ViewModels) { _packages.AddRange(ViewModels); }
    }
}
