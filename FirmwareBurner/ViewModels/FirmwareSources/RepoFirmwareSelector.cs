using System;
using System.Collections.ObjectModel;
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

        public override FirmwarePackage SelectedPackage
        {
            get { throw new NotImplementedException(); }
        }
    }
}
