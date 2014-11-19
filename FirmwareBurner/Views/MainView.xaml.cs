using System.Windows.Controls;
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
    }
}
