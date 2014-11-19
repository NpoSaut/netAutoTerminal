using System;
using System.Reflection;
using System.Windows;
using FirmwareBurner.ViewModels;

namespace FirmwareBurner
{
    /// <summary>Логика взаимодействия для Shell.xaml</summary>
    public partial class Shell : Window
    {
        public Shell(MainViewModel Model)
        {
            DataContext = Model;
            InitializeComponent();
            Version version = Assembly.GetAssembly(typeof (App)).GetName().Version;
            Title += string.Format(" (ver. {0})", version);
        }
    }
}
