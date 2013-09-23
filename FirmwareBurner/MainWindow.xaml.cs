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
using FirmwareBurner.Models;

namespace FirmwareBurner
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainModel Model { get; set; }

        public MainWindow()
        {
            Model = new MainModel();
            this.DataContext = Model;

            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            IntelHex.IntelHexStream h = new IntelHex.IntelHexStream();
            h.Write(new Byte[] { 0xaa, 0xbb, 0xcc }, 0, 3);
                                                                    Console.WriteLine(h.ToHexFormat());
            h.Seek(1, System.IO.SeekOrigin.Current);
            h.Write(new Byte[] { 0xaa, 0xbb, 0xcc }, 0, 3);
                                                                    Console.WriteLine(h.ToHexFormat());
            h.Seek(-1, System.IO.SeekOrigin.Current);
            h.Write(new Byte[] { 0xaa, 0xbb, 0xcc }, 0, 3);
                                                                    Console.WriteLine(h.ToHexFormat());
            h.Seek(1, System.IO.SeekOrigin.Current);
            h.Write(new Byte[] { 0xaa, 0xbb, 0xcc }, 0, 3);
                                                                    Console.WriteLine(h.ToHexFormat());
            h.Seek(2, System.IO.SeekOrigin.Begin);
            h.Write(new Byte[] { 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff }, 0, 9);
                                                                    Console.WriteLine(h.ToHexFormat());
        }
    }
}
