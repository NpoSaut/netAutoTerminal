﻿using System;
using System.Collections.Generic;
using System.Linq;
using FirmwareBurner.Annotations;
using FirmwareBurner.Burning;
using FirmwareBurner.Settings;
using FirmwareBurner.Validation;
using FirmwarePacking.SystemsIndexes;
using Microsoft.Practices.Prism.Events;

namespace FirmwareBurner.ViewModels
{
    [UsedImplicitly]
    public class BurningViewModelFactory : IBurningViewModelFactory
    {
        private readonly IBurningService _burningService;

        private readonly IEventAggregator _eventAggregator;
        private readonly IIndexHelper _indexHelper;

        private readonly ISettingsService _settingsService;

        public BurningViewModelFactory(IBurningReceiptsCatalog BurningReceiptsCatalog, IIndexHelper IndexHelper, IExceptionService ExceptionService,
                                       IBurningService BurningService, IEventAggregator EventAggregator, ISettingsService SettingsService)
        {
            _indexHelper = IndexHelper;
            _burningService = BurningService;
            _eventAggregator = EventAggregator;
            _settingsService = SettingsService;
        }

        public BurningViewModel GetViewModel(int CellKindId, int ModificationId, IValidationContext ValidationContext, IProjectAssembler projectAssembler)
        {
            BlockKind cellKind = _indexHelper.GetCell(CellKindId);
            ModificationKind modification = _indexHelper.GetModification(cellKind, ModificationId);

            List<BurningOptionViewModel> burningOptions = Enumerable.Range(1, cellKind.ChannelsCount)
                                                                    .Select(i => new BurningOptionViewModel(String.Format("Канал {0}", i), i))
                                                                    .ToList();
            List<BurningMethodViewModel> burningMethods = _burningService.GetBurningMethods(modification.DeviceName)
                                                                         .Select(
                                                                             burningMethod =>
                                                                             new BurningMethodViewModel(burningMethod.Name, burningMethod.Receipt))
                                                                         .ToList();

            var viewModel = new BurningViewModel(burningOptions, burningMethods);
            var controller = new BurningViewModelController(viewModel, CellKindId, _eventAggregator, _settingsService, ValidationContext, projectAssembler,
                                                            _burningService);
            return viewModel;
        }
    }
}
