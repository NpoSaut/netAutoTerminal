using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FirmwarePacking;
using System.Collections.ObjectModel;
using System.IO;

namespace FirmwareBurner.Models.FirmwareSources
{
    public class RepoFirmwareSource : FirmwareSource
    {
        private ILookup<ComponentTarget, FirmwarePackage> AllPackages { get; set; }
        public ObservableCollection<FirmwarePackage> PackagesForTarget { get; private set; }

        public RepoFirmwareSource()
        {
            var PackageFiles = (new DirectoryInfo("repository")).EnumerateFiles("*." + FirmwarePackage.FirmwarePackageExtension);
            AllPackages =
                PackageFiles
                    .Select(pf => FirmwarePackage.Open(pf))
                    .SelectMany(fw => fw.Components
                        .SelectMany(c => c.Targets.Select(t => new { t, fw })))
                //.GroupBy(x => x.t)
                .ToLookup(x => x.t, x => x.fw);

            PackagesForTarget = new ObservableCollection<FirmwarePackage>();
        }

        protected override void OnCheckTarget(ComponentTarget target)
        {
            base.OnCheckTarget(target);
            PackagesForTarget.Clear();
            foreach (var p in AllPackages[target].OrderByDescending(fw => fw.Information.FirmwareVersion))
                PackagesForTarget.Add(p);
            SelectedPackage = PackagesForTarget.FirstOrDefault();
        }
    }
}
