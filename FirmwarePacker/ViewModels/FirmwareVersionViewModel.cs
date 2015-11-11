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
            var versionParts = Version.Split('.');
            var major = versionParts.Length > 0
                            ? int.Parse(versionParts[0])
                            : 0;
            var minor = versionParts.Length > 1
                            ? int.Parse(versionParts[1])
                            : 0;
            return new PackageVersion(major, minor, Label, ReleaseDate);
        }
    }
}
