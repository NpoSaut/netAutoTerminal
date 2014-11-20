using System;
using System.Linq;
using FirmwareBurner.ViewModels.FirmwareSources;
using FirmwarePacking.SystemsIndexes;

namespace FirmwareBurner.ViewModels
{
    public class FirmwareSetConstructorViewModelProvider : IFirmwareSetConstructorViewModelProvider
    {
        private readonly IFirmwareSelectorViewModelProvider _firmwareSelectorViewModelProvider;
        private readonly IIndex _index;

        public FirmwareSetConstructorViewModelProvider(IIndex Index, IFirmwareSelectorViewModelProvider FirmwareSelectorViewModelProvider)
        {
            _index = Index;
            _firmwareSelectorViewModelProvider = FirmwareSelectorViewModelProvider;
        }

        public FirmwareSetConstructorViewModel GetViewModel(int CellKindId, int CellModificationId)
        {
            BlockKind block = _index.Blocks.FirstOrDefault(c => c.Id == CellKindId);
            if (block == null) throw new ArgumentException("Типа ячейки с указанным идентификатором не существует в каталоге", "CellKindId");
            return
                new FirmwareSetConstructorViewModel(
                    block.Modules.Select(module =>
                                         new FirmwareSetComponentViewModel(module.Id,
                                                                           module.Name,
                                                                           _firmwareSelectorViewModelProvider.GetViewModel(CellKindId, CellModificationId)))
                         .ToList());
        }
    }
}