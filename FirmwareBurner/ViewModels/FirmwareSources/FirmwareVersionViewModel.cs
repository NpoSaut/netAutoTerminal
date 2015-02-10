using System;
using System.Linq;
using FirmwareBurner.ViewModels.Bases;

namespace FirmwareBurner.ViewModels.FirmwareSources
{
    public class FirmwareVersionViewModel : ViewModelBase
    {
        public FirmwareVersionViewModel(String Version, string Label, DateTime ReleaseDate)
        {
            this.ReleaseDate = ReleaseDate;
            this.Version = Version;
            this.Label = Label;
        }

        public String Version { get; private set; }
        public String Label { get; private set; }
        public DateTime ReleaseDate { get; private set; }

        public Boolean HaveLabel
        {
            get { return string.IsNullOrWhiteSpace(Label); }
        }

        public override string ToString() { return string.Join(" ", new[] { Version, Label }.Where(i => i != null)); }
    }
}
