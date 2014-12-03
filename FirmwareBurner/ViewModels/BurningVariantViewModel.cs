using System;
using System.Windows.Input;
using FirmwareBurner.Burning;
using FirmwareBurner.Project;
using FirmwareBurner.ViewModels.Bases;
using Microsoft.Practices.Prism.Commands;

namespace FirmwareBurner.ViewModels
{
    /// <summary>Модель представления варианта прошивки устройства</summary>
    public class BurningVariantViewModel : ViewModelBase
    {
        private readonly IBurningReceipt _burningReceipt;
        private readonly IProjectAssembler _projectAssembler;

        public BurningVariantViewModel(IBurningReceipt BurningReceipt, IProjectAssembler ProjectAssembler, bool IsDefault = false)
        {
            _burningReceipt = BurningReceipt;
            _projectAssembler = ProjectAssembler;
            this.IsDefault = IsDefault;

            Name = BurningReceipt.Name;
            BurnCommand = new DelegateCommand<int?>(Burn);
        }

        /// <summary>Имя варианта прошивки</summary>
        public String Name { get; private set; }

        /// <summary>Признак того, что этот вариант является вариантом по-умолчанию</summary>
        public Boolean IsDefault { get; private set; }

        /// <summary>Команда на прошивку</summary>
        public ICommand BurnCommand { get; private set; }

        private void Burn(int? ChannelNumber)
        {
            if (!ChannelNumber.HasValue) throw new ArgumentNullException("ChannelNumber");
            FirmwareProject project = _projectAssembler.GetProject((int)ChannelNumber);
            _burningReceipt.Burn(project);
        }
    }
}
