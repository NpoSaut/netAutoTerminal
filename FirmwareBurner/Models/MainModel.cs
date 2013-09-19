using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FirmwarePacker.Models;
using FirmwareBurner.Models.FirmwareSources;

namespace FirmwareBurner.Models
{
    public class MainModel : ViewModel
    {
        public static FirmwarePacking.SystemsIndexes.Index Index { get; private set; }

        public ModuleSelectorModel Module { get; set; }

        public RepoFirmwareSource AutoFirmwareSource { get; private set; }
        public ManualFirmwareSource UserFirmwareSource { get; private set; }

        public MainModel()
        {
            Module = new ModuleSelectorModel(Index);
            AutoFirmwareSource = new RepoFirmwareSource();
            UserFirmwareSource = new ManualFirmwareSource();

            Module.SelectionChanged += new EventHandler<ModuleSelectorModel.ModuleSelectedEventArgs>(Module_SelectionChanged);
        }

        void Module_SelectionChanged(object sender, ModuleSelectorModel.ModuleSelectedEventArgs e)
        {
            AutoFirmwareSource.CheckTarget(Module.GetTargets(true).First());
        }

        static MainModel()
        {
            Index = new FirmwarePacking.SystemsIndexes.XmlIndex("BlockKinds.xml");
        }
    }
}
