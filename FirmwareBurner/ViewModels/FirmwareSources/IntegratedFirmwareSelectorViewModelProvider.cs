using System;
using System.Collections.Generic;
using System.Linq;
using FirmwareBurner.Annotations;
using FirmwarePacking;
using FirmwarePacking.Repositories;

namespace FirmwareBurner.ViewModels.FirmwareSources
{
    [UsedImplicitly]
    public class IntegratedFirmwareSelectorViewModelProvider : IFirmwareSelectorViewModelProvider
    {
        private readonly Lazy<DirectoryRepository> _userDirectoryRepository;
        public IntegratedFirmwareSelectorViewModelProvider()
        {
            _userDirectoryRepository = _userDirectoryRepository = new Lazy<DirectoryRepository>(GetUserRepository); ;
        }

        public FirmwareSelectorViewModel GetViewModel(int CellKindId, int ModificationId, int ModuleId, int ChannelsCount)
        {
            List<ComponentTarget> requiredTargets = Enumerable.Range(1, ChannelsCount)
                                                              .Select(channel => new ComponentTarget(CellKindId, ModificationId, channel, ModuleId))
                                                              .ToList();

            return new IntegratedFirmwareSelectorViewModel(_userDirectoryRepository.Value, requiredTargets);
        }

        private DirectoryRepository GetUserRepository() { return new DirectoryRepository(DirectoryRepository.UserRepositoryDirectory); }
    }
}