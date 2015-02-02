using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using FirmwarePacking;
using FirmwarePacking.Repositories;

namespace FirmwareBurner.ViewModels.FirmwareSources
{
    public class RepositoryFirmwareSelectorViewModel : FirmwareSelectorViewModel
    {
        private readonly IRepository _repository;
        private FirmwarePackage _selectedPackage;

        public RepositoryFirmwareSelectorViewModel(string Name, IRepository Repository, ICollection<ComponentTarget> RequiredTargets) : base(Name)
        {
            _repository = Repository;
            Packages = new ObservableCollection<IRepositoryElement>(
                _repository.GetPackagesForTargets(RequiredTargets).ToList());
        }

        public ObservableCollection<IRepositoryElement> Packages { get; private set; }

        public override FirmwarePackage SelectedPackage
        {
            get { return _selectedPackage; }
        }
    }
}
