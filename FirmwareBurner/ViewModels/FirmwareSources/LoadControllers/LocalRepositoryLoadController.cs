using System.Collections.Generic;
using System.Threading;
using FirmwarePacking.Repositories;
using Microsoft.Practices.Prism.Events;

namespace FirmwareBurner.ViewModels.FirmwareSources.LoadControllers
{
    internal class LocalRepositoryLoadController : DispatcherRepositoryLoadControllerBase
    {
        private readonly IFirmwarePackageViewModelFactory _packageViewModelFactory;

        public LocalRepositoryLoadController(IRepositoryLoader Loader, ICollection<FirmwarePackageViewModel> PackagesCollection, IDispatcherFacade Dispatcher,
                                             IFirmwarePackageViewModelKeyFormatter KeyFormatter, CancellationTokenSource CancellationTokenSource,
                                             IFirmwarePackageViewModelFactory PackageViewModelFactory)
            : base(Loader, PackagesCollection, Dispatcher, KeyFormatter, CancellationTokenSource)
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
