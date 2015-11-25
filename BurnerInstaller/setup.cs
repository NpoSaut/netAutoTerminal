using System;
using System.IO;
using System.Linq;
using System.Reflection;
using WixSharp;
using WixSharp.CommonTasks;
using Assembly = System.Reflection.Assembly;
using File = WixSharp.File;

public class Script
{
    public static void Main(string[] args)
    {
        var dir = new DirectoryInfo(@"..\..\..\FirmwareBurner\bin\Release");
        string exeFileFullName = dir.EnumerateFiles("*.exe").First().FullName;
        string exeFileName = Path.GetFileName(exeFileFullName);
        Assembly assembly = Assembly.LoadFile(exeFileFullName);

        string projectName = assembly.GetCustomAttributes<AssemblyTitleAttribute>().First().Title;
        string company = assembly.GetCustomAttributes<AssemblyCompanyAttribute>().First().Company;

        string targetDirectoryPath = string.Format("%ProgramFiles%\\Saut\\{0}", projectName);

        var items = new Files(dir.FullName + "\\*.*", CheckFile);
        var project = new Project(projectName,
                                  new Dir(targetDirectoryPath,
                                          items),

                                  // Shortcuts
                                  new Dir(Path.Combine("%ProgramMenu%", company),
                                          new ExeFileShortcut(projectName, "[INSTALL_DIR]" + exeFileName, "") { WorkingDirectory = "INSTALL_DIR" },
                                          new ExeFileShortcut("Удалить " + projectName, "[System64Folder]msiexec.exe", "/x [ProductCode]")
                                          {
                                              WorkingDirectory = "INSTALLDIR"
                                          }
                                      ),
                                  new Dir("%Desktop%",
                                          new ExeFileShortcut(projectName, "[INSTALLDIR]" + exeFileName, "") { WorkingDirectory = "INSTALL_DIR" }
                                      ));

        File exeFile = project.ResolveWildCards(true)
                              .FindFile(f => f.Name.EndsWith(exeFileName))
                              .First();

        project.UI = WUI.WixUI_InstallDir;

        project.Language = "ru-RU";
        project.Version = assembly.GetName().Version;
        project.Description = assembly.GetCustomAttributes<AssemblyDescriptionAttribute>().First().Description;
        project.ControlPanelInfo.Manufacturer = company;
        project.ControlPanelInfo.ProductIcon = @"..\..\..\FirmwareBurner\icon.ico";
        project.ControlPanelInfo.HelpLink = "https://repo.nposaut.ru/#/tools?tool=Burner";
        project.ControlPanelInfo.NoRepair = true;
        project.ControlPanelInfo.NoModify = true;

        project.GUID = new Guid("6f330b47-2577-43ad-9095-1861ba25889b");
        project.MajorUpgradeStrategy = MajorUpgradeStrategy.Default;
        project.OutDir = @"..\..\..\installers";

        Compiler.BuildMsi(project);
        Console.WriteLine("done");
        Console.ReadLine();
    }

    private static bool CheckFile(string file)
    {
        return
            !file.Contains(".vhost.exe") &&
            !file.EndsWith(".pdb");
    }
}
