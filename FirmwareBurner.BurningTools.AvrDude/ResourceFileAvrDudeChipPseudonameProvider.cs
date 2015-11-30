using System;
using System.Collections.Generic;
using System.Linq;
using FirmwareBurner.BurningTools.AvrDude.Properties;

namespace FirmwareBurner.BurningTools.AvrDude
{
    /// <summary>Ищет псевдоним для названия процессора, исходя из списка в файле <see cref="Resources.ChipPseudonames" />
    /// </summary>
    public class ResourceFileAvrDudeChipPseudonameProvider : IAvrDudeChipPseudonameProvider
    {
        private readonly Dictionary<string, string> _dictionary;

        public ResourceFileAvrDudeChipPseudonameProvider()
        {
            _dictionary = Resources.ChipPseudonames.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)
                                   .Select(line => line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries))
                                   .ToDictionary(line => line[1].ToLower(), line => line[0]);
        }

        /// <summary>Получает псевдоним названия процессора для указанного устройства</summary>
        /// <param name="DeviceName">Имя устройства</param>
        public string GetChipPseudoname(string DeviceName) { return _dictionary[DeviceName.ToLower()]; }
    }
}
