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
        private readonly IChannelSelector _channelSelector;

        public BurningVariantViewModel(IBurningReceipt BurningReceipt, IProjectAssembler ProjectAssembler, IChannelSelector ChannelSelector, bool IsDefault = false)
        {
            _burningReceipt = BurningReceipt;
            _projectAssembler = ProjectAssembler;
            _channelSelector = ChannelSelector;
            this.IsDefault = IsDefault;

            Name = BurningReceipt.Name;
            BurnCommand = new DelegateCommand(Burn);
        }

        /// <summary>Имя варианта прошивки</summary>
        public String Name { get; private set; }

        /// <summary>Признак того, что этот вариант является вариантом по-умолчанию</summary>
        public Boolean IsDefault { get; private set; }

        /// <summary>Команда на прошивку</summary>
        public ICommand BurnCommand { get; private set; }

        private void Burn()
        {
            FirmwareProject project = _projectAssembler.GetProject(_channelSelector.SelectedChannelNumber);
            _burningReceipt.Burn(project);
        }
    }
}
