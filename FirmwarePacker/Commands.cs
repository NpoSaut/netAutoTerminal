using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace FirmwarePacker
{
    public static class Commands
    {
        public static readonly RoutedUICommand Clone = new RoutedUICommand("Дублировать", "Clone", typeof(MainWindow));
    }
}
