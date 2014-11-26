﻿using System;
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

        public BurningVariantViewModel(string Name, bool IsDefault, IBurningReceipt BurningReceipt, IProjectAssembler ProjectAssembler)
        {
            _burningReceipt = BurningReceipt;
            _projectAssembler = ProjectAssembler;
            this.Name = Name;
            this.IsDefault = IsDefault;

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
