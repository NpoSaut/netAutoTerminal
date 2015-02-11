using System.Collections.Generic;
using System.Threading;
using FirmwarePacking.Repositories;
using Microsoft.Practices.Prism.Events;

namespace FirmwareBurner.ViewModels.FirmwareSources.LoadControllers
{
    internal class RemoteRepositoryLoadController : DispatcherRepositoryLoadControllerBase
    {
        public RemoteRepositoryLoadController(IRepositoryLoader Loader, ICollection<FirmwarePackageViewModel> PackagesCollection, IDispatcherFacade Dispatcher, IFirmwarePackageViewModelKeyFormatter KeyFormatter, CancellationTokenSource CancellationTokenSource)
            : base(Loader, PackagesCollection, Dispatcher, KeyFormatter, CancellationTokenSource) { }

        protected override void AddNewViewModel(string ElementKey, IRepositoryElement Element)
        {
            var viewModel =
                new FirmwarePackageViewModel(ElementKey,
                                             new FirmwareVersionViewModel(Element.Information.FirmwareVersion.ToString(2),
                                                                          Element.Information.FirmwareVersionLabel,
                                                                          Element.Information.ReleaseDate),
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