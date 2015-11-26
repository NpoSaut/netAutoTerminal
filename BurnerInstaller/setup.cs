using System;
using System.IO;
using System.Linq;
using System.Reflection;
using InstallerTools;
using WixSharp;
using Assembly = System.Reflection.Assembly;

public class Script
{
    public static void Main(string[] args)
    {
        var dir = new DirectoryInfo(@"..\..\..\FirmwareBurner\bin\Release");

        string exeFileFullName = dir.EnumerateFiles("*.exe").First().FullName;
        string exeFileName = Path.GetFileName(exeFileFullName);
        Assembly assembly = Assembly.LoadFile(exeFileFullName);

        string projectName = assembly.GetCustomAttributes<AssemblyTitleAttribute>().First().Title;

        var mainFeature = new Feature("Программа", true) { AllowChange = false, ConfigurableDir = "INSTALLDIR" };

        var project = new Project(projectName,
                                  new Dir(mainFeature, InstallerHelper.GetSautProgramLocation(projectName),
                                          new Files(dir.FullName + "\\*.*", InstallerHelper.IsFileDesired)),
                                  new Dir(mainFeature, @"%PersonalFolder%\Firmwares"),

                                  // Shortcuts
                                  new Dir(mainFeature, @"%ProgramMenu%",
                                          new ExeFileShortcut(projectName, "[INSTALLDIR]" + exeFileName, "") { WorkingDirectory = "INSTALLDIR" }
                                      ),
                                  new Dir(mainFeature, "%Desktop%",
                                          new ExeFileShortcut(mainFeature, projectName, "[INSTALLDIR]" + exeFileName, "") { WorkingDirectory = "INSTALLDIR" }
                                      ))
                      {
                          DefaultFeature = mainFeature,
                          OutDir = @"..\..\..\installers"
                      };

        project.SetBasicThings(new Guid("6f330b47-2577-43ad-9095-1861ba25889b"), DotNetVersion.DotNet4);
        project.SetInterface(InstallerInterfaceKind.SelectDirectory);
        project.SetProjectInformation(assembly,
                                      @"..\..\..\FirmwareBurner\icon.ico",
                                      "https://repo.nposaut.ru/#/tools?tool=Burner");

        string msi = Compiler.BuildMsi(project);
        InstallerHelper.SignInstaller(msi);
        Console.ReadLine();
    }
}
