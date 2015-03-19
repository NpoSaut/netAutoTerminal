using System.Collections.Generic;
using System.Threading;
using FirmwarePacking;
using FirmwarePacking.Repositories;
using Microsoft.Practices.Prism.Events;

namespace FirmwareBurner.ViewModels.FirmwareSources.LoadControllers
{
    internal class RemoteRepositoryLoadController : DispatcherRepositoryLoadControllerBase
    {
        private readonly IFirmwarePackageViewModelFactory _packageViewModelFactory;

        public RemoteRepositoryLoadController(INotifyRepository Repository, ICollection<FirmwarePackageViewModel> PackagesCollection,
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
                                                      new FirmwarePackageAvailabilityViewModel(false),
                                                      Element.Status);
            AddModel(viewModel);
        }

        protected override void UpdateExistingViewModel(FirmwarePackageViewModel ExistingViewModel, IRepositoryElement Element)
        {
            ExistingViewModel.Status = Element.Status;
        }
    }
}
