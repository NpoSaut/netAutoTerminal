using System.Collections.ObjectModel;
using System.Linq;
using FirmwarePacking;
using FirmwarePacking.Repositories;

namespace FirmwareBurner.ViewModels.FirmwareSources
{
    public class RepoFirmwareSelector : FirmwareSelectorViewModel
    {
        public RepoFirmwareSelector(string Name, Repository[] Repositories) : base(Name)
        {
            this.Repositories = Repositories;
            PackagesForTarget = new ObservableCollection<FirmwarePackage>();
        }

        public Repository[] Repositories { get; set; }
        public ObservableCollection<FirmwarePackage> PackagesForTarget { get; private set; }

        protected override void OnCheckTarget(ComponentTarget target)
        {
            base.OnCheckTarget(target);
            PackagesForTarget.Clear();

            foreach (FirmwarePackage p in
                Repositories.SelectMany(repo => repo.GetPackagesForTargets(target))
                            .OrderByDescending(fw => fw.Information.FirmwareVersion))
                PackagesForTarget.Add(p);

            SelectedPackage = PackagesForTarget.FirstOrDefault();
        }
    }
}
