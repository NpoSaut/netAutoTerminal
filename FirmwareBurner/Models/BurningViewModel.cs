using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FirmwareBurner.Burning;
using FirmwareBurner.Formating;
using FirmwarePacking;

namespace FirmwareBurner.Models
{
    public class BurningViewModel : ViewModelBase
    {
        private IFirmwareBurner Burner { get; set; }
        private IFirmwareFormatter Formatter { get; set; }
        private IFirmwareCook Coock { get; set; }

        public FirmwarePackage Firmware { get; set; }
        public ComponentTarget Target { get; set; }
        public BlockDetailsViewModel BlockDetails { get; set; }

        public ActionCommand BurnCommand { get; private set; }

        public bool InProgress { get; set; }

        private BurningViewModel()
        {
            BurnCommand = new ActionCommand(Burn, CanBurn);
        }
        public BurningViewModel(IFirmwareBurner WithBurner, IFirmwareFormatter WithFormatter, IFirmwareCook Coock)
            : this()
        {
            this.Burner = WithBurner;
            this.Formatter = WithFormatter;
            this.Coock = Coock;
        }

        private bool CanBurn()
        {
            return
                Firmware != null &&
                Target != null &&
                BlockDetails != null &&
                BlockDetails.SerialNumber > 0 &&
                BlockDetails.AssemblyDate <= DateTime.Now &&
                Firmware.Components.Any(c => c.Targets.Contains(Target)) &&
                !InProgress;
        }
        private void Burn()
        {
            InProgress = true;
            System.Windows.Input.CommandManager.InvalidateRequerySuggested();
            System.Threading.Tasks.Task.Factory.StartNew(() =>
                {
                    try
                    {
                        Pie p = Coock.Cook(Firmware, Target, BlockDetails.SerialNumber, BlockDetails.AssemblyDate);
                        Burner.Burn(p);
                        Dispatcher.BeginInvoke((Func<String, String, System.Windows.MessageBoxButton, System.Windows.MessageBoxImage, System.Windows.MessageBoxResult>)System.Windows.MessageBox.Show,
                            string.Format("Канал {0} записан", Target.Channel), "Готово", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                    }
                    catch (Burning.Exceptions.BurningException exc)
                    {
                        System.Windows.MessageBox.Show(string.Format("При работе с программатором возникла ошибка:\n{0}", exc.Message), "Ошибка при записи", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
                    }
                    finally
                    {
                        InProgress = false;
                        System.Windows.Input.CommandManager.InvalidateRequerySuggested();
                    }
                });
        }
    }
}
