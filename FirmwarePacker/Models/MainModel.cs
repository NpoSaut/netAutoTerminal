using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace FirmwarePacker.Models
{
    public class MainModel : ViewModel, IDataCheck
    {
        public static FirmwarePacking.SystemsIndexes.Index Index { get; private set; }
        public ObservableCollection<FirmwareComponentModel> Components { get; private set; }

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

        public MainModel()
        {
            Components =
                new ObservableCollection<FirmwareComponentModel>()
                {
                    new FirmwareComponentModel()
                };
            FirmwareVersion = new Version(0, 0);
            ReleaseDate = DateTime.Now;
        }

        static MainModel()
        {
            Index = new FirmwarePacking.SystemsIndexes.XmlIndex("BlockKinds.xml");
        }

        public bool Check()
        {
            // TODO: Добавить логику проверки покрытия, уникальности и непересекаемости компонентов!
            return
                Components.All(c => c.Check());
        }
    }
}
