using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using FirmwareBurner.ViewModels;

namespace FirmwareBurner.Views
{
    /// <summary>Логика взаимодействия для BurningView.xaml</summary>
    public partial class BurningView : UserControl
    {
        public BurningView() { InitializeComponent(); }

        private void BurningMethodMenuItem_OnClick(object Sender, RoutedEventArgs E)
        {
            var item = (MenuItem)Sender;
            var method = (BurningMethodViewModel)item.DataContext;
            var model = (BurningViewModel)DataContext;
            model.SelectedBurningMethod = method;
        }

        private void Hyperlink_OnClick(object Sender, RoutedEventArgs E)
        {
            var menu = ((Hyperlink)Sender).ContextMenu;
            menu.Visibility = Visibility.Visible;
            menu.IsOpen = true;
        }
    }
}
