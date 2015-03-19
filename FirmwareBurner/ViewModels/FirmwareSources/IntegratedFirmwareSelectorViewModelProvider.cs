using System.Collections.Generic;
using System.Linq;
using FirmwareBurner.Annotations;
using FirmwareBurner.ViewModels.FirmwareSources.LoadControllers;
using FirmwarePacking;
using FirmwarePacking.Repositories;

namespace FirmwareBurner.ViewModels.FirmwareSources
{
    /// <summary>
    ///     Фабрика для <seealso cref="IntegratedFirmwareSelectorViewModel" />, создающая селекторы на основе локального
    ///     репозитория.
    /// </summary>
    [UsedImplicitly]
    public class IntegratedFirmwareSelectorViewModelProvider : IFirmwareSelectorViewModelProvider
    {
        private readonly ILoadControllerFactory _loadControllerFactory;
        private readonly NotifyDirectoryRepository _userDirectoryRepository;

        public IntegratedFirmwareSelectorViewModelProvider(ILoadControllerFactory LoadControllerFactory)
        {
            _loadControllerFactory = LoadControllerFactory;
            _userDirectoryRepository = _userDirectoryRepository = new NotifyDirectoryRepository(DirectoryRepository.UserRepositoryDirectory);
        }

        public FirmwareSelectorViewModel GetViewModel(int CellKindId, int ModificationId, int ModuleId, int ChannelsCount)
        {
            List<ComponentTarget> requiredTargets = Enumerable.Range(1, ChannelsCount)
                                                              .Select(channel => new ComponentTarget(CellKindId, ModificationId, channel, ModuleId))
                                                              .ToList();
            return new IntegratedFirmwareSelectorViewModel(
                _userDirectoryRepository,
                requiredTargets,
                _loadControllerFactory);
        }
    }
}
