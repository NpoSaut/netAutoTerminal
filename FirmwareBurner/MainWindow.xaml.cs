using System;
using System.Reflection;
using System.Windows;
using FirmwareBurner.ViewModels;

namespace FirmwareBurner
{
    /// <summary>Логика взаимодействия для MainWindow.xaml</summary>
    public partial class MainWindow : Window
    {
        public MainWindow(MainViewModel Model)
        {
            DataContext = Model;
            InitializeComponent();
            Version version = Assembly.GetAssembly(typeof (App)).GetName().Version;
            Title += string.Format(" (ver. {0})", version);
        }
    }
}
