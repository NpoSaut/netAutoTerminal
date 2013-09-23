using System;
using System.Collections.Generic;
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
            
            Components.Add(ServiceLocator.Container.Resolve<FirmwareComponentViewModel>());

            SaveCommand = new ActionCommand(Save, Check);
            
            FirmwareVersion = new Version(0, 0);
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
            this.ReleaseDate = Components.SelectMany(c => c.Tree.GetFiles().Select(f => f.LastWriteTime)).DefaultIfEmpty(DateTime.Now).Max();
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
            var FileName = selector.SelectSave();
            if (FileName != null)
            {
                var pack = PackageFormatter.Enpack(this);
                pack.Save(FileName);
            }
        }
    }
}
