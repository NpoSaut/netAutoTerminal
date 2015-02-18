using System;
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

        public string FullVersion
        {
            get { return HaveLabel ? String.Format("{0} {1}", Version, Label) : Version; }
        }

        public Boolean HaveLabel
        {
            get { return !string.IsNullOrWhiteSpace(Label); }
        }

        public override string ToString() { return String.Format("Версия {0} от {1:d}", FullVersion, ReleaseDate); }
    }
}
