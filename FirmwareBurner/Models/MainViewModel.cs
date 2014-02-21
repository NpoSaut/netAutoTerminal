using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FirmwarePacker.Models;
using FirmwareBurner.Models.FirmwareSources;

namespace FirmwareBurner.Models
{
    public class MainViewModel : ViewModelBase
    {
        public ModuleSelectorModel ModuleSelector { get; set; }

        public RepoFirmwareSource AutoFirmwareSource { get; private set; }
        public ManualFirmwareSource UserFirmwareSource { get; private set; }

        public BlockDetailsViewModel BlockDetails { get; set; }

        public BurningViewModel Burner { get; set; }

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
            foreach (var c in ModuleSelector.Channels.Skip(1)) c.IsSelected = false;
            ModuleSelector.SelectionChanged += Module_SelectionChanged;
            Module_SelectionChanged(ModuleSelector, new ModuleSelectorModel.ModuleSelectedEventArgs());

            AutoFirmwareSource.PackageSelected += FirmwareSource_PackageSelected;
            FirmwareSource_PackageSelected(AutoFirmwareSource, new EventArgs());
        }

        void FirmwareSource_PackageSelected(object sender, EventArgs e)
        {
            Burner.Firmware = ((FirmwareSource)sender).SelectedPackage;
        }

        void Module_SelectionChanged(object sender, ModuleSelectorModel.ModuleSelectedEventArgs e)
        {
            var SelectedTarget = ModuleSelector.GetTargets(true).First();
            AutoFirmwareSource.CheckTarget(SelectedTarget);
            Burner.Target = SelectedTarget;
        }
    }
}
