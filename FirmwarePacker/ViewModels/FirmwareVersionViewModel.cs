using System;
using FirmwarePacker.Project;

namespace FirmwarePacker.ViewModels
{
    public class FirmwareVersionViewModel : ViewModel
    {
        public FirmwareVersionViewModel(string Version, string Label, DateTime ReleaseDate)
        {
            this.Version = Version;
            this.Label = Label;
            this.ReleaseDate = ReleaseDate;
        }

        public string Version { get; set; }
        public string Label { get; set; }
        public DateTime ReleaseDate { get; set; }

        public PackageVersion GetModel()
        {
            string[] versionParts = Version.Split('.');
            int major = versionParts.Length > 0
                            ? int.Parse(versionParts[0])
                            : 0;
            int minor = versionParts.Length > 1
                            ? int.Parse(versionParts[1])
                            : 0;
            string label = string.IsNullOrWhiteSpace(Label)
                               ? Label.Substring(0, Math.Min(Label.Length, 4))
                               : null;
            return new PackageVersion(major, minor, label, ReleaseDate);
        }
    }
}
