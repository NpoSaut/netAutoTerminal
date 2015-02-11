using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using FirmwareBurner.Annotations;
using FirmwareBurner.ViewModels.FirmwareSources.LoadControllers;
using FirmwarePacking;
using FirmwarePacking.Repositories;

namespace FirmwareBurner.ViewModels.FirmwareSources
{
    public class IntegratedFirmwareSelectorViewModel : FirmwareSelectorViewModel
    {
        private readonly CancellationTokenSource _cancellationTokenSource;
        private readonly ObservableCollection<FirmwarePackageViewModel> _packages;
        private ILoadControllerFactory _loadControllerFactory;

        public IntegratedFirmwareSelectorViewModel([NotNull] IList<FirmwarePackageViewModel> Packages) : base("Интегрированный")
        {
            _packages = new ObservableCollection<FirmwarePackageViewModel>(Packages);
            this.Packages = new ReadOnlyObservableCollection<FirmwarePackageViewModel>(_packages);
        }

        public IntegratedFirmwareSelectorViewModel(ICollection<IRepositoryLoader> LocalLoaders, ICollection<IRepositoryLoader> RemoteLoaders,
                                                   ILoadControllerFactory LoadControllerFactory) : base("Интегрированный")
        {
            _loadControllerFactory = LoadControllerFactory;
            _packages = new ObservableCollection<FirmwarePackageViewModel>();
            Packages = new ReadOnlyObservableCollection<FirmwarePackageViewModel>(_packages);

            _cancellationTokenSource = new CancellationTokenSource();

            foreach (IRepositoryLoader loader in LocalLoaders)
                _loadControllerFactory.GetLocalLoadController(loader, _packages, _cancellationTokenSource).BeginLoad();

            foreach (IRepositoryLoader loader in RemoteLoaders)
                _loadControllerFactory.GetRemoteLoadController(loader, _packages, _cancellationTokenSource).BeginLoad();
        }

        public ReadOnlyObservableCollection<FirmwarePackageViewModel> Packages { get; private set; }

        public override FirmwarePackage SelectedPackage
        {
            get { return null; }
        }

        private string GetElementKey(IRepositoryElement element)
        {
            return String.Format("{0}.{1} {2}", element.Information.FirmwareVersion.Major, element.Information.FirmwareVersion.Minor,
                                 element.Information.FirmwareVersionLabel);
        }
    }

    internal interface IFirmwarePackageViewModelKeyFormatter
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
