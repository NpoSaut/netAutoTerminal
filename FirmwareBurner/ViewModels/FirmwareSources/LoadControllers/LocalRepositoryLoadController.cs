using System.Collections.Generic;
using System.Threading;
using FirmwarePacking;
using FirmwarePacking.Repositories;
using Microsoft.Practices.Prism.Events;

namespace FirmwareBurner.ViewModels.FirmwareSources.LoadControllers
{
    internal class LocalRepositoryLoadController : DispatcherRepositoryLoadControllerBase
    {
        private readonly IFirmwarePackageViewModelFactory _packageViewModelFactory;

        public LocalRepositoryLoadController(INotifyRepository Repository, ICollection<FirmwarePackageViewModel> PackagesCollection,
                                             List<ComponentTarget> RequiredTargets, IDispatcherFacade Dispatcher,
                                             IFirmwarePackageViewModelKeyFormatter KeyFormatter, CancellationTokenSource CancellationTokenSource,
                                             IFirmwarePackageViewModelFactory PackageViewModelFactory)
            : base(Repository, PackagesCollection, RequiredTargets, Dispatcher, KeyFormatter, CancellationTokenSource)
        {
            _packageViewModelFactory = PackageViewModelFactory;
        }

        protected override void AddNewViewModel(string ElementKey, IRepositoryElement Element)
        {
            FirmwarePackageViewModel viewModel =
                _packageViewModelFactory.GetViewModel(ElementKey, Element,
                                                      new FirmwarePackageAvailabilityViewModel(true),
                                                      ReleaseStatus.Unknown);
            AddModel(viewModel);
        }

        protected override void UpdateExistingViewModel(FirmwarePackageViewModel ExistingViewModel, IRepositoryElement Element)
        {
            ExistingViewModel.Availability = new FirmwarePackageAvailabilityViewModel(true);
        }
    }
}
