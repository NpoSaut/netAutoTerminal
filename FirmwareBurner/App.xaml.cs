using System;
using System.Windows;
using System.Windows.Threading;

namespace FirmwareBurner
{
    /// <summary>Логика взаимодействия для App.xaml</summary>
    public partial class App : Application
    {
        private BurnerBootstrapper _bootstrapper;

        protected override void OnStartup(StartupEventArgs e)
        {
            Current.DispatcherUnhandledException += CurrentOnDispatcherUnhandledException;
            base.OnStartup(e);
            _bootstrapper = new BurnerBootstrapper();
            _bootstrapper.Run();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
            _bootstrapper.Dispose();
        }

        private void CurrentOnDispatcherUnhandledException(object Sender, DispatcherUnhandledExceptionEventArgs e)
        {
            if (MessageBox.Show(
                String.Format("Непредвиденная ошибка при работе приложения:\n{0}\nСкопировать сведения об ошибке в буфер обмена?", e.Exception),
                "Ошибка в приложении", MessageBoxButton.YesNo, MessageBoxImage.Error) == MessageBoxResult.Yes)
                Clipboard.SetText(e.Exception.ToString());
        }
    }
}
