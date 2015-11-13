using System.Collections.Generic;

namespace FirmwarePacker.ViewModels
{
    public class TargetViewModel : ViewModel
    {
        private static readonly IEqualityComparer<TargetViewModel> _comparerInstance = new EqualityComparer();

        public TargetViewModel(string Cell, string Module)
        {
            this.Cell = Cell;
            this.Module = Module;
        }

        public string Cell { get; private set; }
        public string Module { get; private set; }

        public static IEqualityComparer<TargetViewModel> Comparer
        {
            get { return _comparerInstance; }
        }

        private sealed class EqualityComparer : IEqualityComparer<TargetViewModel>
        {
            public bool Equals(TargetViewModel x, TargetViewModel y)
            {
                if (ReferenceEquals(x, y)) return true;
                if (ReferenceEquals(x, null)) return false;
                if (ReferenceEquals(y, null)) return false;
                if (x.GetType() != y.GetType()) return false;
                return string.Equals(x.Cell, y.Cell) && string.Equals(x.Module, y.Module);
            }

            public int GetHashCode(TargetViewModel obj)
            {
                unchecked
                {
                    return ((obj.Cell != null ? obj.Cell.GetHashCode() : 0) * 397) ^ (obj.Module != null ? obj.Module.GetHashCode() : 0);
                }
            }
        }
    }
}