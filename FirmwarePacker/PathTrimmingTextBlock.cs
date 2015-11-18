using System;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace FirmwarePacker
{
    public class PathTrimmingTextBlock : TextBlock, INotifyPropertyChanged
    {
        public static readonly DependencyProperty PathProperty =
            DependencyProperty.Register("Path", typeof (string), typeof (PathTrimmingTextBlock), new UIPropertyMetadata(""));

        private FrameworkElement _container;

        public PathTrimmingTextBlock() { Loaded += PathTrimmingTextBlock_Loaded; }

        public string Path
        {
            get { return (string)GetValue(PathProperty); }
            set { SetValue(PathProperty, value); }
        }

        private void PathTrimmingTextBlock_Loaded(object sender, RoutedEventArgs e)
        {
            if (Parent == null) throw new InvalidOperationException("PathTrimmingTextBlock must have a container such as a Grid.");

            _container = (FrameworkElement)Parent;
            _container.SizeChanged += container_SizeChanged;

            Text = GetTrimmedPath(_container.ActualWidth);
        }

        private void container_SizeChanged(object sender, SizeChangedEventArgs e) { Text = GetTrimmedPath(_container.ActualWidth); }

        private string GetTrimmedPath(double width)
        {
            string filename = System.IO.Path.GetFileName(Path);
            string directory = System.IO.Path.GetDirectoryName(Path);
            string drive = directory.Substring(0, directory.IndexOf(System.IO.Path.VolumeSeparatorChar) + 2);
            directory = directory.Substring(drive.Length);
            bool widthOk = false;
            bool changedWidth = false;

            do
            {
                var formatted = new FormattedText(
                    string.Format("{2}:\\...{0}\\{1}", directory, filename, drive),
                    CultureInfo.CurrentCulture,
                    FlowDirection.LeftToRight,
                    FontFamily.GetTypefaces().First(),
                    FontSize,
                    Foreground
                    );

                widthOk = formatted.Width < width;

                if (!widthOk)
                {
                    changedWidth = true;
                    directory = directory.Substring(directory.IndexOf(System.IO.Path.VolumeSeparatorChar) + 2);

                    if (directory.Length == 0) return "...\\" + filename;
                }
            } while (!widthOk);

            if (!changedWidth)
                return Path;
            return string.Format("{2}...{0}\\{1}", directory, filename, drive);
        }

        #region Implementation of INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string name) { if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(name)); }

        #endregion
    }
}
