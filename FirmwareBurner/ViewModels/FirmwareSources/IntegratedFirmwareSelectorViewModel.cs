using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using FirmwareBurner.Annotations;
using FirmwareBurner.ViewModels.FirmwareSources.LoadControllers;
using FirmwarePacking.Repositories;

namespace FirmwareBurner.ViewModels.FirmwareSources
{
    public class IntegratedFirmwareSelectorViewModel : FirmwareSelectorViewModel
    {
        private readonly CancellationTokenSource _cancellationTokenSource;
        private readonly ObservableCollection<FirmwarePackageViewModel> _packages;
        private FirmwarePackageViewModel _selectedPackage;

        public IntegratedFirmwareSelectorViewModel([NotNull] IList<FirmwarePackageViewModel> Packages) : base("Интегрированный")
        {
            _packages = new ObservableCollection<FirmwarePackageViewModel>(Packages);
            this.Packages = new ReadOnlyObservableCollection<FirmwarePackageViewModel>(_packages);
        }

        public IntegratedFirmwareSelectorViewModel(ICollection<IRepositoryLoader> LocalLoaders, ICollection<IRepositoryLoader> RemoteLoaders,
                                                   ILoadControllerFactory LoadControllerFactory) : base("Интегрированный")
        {
            ILoadControllerFactory loadControllerFactory = LoadControllerFactory;
            _packages = new ObservableCollection<FirmwarePackageViewModel>();
            Packages = new ReadOnlyObservableCollection<FirmwarePackageViewModel>(_packages);

            _cancellationTokenSource = new CancellationTokenSource();

            foreach (IRepositoryLoader loader in LocalLoaders)
                loadControllerFactory.GetLocalLoadController(loader, _packages, _cancellationTokenSource).BeginLoad();

            foreach (IRepositoryLoader loader in RemoteLoaders)
                loadControllerFactory.GetRemoteLoadController(loader, _packages, _cancellationTokenSource).BeginLoad();
        }

        public ReadOnlyObservableCollection<FirmwarePackageViewModel> Packages { get; private set; }

        public override FirmwarePackageViewModel SelectedPackage
        {
            get { return _selectedPackage; }
            set
            {
                if (Equals(value, _selectedPackage)) return;
                _selectedPackage = value;
                OnSelectedPackageChanged();
            }
        }
    }

    public interface IFirmwarePackageViewModelKeyFormatter
    {
        String GetKey(IRepositoryElement Element);
    }

    internal class FirmwarePackageViewModelKeyFormatter : IFirmwarePackageViewModelKeyFormatter
    {
        public string GetKey(IRepositoryElement Element)
        {
            return String.Format("{0}.{1} {2}", Element.Information.FirmwareVersion.Major, Element.Information.FirmwareVersion.Minor,
                                 Element.Information.FirmwareVersionLabel);
        }
    }
}
