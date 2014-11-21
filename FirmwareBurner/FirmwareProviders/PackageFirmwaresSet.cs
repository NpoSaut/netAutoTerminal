using System.Collections.Generic;
using System.Linq;
using FirmwarePacking;

namespace FirmwareBurner.FirmwareProviders
{
    /// <summary>Набор прошивок, основанный на наборе файлов пакетов прошивки</summary>
    /// <remarks>
    ///     Хранит идентификаторы цели прошивки и словарь, в котором указано, для какого модуля какой пакет ПО
    ///     используется. При запросе прошивки находит в словаре пакет ПО, указанный для запрашиваемого модуля и находит
    ///     компонент, соответствующий идентификаторам цели
    /// </remarks>
    public class PackageFirmwaresSet : IFirmwaresSet
    {
        private readonly IDictionary<int, FirmwarePackage> _packages;

        public PackageFirmwaresSet(TargetHardwareIdentifier Target, IDictionary<int, FirmwarePackage> Packages)
        {
            this.Target = Target;
            _packages = Packages;
        }

        /// <summary>Идентификатор цели прошивки</summary>
        public TargetHardwareIdentifier Target { get; private set; }

        /// <summary>Список идентификаторов модулей в наборе</summary>
        public ICollection<int> Modules
        {
            get { return _packages.Keys; }
        }

        /// <summary>Получает информацию о прошивке для указанного модуля</summary>
        /// <param name="ModuleId">Идентификатор модуля</param>
        public PackageInformation GetFirmwareInformation(int ModuleId) { return _packages[ModuleId].Information; }

        /// <summary>Получает компонент для указанного модуля из набора</summary>
        /// <param name="ModuleId">Идентификатор модуля</param>
        public FirmwareComponent GetComponent(int ModuleId)
        {
            return
                _packages[ModuleId].Components
                                   .Single(c => c.Targets.Any(CheckComponentTargetIsAppropriate));
        }

        private bool CheckComponentTargetIsAppropriate(ComponentTarget ComponentTarget)
        {
            return ComponentTarget.CellId == Target.CellId &&
                   ComponentTarget.CellModification == Target.ModificationId &&
                   ComponentTarget.Channel == Target.ChannelNumber;
        }
    }
}
