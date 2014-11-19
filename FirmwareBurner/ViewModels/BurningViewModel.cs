﻿using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using FirmwareBurner.Burning;
using FirmwareBurner.Burning.Exceptions;
using FirmwareBurner.Formating;
using FirmwareBurner.ViewModels.Bases;
using FirmwareBurner.ViewModels.Targeting;
using FirmwarePacking;

namespace FirmwareBurner.ViewModels
{
    public class BurningViewModel : ViewModelBase
    {
        private BurningViewModel() { BurnCommand = new ActionCommand(Burn, CanBurn); }

        public BurningViewModel(IFirmwareBurner WithBurner, IFirmwareFormatter WithFormatter, IFirmwareCook Coock)
            : this()
        {
            Burner = WithBurner;
            Formatter = WithFormatter;
            this.Coock = Coock;
        }

        private IFirmwareBurner Burner { get; set; }
        private IFirmwareFormatter Formatter { get; set; }
        private IFirmwareCook Coock { get; set; }

        public FirmwarePackage Firmware { get; set; }
        public ComponentTarget Target { get; set; }
        public BlockDetailsViewModel BlockDetails { get; set; }

        public ActionCommand BurnCommand { get; private set; }

        public bool InProgress { get; set; }

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
            /*InProgress = true;
            CommandManager.InvalidateRequerySuggested();
            Task.Factory.StartNew(() =>
                                  {
                                      try
                                      {
                                          Pie p = Coock.Cook(Firmware, Target, BlockDetails.SerialNumber, BlockDetails.AssemblyDate);
                                          Burner.Burn(p);
                                          Dispatcher.BeginInvoke((Func<String, String, MessageBoxButton, MessageBoxImage, MessageBoxResult>)MessageBox.Show,
                                                                 string.Format("Канал {0} записан", Target.Channel), "Готово", MessageBoxButton.OK,
                                                                 MessageBoxImage.Information);
                                      }
                                      catch (BurningException exc)
                                      {
                                          MessageBox.Show(string.Format("При работе с программатором возникла ошибка:\n{0}", exc.Message), "Ошибка при записи",
                                                          MessageBoxButton.OK, MessageBoxImage.Error);
                                      }
                                      finally
                                      {
                                          InProgress = false;
                                          CommandManager.InvalidateRequerySuggested();
                                      }
                                  });*/
        }
    }
}