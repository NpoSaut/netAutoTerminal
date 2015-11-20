using System;
using System.IO;
using System.Linq;
using WixSharp;
using File = WixSharp.File;

public class Script
{
    public static void Main(string[] args)
    {
        var dir = new DirectoryInfo(@"bin\Release");
        File[] files = dir.EnumerateFiles("*.*", SearchOption.AllDirectories)
                          .Where(f => CheckFile(f))
                          .Select(f => new File(f.FullName)).ToArray();

        var project =
            new Project("Burner",
                        MapDirectory(new DirectoryInfo(@"bin\Release"), @"%ProgramFiles%\Saut\Burner"));

        project.UI = WUI.WixUI_InstallDir;
        //project.Version = System.Reflection.Assembly.GetAssembly(typeof (FirmwareBurner.App)).GetName().Version;
        project.Version = new Version(1, 6);
        project.GUID = new Guid("6f330b47-2577-43ad-9095-1861ba25889b");
        //project.UpgradeCode = new Guid("6f330b47-2577-43ad-9095-1861ba25889b");

        Compiler.BuildMsi(project);
    }

    private static Dir MapDirectory(DirectoryInfo di, string MappingPath)
    {
        return new Dir(MappingPath,
                       Enumerable.Empty<WixEntity>()
                                 .Concat(di.EnumerateDirectories()
                                           .Select(d => MapDirectory(d, d.Name)))
                                 .Concat(di.EnumerateFiles()
                                           .Where(CheckFile)
                                           .Select(f => new File(f.FullName)))
                                 .ToArray());
    }

    private static bool CheckFile(FileInfo file)
    {
        return
            !file.Name.Contains(".vhost.exe") &&
            !file.Name.EndsWith(".pdb");
    }
}
