using System.Windows.Controls;
using FirmwarePacker.ViewModels.Factories;

namespace FirmwarePacker.Views
{
    /// <summary>Логика взаимодействия для RootView.xaml</summary>
    public partial class RootView : UserControl
    {
        public RootView(RootViewModelFactory ViewModelFactory)
        {
            InitializeComponent();
            DataContext = ViewModelFactory.GetViewModel();
        }
    }
}
