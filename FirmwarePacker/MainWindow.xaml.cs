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
using System.Windows.Forms;

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
            Model = new MainModel();
            this.DataContext = Model;

            InitializeComponent();
        }

        private void SelectTreeCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var component = (sender as FrameworkElement).DataContext as FirmwareComponentModel;
            var dlg = new FolderBrowserDialog();
            if (component.Tree.RootDirectory != null) dlg.SelectedPath = component.Tree.RootDirectory.FullName;
            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                component.Tree = new FirmwareTreeModel(dlg.SelectedPath);
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
    }
}
