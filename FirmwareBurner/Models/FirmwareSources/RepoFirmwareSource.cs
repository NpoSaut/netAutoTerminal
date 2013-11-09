using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FirmwarePacking;
using System.Collections.ObjectModel;
using System.IO;
using FirmwarePacking.Repositories;

namespace FirmwareBurner.Models.FirmwareSources
{
    public class RepoFirmwareSource : FirmwareSource
    {
        public Repository[] Repositories { get; set; }
        public ObservableCollection<FirmwarePackage> PackagesForTarget { get; private set; }

        public RepoFirmwareSource(Repository[] Repositories)
        {
            this.Repositories = Repositories;
            PackagesForTarget = new ObservableCollection<FirmwarePackage>();
        }

        protected override void OnCheckTarget(ComponentTarget target)
        {
            base.OnCheckTarget(target);
            PackagesForTarget.Clear();

            foreach (var p in
                    Repositories.SelectMany(repo => repo.GetPackagesForTargets(target))
                                .OrderByDescending(fw => fw.Information.FirmwareVersion))
                PackagesForTarget.Add(p);
            
            SelectedPackage = PackagesForTarget.FirstOrDefault();
        }
    }
}
