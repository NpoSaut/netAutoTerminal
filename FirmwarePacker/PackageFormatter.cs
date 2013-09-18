using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FirmwarePacking;
using FirmwarePacker.Models;
using System.IO;

namespace FirmwarePacker
{
    public static class PackageFormatter
    {
        public static FirmwarePackage Enpack(MainModel PackageModel)
        {
            var res =
                new FirmwarePackage()
                {
                    Information =
                        new PackageInformation()
                        {
                            FirmwareVersion = PackageModel.FirmwareVersion,
                            ReleaseDate = PackageModel.ReleaseDate
                        },
                    Components =
                        PackageModel.Components.Select(ComponentModel =>
                            new FirmwareComponent(ToComponentTargets(ComponentModel.TargetModule))
                            {
                                Files = ComponentModel.Tree.GetFiles().Select(f => ToFirmwareFile(f, ComponentModel.Tree.RootDirectory)).ToList()
                            }).ToList()
                };
            return res;
        }

        private static FirmwareFile ToFirmwareFile(FileInfo f, DirectoryInfo root)
        {
            var buff = new Byte[f.Length];
            f.OpenRead().Read(buff, 0, buff.Length);
            string relativePath = f.FullName.Substring(root.FullName.Length).TrimStart(new char[] { Path.DirectorySeparatorChar });
            return new FirmwareFile(relativePath, buff);
        }

        private static IList<ComponentTarget> ToComponentTargets(ModuleSelectorModel TargetModel)
        {
            return
                TargetModel.Channels.Where(c => c.IsSelected).Select(channel =>
                    new ComponentTarget()
                    {
                        SystemId = TargetModel.SelectedSystemKind.Id,
                        CellId = TargetModel.SelectedBlockKind.Id,
                        CellModification = TargetModel.Modification,
                        Module = TargetModel.SelectedModuleKind.Id,
                        Channel = channel.Id
                    }).ToList();
        }
    }
}
