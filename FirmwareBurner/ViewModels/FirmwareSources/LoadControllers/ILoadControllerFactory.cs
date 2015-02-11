using System.Collections.Generic;
using System.Threading;
using FirmwareBurner.Annotations;
using Microsoft.Practices.Prism.Events;

namespace FirmwareBurner.ViewModels.FirmwareSources.LoadControllers
{
    public interface ILoadControllerFactory
    {
        IRepositoryLoadController GetLocalLoadController(IRepositoryLoader Loader, ICollection<FirmwarePackageViewModel> PackagesCollection, CancellationTokenSource CancellationTokenSource);
        IRepositoryLoadController GetRemoteLoadController(IRepositoryLoader Loader, ICollection<FirmwarePackageViewModel> PackagesCollection, CancellationTokenSource CancellationTokenSource);
    }

    [UsedImplicitly]
    internal class LoadControllerFactory : ILoadControllerFactory
    {
        private readonly IDispatcherFacade _dispatcher;
        private readonly IFirmwarePackageViewModelKeyFormatter _keyFormatter;

        public LoadControllerFactory(IDispatcherFacade Dispatcher, IFirmwarePackageViewModelKeyFormatter KeyFormatter)
        {
            _dispatcher = Dispatcher;
            _keyFormatter = KeyFormatter;
        }

        public IRepositoryLoadController GetLocalLoadController(IRepositoryLoader Loader, ICollection<FirmwarePackageViewModel> PackagesCollection, CancellationTokenSource CancellationTokenSource)
        {
            return new LocalRepositoryLoadController(Loader, PackagesCollection, _dispatcher, _keyFormatter, CancellationTokenSource);
        }

        public IRepositoryLoadController GetRemoteLoadController(IRepositoryLoader Loader, ICollection<FirmwarePackageViewModel> PackagesCollection, CancellationTokenSource CancellationTokenSource)
        {
            return new RemoteRepositoryLoadController(Loader, PackagesCollection, _dispatcher, _keyFormatter, CancellationTokenSource);
        }
    }
}
