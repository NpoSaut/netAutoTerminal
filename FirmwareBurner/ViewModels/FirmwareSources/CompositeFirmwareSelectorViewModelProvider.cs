using System;
using System.Collections.Generic;
using System.Linq;
using FirmwarePacking;
using FirmwarePacking.Repositories;

namespace FirmwareBurner.ViewModels.FirmwareSources
{
    public class CompositeFirmwareSelectorViewModelProvider : IFirmwareSelectorViewModelProvider
    {
        private readonly Lazy<DirectoryRepository> _userDirectoryRepository;
        public CompositeFirmwareSelectorViewModelProvider() { _userDirectoryRepository = new Lazy<DirectoryRepository>(GetUserRepository); }

        public FirmwareSelectorViewModel GetViewModel(int CellKindId, int ModificationId, int ModuleId, int ChannelsCount)
        {
            List<ComponentTarget> requiredTargets = Enumerable.Range(1, ChannelsCount)
                                                              .Select(channel => new ComponentTarget(CellKindId, ModificationId, channel, ModuleId))
                                                              .ToList();

            return new CompositeFirmwareSelectorViewModel(
                new FirmwareSelectorViewModel[]
                {
                    new ManualFirmwareSelectorViewModel("Из файла", requiredTargets),
                    new RepositoryFirmwareSelectorViewModel("Из папки", _userDirectoryRepository.Value, requiredTargets)
                });
        }

        private DirectoryRepository GetUserRepository() { return new DirectoryRepository(DirectoryRepository.UserRepositoryDirectory); }
    }
}
