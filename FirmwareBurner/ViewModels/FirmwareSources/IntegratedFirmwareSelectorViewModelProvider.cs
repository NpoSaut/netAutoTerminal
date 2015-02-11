using System;
using System.Collections.Generic;
using System.Linq;
using FirmwareBurner.Annotations;
using FirmwareBurner.Fakes;
using FirmwarePacking;
using FirmwarePacking.Repositories;
using Microsoft.Practices.Prism.Events;

namespace FirmwareBurner.ViewModels.FirmwareSources
{
    [UsedImplicitly]
    public class IntegratedFirmwareSelectorViewModelProvider : IFirmwareSelectorViewModelProvider
    {
        private IDispatcherFacade _dispatcher;
        //private readonly Lazy<DirectoryRepository> _userDirectoryRepository;
        public IntegratedFirmwareSelectorViewModelProvider(IDispatcherFacade Dispatcher)
        {
            _dispatcher = Dispatcher;
            //_userDirectoryRepository = _userDirectoryRepository = new Lazy<DirectoryRepository>(GetUserRepository); ;
        }

        public FirmwareSelectorViewModel GetViewModel(int CellKindId, int ModificationId, int ModuleId, int ChannelsCount)
        {
            List<ComponentTarget> requiredTargets = Enumerable.Range(1, ChannelsCount)
                                                              .Select(channel => new ComponentTarget(CellKindId, ModificationId, channel, ModuleId))
                                                              .ToList();

            return new IntegratedFirmwareSelectorViewModel(new BackgroundRepositoryLoader(new FakeRepository(2000), requiredTargets), _dispatcher);
        }

        private DirectoryRepository GetUserRepository() { return new DirectoryRepository(DirectoryRepository.UserRepositoryDirectory); }
    }
}