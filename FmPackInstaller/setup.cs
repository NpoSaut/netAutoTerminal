using System;
using System.IO;
using System.Linq;
using InstallerTools;
using WixSharp;
using WixSharp.CommonTasks;
using Assembly = System.Reflection.Assembly;

namespace PackerInstaller
{
    internal class Script
    {
        private static void Main(string[] args)
        {
            var dir = new DirectoryInfo(@"..\..\..\FirmwarePacker\bin\Release");

            string exeFileFullName = dir.EnumerateFiles("*.exe").First().FullName;
            string exeFileName = Path.GetFileName(exeFileFullName);
            Assembly assembly = Assembly.LoadFile(exeFileFullName);

            const string projectName = "FmPack";

            var mainFeature = new Feature("Программа", true) { AllowChange = false, ConfigurableDir = "INSTALLDIR" };

            var project = new Project(projectName,
                                      new Dir(mainFeature, InstallerHelper.GetSautProgramLocation(projectName),
                                              new Files(dir.FullName + "\\*.*", InstallerHelper.IsFileDesired)),

                                      // Environment Variables
                                      new EnvironmentVariable("FmPack", "\"[INSTALLDIR]" + exeFileName + "\""),

                                      // Shortcuts
                                      new Dir(mainFeature, @"%ProgramMenu%\saut",
                                              new ExeFileShortcut(projectName, "[INSTALLDIR]" + exeFileName, "") { WorkingDirectory = "INSTALLDIR" }
                                          ),
                                      new Dir(mainFeature, "%Desktop%",
                                              new ExeFileShortcut(mainFeature, projectName, "[INSTALLDIR]" + exeFileName, "")
                                              {
                                                  WorkingDirectory = "INSTALLDIR"
                                              }
                                          ))
                          {
                              DefaultFeature = mainFeature,
                              OutDir = @"..\..\..\installers"
                          };

            project.ResolveWildCards(true)
                   .FindFile(f => f.Name.EndsWith(exeFileName))
                   .First()
                   .AddAssociation(new FileAssociation("fpc", "application/firmware-project", "Открыть", "\"%1\"")
                                   {
                                       Icon = "project.ico",
                                       Description = "Файл проекта для FmPack"
                                   });

            project.SetBasicThings(new Guid("1E7AA096-4665-4BE0-A5EB-D7EE62616E38"), DotNetVersion.DotNet4);
            project.SetInterface(InstallerInterfaceKind.SelectDirectory);
            project.SetProjectInformation(assembly,
                                          @"..\..\..\FirmwarePacker\icon.ico",
                                          "https://repo.nposaut.ru/#/tools?tool=FmPack");

            string msi = Compiler.BuildMsi(project);
            InstallerHelper.SignInstaller(msi);
        }
    }
}
