using System.Windows.Controls;
using FirmwarePacker.ViewModels;
using FirmwarePacker.ViewModels.Factories;

namespace FirmwarePacker.Views
{
    /// <summary>Логика взаимодействия для MainView.xaml</summary>
    public partial class MainView : UserControl
    {
        public MainView(MainViewModelFactory ViewModelFactory)
        {
            DataContext = ViewModelFactory.GetInstance();
            InitializeComponent();
        }
    }
}
