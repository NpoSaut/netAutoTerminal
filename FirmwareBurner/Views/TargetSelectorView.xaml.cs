using System.Windows.Controls;
using FirmwareBurner.ViewModels.Targeting;

namespace FirmwareBurner.Views
{
    /// <summary>Логика взаимодействия для TargetSelectorView.xaml</summary>
    public partial class TargetSelectorView : UserControl
    {
        public TargetSelectorView(TargetSelectorViewModel ViewModel)
        {
            InitializeComponent();
            Loaded += (Sender, Args) => DataContext = ViewModel;
        }
    }
}
