using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading;
using System.Windows.Input;
using FirmwareBurner.Annotations;
using FirmwareBurner.ViewModels.FirmwareSources.LoadControllers;
using FirmwarePacking;
using FirmwarePacking.Repositories;
using Microsoft.Practices.Prism.Commands;

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

        public IntegratedFirmwareSelectorViewModel(INotifyRepository Repository, List<ComponentTarget> RequiredTargets,
                                                   ILoadControllerFactory LoadControllerFactory) : base("Интегрированный")
        {
            _packages = new ObservableCollection<FirmwarePackageViewModel>();
            Packages = new ReadOnlyObservableCollection<FirmwarePackageViewModel>(_packages);
            _cancellationTokenSource = new CancellationTokenSource();

            BrowseFolderCommand = new DelegateCommand(ExecuteMethod);

            IRepositoryLoadController lc = LoadControllerFactory.GetLocalLoadController(Repository, _packages, RequiredTargets, _cancellationTokenSource);
            lc.BeginLoad();
        }

        public ICommand BrowseFolderCommand { get; private set; }

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

        private void ExecuteMethod() { Process.Start(DirectoryRepository.UserRepositoryDirectory); }
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
