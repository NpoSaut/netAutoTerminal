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
using FirmwareBurner.ViewModel;

namespace FirmwareBurner
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow(MainViewModel Model)
        {
            this.DataContext = Model;
            InitializeComponent();
            var version = System.Reflection.Assembly.GetAssembly(typeof(App)).GetName().Version;
            Title += string.Format(" (ver. {0})", version);
        }
    }
}
