using System;
using System.Linq;
using FirmwareBurner.ViewModels.FirmwareSources;
using FirmwarePacking;

namespace FirmwareBurner.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public MainViewModel(
            ModuleSelectorModel ModuleSelector,
            RepoFirmwareSource AutoFirmwareSource,
            ManualFirmwareSource UserFirmwareSource,
            BurningViewModel Burner,
            BlockDetailsViewModel BlockDetails)
        {
            this.ModuleSelector = ModuleSelector;
            this.AutoFirmwareSource = AutoFirmwareSource;
            this.UserFirmwareSource = UserFirmwareSource;
            this.BlockDetails = BlockDetails;
            this.Burner = Burner;

            Burner.BlockDetails = BlockDetails;

            ModuleSelector.Channels.First().IsSelected = true;
            foreach (ModuleSelectorModel.ChannelModel c in ModuleSelector.Channels.Skip(1)) c.IsSelected = false;
            ModuleSelector.SelectionChanged += Module_SelectionChanged;
            Module_SelectionChanged(ModuleSelector, new ModuleSelectorModel.ModuleSelectedEventArgs());

            AutoFirmwareSource.PackageSelected += FirmwareSource_PackageSelected;
            FirmwareSource_PackageSelected(AutoFirmwareSource, new EventArgs());
        }

        public ModuleSelectorModel ModuleSelector { get; set; }

        public RepoFirmwareSource AutoFirmwareSource { get; private set; }
        public ManualFirmwareSource UserFirmwareSource { get; private set; }

        public BlockDetailsViewModel BlockDetails { get; set; }

        public BurningViewModel Burner { get; set; }

        private void FirmwareSource_PackageSelected(object sender, EventArgs e) { Burner.Firmware = ((FirmwareSource)sender).SelectedPackage; }

        private void Module_SelectionChanged(object sender, ModuleSelectorModel.ModuleSelectedEventArgs e)
        {
            ComponentTarget selectedTarget = ModuleSelector.GetTargets(true).First();
            AutoFirmwareSource.CheckTarget(selectedTarget);
            Burner.Target = selectedTarget;
        }
    }
}
