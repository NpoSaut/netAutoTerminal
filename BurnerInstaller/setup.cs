using System;
using System.Linq;
using WixSharp;

public class Script
{
    static public void Main(string[] args)
    {
        var dir = new System.IO.DirectoryInfo(@"bin\Release");
        var files = dir.EnumerateFiles().Where(f => CheckFile(f)).Select(f => new File(f.FullName)).ToArray();

        Project project =
            new Project("Burner",
                new Dir(@"%ProgramFiles%\Saut\Burner",
                    files));

        project.UI = WUI.WixUI_InstallDir;
        project.Version = System.Reflection.Assembly.GetAssembly(typeof (FirmwareBurner.App)).GetName().Version;
        project.GUID = new Guid("6f330b47-2577-43ad-9095-1861ba25889b");
        
        Compiler.BuildMsi(project);
    }

    static bool CheckFile(System.IO.FileInfo file)
    {
        return
            !file.Name.Contains(".vhost.exe") &&
            (file.Name.EndsWith(".dll") ||
             file.Name.EndsWith(".exe"));
    }
}



