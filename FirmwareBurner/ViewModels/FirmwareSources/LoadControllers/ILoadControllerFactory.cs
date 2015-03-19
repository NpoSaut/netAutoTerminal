using System.Collections.Generic;
using System.Threading;
using FirmwareBurner.Annotations;
using FirmwarePacking;
using FirmwarePacking.Repositories;
using Microsoft.Practices.Prism.Events;

namespace FirmwareBurner.ViewModels.FirmwareSources.LoadControllers
{
    public interface ILoadControllerFactory
    {
        IRepositoryLoadController GetLocalLoadController(INotifyRepository Repository, ICollection<FirmwarePackageViewModel> PackagesCollection,
                                                         List<ComponentTarget> RequiredTargets, CancellationTokenSource CancellationTokenSource);

        IRepositoryLoadController GetRemoteLoadController(INotifyRepository Repository, ICollection<FirmwarePackageViewModel> PackagesCollection,
                                                          List<ComponentTarget> RequiredTargets, CancellationTokenSource CancellationTokenSource);
    }

    [UsedImplicitly]
    internal class LoadControllerFactory : ILoadControllerFactory
    {
        private readonly IDispatcherFacade _dispatcher;
        private readonly IFirmwarePackageViewModelFactory _firmwarePackageViewModelFactory;
        private readonly IFirmwarePackageViewModelKeyFormatter _keyFormatter;

        public LoadControllerFactory(IDispatcherFacade Dispatcher, IFirmwarePackageViewModelKeyFormatter KeyFormatter,
                                     IFirmwarePackageViewModelFactory FirmwarePackageViewModelFactory)
        {
            _dispatcher = Dispatcher;
            _keyFormatter = KeyFormatter;
            _firmwarePackageViewModelFactory = FirmwarePackageViewModelFactory;
        }

        public IRepositoryLoadController GetLocalLoadController(INotifyRepository Repository, ICollection<FirmwarePackageViewModel> PackagesCollection,
                                                                List<ComponentTarget> RequiredTargets, CancellationTokenSource CancellationTokenSource)
        {
            return new LocalRepositoryLoadController(Repository, PackagesCollection, RequiredTargets,
                                                     _dispatcher, _keyFormatter, CancellationTokenSource, _firmwarePackageViewModelFactory);
        }

        public IRepositoryLoadController GetRemoteLoadController(INotifyRepository Repository, ICollection<FirmwarePackageViewModel> PackagesCollection,
                                                                 List<ComponentTarget> RequiredTargets, CancellationTokenSource CancellationTokenSource)
        {
            return new RemoteRepositoryLoadController(Repository, PackagesCollection, RequiredTargets, _dispatcher, _keyFormatter, CancellationTokenSource,
                                                      _firmwarePackageViewModelFactory);
        }
    }
}
