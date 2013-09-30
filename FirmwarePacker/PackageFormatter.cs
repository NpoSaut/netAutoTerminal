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
        public static FirmwarePackage Enpack(MainViewModel PackageModel)
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
                            new FirmwareComponent(ComponentModel.TargetModule.GetTargets().ToList())
                            {
                                Files = ComponentModel.Tree.Files.Select(f => ToFirmwareFile(f, ComponentModel.Tree.RootDirectory)).ToList()
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
    }
}
