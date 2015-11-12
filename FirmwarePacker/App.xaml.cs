using System;
using System.Windows;

namespace FirmwarePacker
{
    /// <summary>Логика взаимодействия для App.xaml</summary>
    public partial class App : Application
    {
        private PackerBootstrapper _bootstrapper;

        protected override void OnStartup(StartupEventArgs e)
        {
            //Current.DispatcherUnhandledException += (s, a) => ShowExceptionMessage(a.Exception);
            AppDomain.CurrentDomain.UnhandledException += (s, a) => ShowExceptionMessage((Exception)a.ExceptionObject);
            base.OnStartup(e);
            _bootstrapper = new PackerBootstrapper(e.Args);
            _bootstrapper.Run();
        }

        private static void ShowExceptionMessage(Exception Exc)
        {
            if (MessageBox.Show(
                String.Format("Непредвиденная ошибка при работе приложения:\n{0}\n\nСкопировать сведения об ошибке в буфер обмена?", Exc.Message),
                "Ошибка в приложении", MessageBoxButton.YesNo, MessageBoxImage.Error) == MessageBoxResult.Yes)
                Clipboard.SetText(Exc.ToString());
        }
    }
}
