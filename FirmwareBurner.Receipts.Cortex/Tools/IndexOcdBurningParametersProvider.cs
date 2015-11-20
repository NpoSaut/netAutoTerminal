using FirmwarePacking.SystemsIndexes;

namespace FirmwareBurner.Receipts.Cortex.Tools
{
    /// <summary>Провайдер названия board для ячейки, работающий через <see cref="IIndex" />.</summary>
    internal class IndexOcdBurningParametersProvider : IOcdBurningParametersProvider
    {
        private readonly IIndexHelper _indexHelper;
        public IndexOcdBurningParametersProvider(IIndexHelper IndexHelper) { _indexHelper = IndexHelper; }

        /// <summary>Находит BoardName для ячейки по её идентификатору и модификации</summary>
        /// <param name="Target">Цель прошивки</param>
        public string GetBoardName(TargetInformation Target)
        {
            ModificationKind modification = _indexHelper.GetModification(Target.CellId, Target.ModificationId);
            return modification.CustomProperties["board"];
        }

        /// <summary>Находит TargetName для ячейки</summary>
        /// <param name="Target">Цель прошивки</param>
        public string GetTargetName(TargetInformation Target)
        {
            ModificationKind modification = _indexHelper.GetModification(Target.CellId, Target.ModificationId);
            string unichannelProperty = modification.CustomProperties["unichannel"];
            return unichannelProperty != null && bool.Parse(unichannelProperty)
                       ? string.Format("cpu{0}", Target.ChannelNumber)
                       : null;
        }
    }
}
