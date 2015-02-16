﻿using System.Collections.Generic;
using System.Linq;
using FirmwareBurner.Annotations;
using FirmwareBurner.ViewModels.FirmwareSources.LoadControllers;
using FirmwarePacking;
using FirmwarePacking.Repositories;

namespace FirmwareBurner.ViewModels.FirmwareSources
{
    [UsedImplicitly]
    public class IntegratedFirmwareSelectorViewModelProvider : IFirmwareSelectorViewModelProvider
    {
        private readonly ILoadControllerFactory _loadControllerFactory;
        private readonly DirectoryRepository _userDirectoryRepository;

        public IntegratedFirmwareSelectorViewModelProvider(ILoadControllerFactory LoadControllerFactory)
        {
            _loadControllerFactory = LoadControllerFactory;
            _userDirectoryRepository = _userDirectoryRepository = new DirectoryRepository(DirectoryRepository.UserRepositoryDirectory);
        }

        public FirmwareSelectorViewModel GetViewModel(int CellKindId, int ModificationId, int ModuleId, int ChannelsCount)
        {
            List<ComponentTarget> requiredTargets = Enumerable.Range(1, ChannelsCount)
                                                              .Select(channel => new ComponentTarget(CellKindId, ModificationId, channel, ModuleId))
                                                              .ToList();

            //return new IntegratedFirmwareSelectorViewModel(
            //    new[]
            //    {
            //        new BackgroundRepositoryLoader(new FakeRepository(0, 6, 500, false), requiredTargets)
            //    },
            //    new[]
            //    {
            //        new BackgroundRepositoryLoader(new FakeRepository(2, 8, 2000, true), requiredTargets)
            //    },
            //    _loadControllerFactory);

            return new IntegratedFirmwareSelectorViewModel(
                new[]
                {
                    new BackgroundRepositoryLoader(_userDirectoryRepository, requiredTargets)
                },
                new IRepositoryLoader[0],
                _loadControllerFactory);
        }
    }
}
