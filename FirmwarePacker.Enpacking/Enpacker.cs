using System;
using System.Linq;
using FirmwarePacker.Project;
using FirmwarePacking;

namespace FirmwarePacker.Enpacking
{
    public class Enpacker : IEnpacker
    {
        public FirmwarePackage Enpack(PackageProject Project, PackageVersion Version, string RootDirectory)
        {
            return new FirmwarePackage
                   {
                       Components = Project.Components
                                           .Select(componentProject =>
                                                   new FirmwareComponent(
                                                       componentProject.Targets.Select(
                                                           t => new ComponentTarget(t.Cell, t.Modification, t.Channel, t.Module)).ToList())
                                                   {
                                                       Files = componentProject.FileMaps
                                                                               .SelectMany(fm => fm.EnumerateFiles(RootDirectory))
                                                                               .Select(f => new FirmwareFile(f.Name, f.Content))
                                                                               .ToList()
                                                   })
                                           .ToList(),
                       Information = new PackageInformation(new Version(Version.Major, Version.Minor), Version.Label, Version.ReleaseDate)
                   };
        }
    }
}
