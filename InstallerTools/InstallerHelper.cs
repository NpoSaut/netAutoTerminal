using System;
using System.Linq;
using System.Reflection;
using InstallerTools.Properties;
using WixSharp;
using WixSharp.CommonTasks;
using WixSharp.Controls;
using Assembly = System.Reflection.Assembly;

namespace InstallerTools
{
    public static class InstallerHelper
    {
        /// <summary>Настраивает интерфейс инсталлятора</summary>
        /// <param name="Project">Проект инсталлятора</param>
        /// <param name="Interface">Тип интерфейса</param>
        public static void SetInterface(this Project Project, InstallerInterfaceKind Interface)
        {
            switch (Interface)
            {
                case InstallerInterfaceKind.SelectDirectory:
                    Project.UI = WUI.WixUI_InstallDir;
                    Project.CustomUI = new DialogSequence()
                        .On(NativeDialogs.WelcomeDlg, Buttons.Next, new ShowDialog(NativeDialogs.InstallDirDlg))
                        .On(NativeDialogs.InstallDirDlg, Buttons.Back, new ShowDialog(NativeDialogs.WelcomeDlg));
                    break;

                case InstallerInterfaceKind.SelectFeatures:
                    Project.UI = WUI.WixUI_FeatureTree;
                    Project.CustomUI = new DialogSequence()
                        .On(NativeDialogs.WelcomeDlg, Buttons.Next, new ShowDialog(NativeDialogs.CustomizeDlg))
                        .On(NativeDialogs.CustomizeDlg, Buttons.Back, new ShowDialog(NativeDialogs.WelcomeDlg));
                    break;
            }
        }

        /// <summary>Устанавливает всяческую информацию о проекте, основываясь на информации из указываемой сборки</summary>
        /// <param name="Project">Проект инсталлятора</param>
        /// <param name="MainAssembly">Главная сборка, на основании которой заполнится информация о проекте</param>
        /// <param name="IconPath">Путь к файлу иконки</param>
        /// <param name="HelpLink">Ссылка на информацию о программе</param>
        public static void SetProjectInformation(this Project Project, Assembly MainAssembly, string IconPath, string HelpLink = null)
        {
            Project.Language = "ru-RU";
            Project.Version = MainAssembly.GetName().Version;
            Project.Description = MainAssembly.GetCustomAttributes<AssemblyDescriptionAttribute>().First().Description;
            Project.ControlPanelInfo.Manufacturer = MainAssembly.GetCustomAttributes<AssemblyCompanyAttribute>().First().Company;
            Project.ControlPanelInfo.ProductIcon = IconPath;
            Project.ControlPanelInfo.HelpLink = HelpLink;
            Project.ControlPanelInfo.NoRepair = true;
            Project.ControlPanelInfo.NoModify = true;
        }

        public static void SetBasicThings(this Project Project, Guid Guid, DotNetVersion DotNetVersion)
        {
            Project.GUID = Guid;
            Project.MajorUpgradeStrategy = MajorUpgradeStrategy.Default;
            Project.SetDotNetRequirement(DotNetVersion);
        }

        public static void SetDotNetRequirement(this Project Project, DotNetVersion Version)
        {
            switch (Version)
            {
                case DotNetVersion.DotNet4:
                    Project.SetNetFxPrerequisite("NETFRAMEWORK40CLIENT='#1'", "Программа требует установленного Microsoft .NET Framework 4.0 Client Profile");
                    break;
            }
        }

        public static void SignInstaller(string MsiPath)
        {
            string password = Settings.Default.CertificatePassword;
            if (string.IsNullOrWhiteSpace(password))
            {
                Console.WriteLine("Инсталлятор создан. Введите пароль к сертификату, чтобы подписать его или просто нажмите Enter, чтобы выйти:");
                Console.ForegroundColor = ConsoleColor.DarkGray;
                password = Console.ReadLine();
                Console.ResetColor();
            }
            if (string.IsNullOrWhiteSpace(password))
                return;

            int exitCode = Tasks.DigitalySign(MsiPath,
                                              "SautCodeSigning.pfx",
                                              "http://timestamp.verisign.com/scripts/timstamp.dll",
                                              password);
            if (exitCode != 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Could not sign the MSI file.");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("the MSI file was signed successfully.");
                Console.ResetColor();
                Settings.Default.CertificatePassword = password;
                Settings.Default.Save();
            }
        }

        public static bool IsFileDesired(string file)
        {
            return
                !file.Contains(".vhost.exe") &&
                !file.EndsWith(".pdb");
        }

        public static string GetSautProgramLocation(string ProjectName) { return string.Format(@"%ProgramFiles%\Saut\{0}", ProjectName); }
    }
}
