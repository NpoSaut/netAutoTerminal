using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using FirmwarePacker.Models;
using Microsoft.Practices.Unity;

namespace FirmwarePacker
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainViewModel Model { get; private set; }

        public MainWindow(MainViewModel Model)
        {
            // Необъяснимый костыль для поправки локализации строковых конвертеров
            Language = System.Windows.Markup.XmlLanguage.GetLanguage(System.Globalization.CultureInfo.CurrentCulture.IetfLanguageTag);

            this.Model = Model;
            this.DataContext = Model;

            InitializeComponent();

            var version = System.Reflection.Assembly.GetAssembly(typeof(App)).GetName().Version;
            Title += string.Format(" (ver. {0})", version);
        }

        private void ComponentPresenter_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            (sender as FrameworkElement).Focus();
        }
    }
}
