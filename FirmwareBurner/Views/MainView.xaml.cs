using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using FirmwareBurner.ViewModels;

namespace FirmwareBurner.Views
{
    /// <summary>Логика взаимодействия для MainView.xaml</summary>
    public partial class MainView : UserControl
    {
        public MainView(MainViewModel ViewModel)
        {
            InitializeComponent();
            Loaded += (Sender, Args) => DataContext = ViewModel;
        }

        private void DragableElement_OnMouseDown(object Sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                Window window = Window.GetWindow(this);
                if (window != null) window.DragMove();
            }
        }
    }
}
