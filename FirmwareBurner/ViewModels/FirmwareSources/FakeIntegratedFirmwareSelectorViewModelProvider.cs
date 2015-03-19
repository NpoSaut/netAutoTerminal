using System.Collections.Generic;
using System.Linq;
using FirmwareBurner.Fakes;
using FirmwareBurner.ViewModels.FirmwareSources.LoadControllers;
using FirmwarePacking;

namespace FirmwareBurner.ViewModels.FirmwareSources
{
    /// <summary>Создаёт фальшивый <seealso cref="IntegratedFirmwareSelectorViewModel" />
    /// </summary>
    public class FakeIntegratedFirmwareSelectorViewModelProvider : IFirmwareSelectorViewModelProvider
    {
        private readonly ILoadControllerFactory _loadControllerFactory;

        public FakeIntegratedFirmwareSelectorViewModelProvider(ILoadControllerFactory LoadControllerFactory) { _loadControllerFactory = LoadControllerFactory; }

        public FirmwareSelectorViewModel GetViewModel(int CellKindId, int ModificationId, int ModuleId, int ChannelsCount)
        {
            List<ComponentTarget> requiredTargets = Enumerable.Range(1, ChannelsCount)
                                                              .Select(channel => new ComponentTarget(CellKindId, ModificationId, channel, ModuleId))
                                                              .ToList();

            return new IntegratedFirmwareSelectorViewModel(
                new FakeRepository(0, 6, false),
                requiredTargets,
                _loadControllerFactory);
        }
    }
}
