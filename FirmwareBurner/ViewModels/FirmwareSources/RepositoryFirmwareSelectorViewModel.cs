using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using FirmwarePacking;
using FirmwarePacking.Repositories;
using Microsoft.Practices.Prism.Commands;

namespace FirmwareBurner.ViewModels.FirmwareSources
{
    public class RepositoryFirmwareSelectorViewModel : FirmwareSelectorViewModel
    {
        private IRepositoryElement _selectedElement;
        private FirmwarePackage _selectedPackage;

        public RepositoryFirmwareSelectorViewModel(string Name, IRepository Repository, ICollection<ComponentTarget> RequiredTargets) : base(Name)
        {
            Packages = new ObservableCollection<IRepositoryElement>(
                Repository.GetPackagesForTargets(RequiredTargets).ToList());

            GetPackageCommand = new DelegateCommand(GetPackage, CanGetPackage);
        }

        public IRepositoryElement SelectedElement
        {
            get { return _selectedElement; }
            set
            {
                if (Equals(value, _selectedElement)) return;
                _selectedElement = value;
                RaisePropertyChanged("SelectedElement");
                GetPackageCommand.RaiseCanExecuteChanged();
            }
        }

        public DelegateCommand GetPackageCommand { get; private set; }

        public ObservableCollection<IRepositoryElement> Packages { get; private set; }

        public override FirmwarePackage SelectedPackage
        {
            get { return _selectedPackage; }
        }

        private bool CanGetPackage() { return SelectedElement != null; }

        private void GetPackage()
        {
            _selectedPackage = SelectedElement.GetPackage();
            OnSelectedPackageChanged();
        }
    }
}
