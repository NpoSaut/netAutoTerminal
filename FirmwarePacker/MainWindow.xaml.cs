using System;
using System.Globalization;
using System.Reflection;
using System.Windows;
using System.Windows.Input;
using System.Windows.Markup;

namespace FirmwarePacker
{
    /// <summary>Логика взаимодействия для MainWindow.xaml</summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            // Необъяснимый костыль для поправки локализации строковых конвертеров
            Language = XmlLanguage.GetLanguage(CultureInfo.CurrentCulture.IetfLanguageTag);

            InitializeComponent();

            Version version = Assembly.GetAssembly(typeof (App)).GetName().Version;
            Title += string.Format(" (ver. {0})", version.ToString(2));
        }

        private void ComponentPresenter_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e) { (sender as FrameworkElement).Focus(); }
    }
}
