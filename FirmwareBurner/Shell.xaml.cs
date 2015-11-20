using System;
using System.Reflection;
using System.Windows;

namespace FirmwareBurner
{
    /// <summary>Логика взаимодействия для Shell.xaml</summary>
    public partial class Shell : Window
    {
        public Shell()
        {
            InitializeComponent();
            Version version = Assembly.GetAssembly(typeof (App)).GetName().Version;
            Title += string.Format(" (ver. {0})", version.ToString(2));
            VersionIndicator.Text = version.ToString(2);
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e) { Close(); }
    }
}
