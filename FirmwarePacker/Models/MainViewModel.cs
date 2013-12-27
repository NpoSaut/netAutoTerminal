using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using Microsoft.Practices.Unity;
using System.Windows.Input;

namespace FirmwarePacker.Models
{
    public class MainViewModel : ViewModel, IDataCheck
    {
        public ObservableCollection<FirmwareComponentViewModel> Components { get; private set; }

        private Version _FirmwareVersion;
        public Version FirmwareVersion
        {
            get { return _FirmwareVersion; }
            set
            {
                if (_FirmwareVersion != value)
                {
                    _FirmwareVersion = value;
                    OnPropertyChanged("FirmwareVersion");
                }
            }
        }

        private String _FirmwareVersionLabel;
        public String FirmwareVersionLabel
        {
            get { return _FirmwareVersionLabel; }
            set
            {
                if (_FirmwareVersionLabel != value)
                {
                    _FirmwareVersionLabel = value;
                    OnPropertyChanged("FirmwareVersionLabel");
                }
            }
        }

        private DateTime _ReleaseDate;
        public DateTime ReleaseDate
        {
            get { return _ReleaseDate; }
            set
            {
                if (_ReleaseDate != value)
                {
                    _ReleaseDate = value;
                    OnPropertyChanged("ReleaseDate");
                }
            }
        }

        public ICommand SaveCommand { get; private set; }
        
        public MainViewModel()
            : base()
        {
            Components = new ObservableCollection<FirmwareComponentViewModel>();
            Components.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(Components_CollectionChanged);

            FirmwareComponentViewModel model;
            Components.Add(model = ServiceLocator.Container.Resolve<FirmwareComponentViewModel>());
            
            if (App.CurrentApp.ArgsDirectory != null)
                model.Tree = new FirmwareTreeViewModel(App.CurrentApp.ArgsDirectory);

            SaveCommand = new ActionCommand(Save, Check);

            FirmwareVersion =
                new Version(
                        Properties.Settings.Default.LastMajorVersion,
                        Properties.Settings.Default.LastMinorVersion + 1
                    );
            ReleaseDate = DateTime.Now;
        }

        public bool Check()
        {
            // TODO: Добавить логику проверки покрытия, уникальности и непересекаемости компонентов!
            return
                Components.All(c => c.Check());
        }


        void Components_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
                foreach (var i in e.NewItems.OfType<FirmwareComponentViewModel>())
                {
                    i.TreeChanged += FirmwareComponent_TreeChanged;
                    i.CloneCommand = new ActionCommand(() => CloneComponent(i));
                    i.RemoveCommand = new ActionCommand(() => RemoveComponent(i), () => Components.Count > 1);
                }

            if (e.OldItems != null)
                foreach (var i in e.OldItems.OfType<FirmwareComponentViewModel>())
                {
                    i.TreeChanged -= FirmwareComponent_TreeChanged;
                    i.CloneCommand = null;
                    i.RemoveCommand = null;
                }
        }
        
        public void CloneComponent(FirmwareComponentViewModel component)
        { Components.Add(component.DeepClone()); }
        public void RemoveComponent(FirmwareComponentViewModel component)
        { Components.Remove(component); }

        void FirmwareComponent_TreeChanged(object sender, EventArgs e)
        {
            // Выбираем дату билда прошивки как максимальную дату среди дат последней модификации всех файлов в прошивке
            this.ReleaseDate = Components.SelectMany(c => c.Tree.Files.Select(f => f.LastWriteTime)).DefaultIfEmpty(DateTime.Now).Max();
        }

        public void Save()
        {
            var selector = ServiceLocator.Container.Resolve<Dialogs.IFileSelector>();
            selector.Filters =
                new Dictionary<String, String>()
                {
                    { "Файл пакета", "*." + FirmwarePacking.FirmwarePackage.FirmwarePackageExtension },
                    { "Все файлы", "*.*"}
                };
            selector.Message = "Выберите файл для сохранения пакета";
            var fileName =
                selector.SelectSave(string.Format("{0} ver. {1}{2}",
                                                  Components.First().TargetModule.SelectedBlockKind.Name,
                                                  FirmwareVersion.ToString(2),
                                                  string.IsNullOrWhiteSpace(FirmwareVersionLabel)
                                                      ? ""
                                                      : FirmwareVersionLabel));
            if (fileName != null)
            {
                var pack = PackageFormatter.Enpack(this);
                pack.Save(fileName);
            }
            Properties.Settings.Default.LastMajorVersion = this.FirmwareVersion.Major;
            Properties.Settings.Default.LastMinorVersion = this.FirmwareVersion.Minor;
            Properties.Settings.Default.Save();
        }
    }
}
