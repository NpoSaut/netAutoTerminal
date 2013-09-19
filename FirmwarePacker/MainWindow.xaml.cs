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

namespace FirmwarePacker
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainModel Model { get; private set; }

        public MainWindow()
        {
            // Необъяснимый костыль для поправки локализации строковых конвертеров
            Language = System.Windows.Markup.XmlLanguage.GetLanguage(System.Globalization.CultureInfo.CurrentCulture.IetfLanguageTag);

            Model = new MainModel();
            this.DataContext = Model;

            InitializeComponent();
        }

        private void SelectTreeCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var component = (sender as FrameworkElement).DataContext as FirmwareComponentModel;
            var dlg = new System.Windows.Forms.FolderBrowserDialog();
            if (component.Tree.RootDirectory != null) dlg.SelectedPath = component.Tree.RootDirectory.FullName;
            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                component.Tree = new FirmwareTreeModel(dlg.SelectedPath);
                Model.ReleaseDate = Model.Components.SelectMany(c => c.Tree.GetFiles()).AsParallel().Select(f => f.LastWriteTime).Max();
            }
        }

        private void CloneComponentCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var component = (sender as FrameworkElement).DataContext as FirmwareComponentModel;
            Model.Components.Add(component.DeepClone());
        }

        private void DeleteComponentCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = Model.Components.Count > 1;
        }
        private void DeleteComponentCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var component = (sender as FrameworkElement).DataContext as FirmwareComponentModel;
            Model.Components.Remove(component);
        }

        private void ComponentPresenter_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            (sender as FrameworkElement).Focus();
        }

        private void SavePackageCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        { e.CanExecute = Model.Check(); }
        private void SavePackageCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var dlg = new Microsoft.Win32.SaveFileDialog() { Filter = string.Format("Файл пакета ПО (*.{0})|*.{0}|Все файлы (*.*)|*.*", FirmwarePacking.FirmwarePackage.FirmwarePackageExtension), DefaultExt = "*." + FirmwarePacking.FirmwarePackage.FirmwarePackageExtension };
            if (dlg.ShowDialog(this) == true)
            {
                var pack = PackageFormatter.Enpack(Model);
                pack.Save(dlg.FileName);
            }
        }
    }
}
